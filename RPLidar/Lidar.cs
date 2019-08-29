﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace RPLidar
{
    /// <summary>
    /// RPLidar device
    /// </summary>
    public partial class Lidar : IDisposable
    {
        /// <summary>
        /// Command byte
        /// </summary>
        private enum Command : byte
        {
            GetInfo = 0x50,
            GetHealth = 0x52,
            GetConfig = 0x84,
            Stop = 0x25,
            Reset = 0x40,
            Scan = 0x20,
            ExpressScan = 0x82
        };

        /// <summary>
        /// Config type word
        /// </summary>
        private enum ConfigType : uint
        {
            ScanModeCount = 0x70,
            ScanModeUsPerSample = 0x71,
            ScanModeMaxDistance = 0x74,
            ScanModeAnswerType = 0x75,
            ScanModeTypical = 0x7C,
            ScanModeName = 0x7F
        };

        // Constants
        private const byte SyncByte = 0xA5;
        private const byte SyncByte2 = 0x5A;
        private const int DescriptorLength = 7;
        private readonly Descriptor InfoDescriptor = new Descriptor(20, true, 0x04);
        private readonly Descriptor HealthDescriptor = new Descriptor(3, true, 0x06);
        private readonly Descriptor LegacyScanDescriptor = new Descriptor(5, false, 0x81);
        private readonly Descriptor ExpressLegacyScanDescriptor = new Descriptor(84, false, 0x82);

        // Variables
        private readonly SerialPort port;

        /// <summary>
        /// Logging event hanlder
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Arguments</param>
        public delegate void LogEventHandler(object sender, LogEventArgs e);

        /// <summary>
        /// Logging event
        /// </summary>
        public event LogEventHandler OnLog;

        /// <summary>
        /// Constructor
        /// </summary>
        public Lidar()
        {
            port = new SerialPort()
            {
                ReadTimeout = 500,
                ReadBufferSize = 32768,
                BaudRate = 115200
            };
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="portName">Port name</param>
        /// <param name="baudRate">Baud rate</param>
        /// <param name="readBufferSize">Read buffer size in bytes</param>
        public Lidar(string portName = "", int baudRate = 115200, int readBufferSize = 4096)
            : this()
        {
            PortName = portName;
            Baudrate = baudRate;
            ReadBufferSize = readBufferSize;
        }

        /// <summary>
        /// Port name
        /// </summary>
        public string PortName
        {
            get => port.PortName;
            set => port.PortName = value;
        }

        /// <summary>
        /// Baud rate
        /// </summary>
        public int Baudrate
        {
            get => port.BaudRate;
            set => port.BaudRate = value;
        }

        /// <summary>
        /// Read buffer size
        /// </summary>
        public int ReadBufferSize
        {
            get => port.ReadBufferSize;
            set => port.ReadBufferSize = value;
        }

        /// <summary>
        /// Receive timeout in milliseconds
        /// </summary>
        public int ReceiveTimeout
        {
            get => port.ReadTimeout;
            set => port.ReadTimeout = value;
        }

        /// <summary>
        /// Is port open ?
        /// </summary>
        public bool IsOpen
        {
            get
            {
                try
                {
                    return port.IsOpen;
                }
                catch (Exception ex)
                {
                    Log("Error at checking port status: " + ex.Message, Severity.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Try to open lidar port
        /// </summary>
        /// <returns>true if port was opened, false if it failed</returns>
        public bool Open()
        {
            try
            {
                if (!port.IsOpen)
                {
                    port.Open();
                }

                return port.IsOpen;
            }
            catch (Exception ex)
            {
                Log("Error at opening port: " + ex.Message, Severity.Error);
                return false;
            }
        }

        /// <summary>
        /// Try to close lidar port
        /// </summary>
        /// <returns>true if port was closed, false if it failed</returns>
        public bool Close()
        {
            try
            {
                if (port.IsOpen)
                {
                    port.Close();
                }

                return !port.IsOpen;
            }
            catch (Exception ex)
            {
                Log("Error at closing port: " + ex.Message, Severity.Error);
                return false;
            }
        }

        /// <summary>
        /// Disposing of the class instance
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (port.IsOpen)
                {
                    port.Close();
                }
            }
            catch (Exception)
            {
                // Ignore
            }
        }

        /// <summary>
        /// Number of bytes available for reading from port
        /// </summary>
        private int BytesToRead
        {
            get
            {
                try
                {
                    return port.BytesToRead;
                }
                catch (Exception ex)
                {
                    Log("Error at checking bytes to read: " + ex.Message, Severity.Error);
                    return 0;
                }
            }
        }

        /// <summary>
        /// Log message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="severity">Message severity</param>
        private void Log(string message, Severity severity = Severity.Info)
        {
            OnLog?.Invoke(this, new LogEventArgs(message, severity));
        }

        /// <summary>
        /// Timestamp in milliseconds
        /// </summary>
        public long Timestamp => Stopwatch.GetTimestamp() / (Stopwatch.Frequency / 1000);

        /// <summary>
        /// Control motor
        /// </summary>
        /// <param name="onOff">true to turn on motor, false to turn off</param>
        public void ControlMotor(bool onOff)
        {
            port.DtrEnable = onOff;
        }

        /// <summary>
        /// Send command
        /// </summary>
        /// <param name="command">Command</param>
        /// <returns>true if sent, false if failed</returns>
        private bool SendCommand(Command command)
        {
            try
            {
                port.Write(new byte[2] { SyncByte, (byte)command }, 0, 2);
            }
            catch (Exception ex)
            {
                Log("Error at sending command: " + ex.Message, Severity.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Send command
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="payload">Payload</param>
        /// <returns>true if sent, false if failed</returns>
        private bool SendCommand(Command command, byte[] payload)
        {
            byte[] data = new byte[4 + payload.Length];
            byte checksum = 0;

            data[0] = SyncByte;
            data[1] = (byte)command;
            data[2] = (byte)payload.Length;
            Array.Copy(payload, 0, data, 3, payload.Length);

            for (int i = 0; i < data.Length; i++)
            {
                checksum ^= data[i];
            }

            data[3 + payload.Length] = checksum;

            try
            {
                port.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                Log("Error at sending command: " + ex.Message, Severity.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Read descriptor
        /// </summary>
        /// <param name="descriptor">Descriptor</param>
        /// <returns>true if succeeded, false on failure</returns>
        private bool ReadDescriptor(out Descriptor descriptor)
        {
            List<byte> queue = new List<byte>();
            byte[] buffer = new byte[64];
            int missingBytes;
            int bytesRead;
            long startTime = Timestamp;

            descriptor = null;

            while (true)
            {
                // Try to receive as many bytes as are missing from complete packet
                missingBytes = DescriptorLength - queue.Count;
                try
                {
                    bytesRead = port.Read(buffer, 0, missingBytes);
                }
                catch (Exception ex)
                {
                    Log("Error at receiving descriptor: " + ex.Message, Severity.Error);
                    return false;
                }

                // Add bytes to the queue and check if we have complete descriptor
                queue.AddRange(buffer.Take(bytesRead));
                while (queue.Count >= DescriptorLength)
                {
                    if ((queue[0] == SyncByte) && (queue[1] == SyncByte2))
                    {
                        // Seems like we got our descriptor
                        // TODO Should consider with lengths above 255 ?
                        descriptor = new Descriptor(queue[2], queue[5] == 0, queue[6]);
                        return true;
                    }
                    else
                    {
                        // Pop first byte and check for sync bytes again
                        queue.RemoveAt(0);
                    }
                }

                // Timeout ?
                if ((Timestamp - startTime) > ReceiveTimeout)
                {
                    Log("Timeout on receiving descriptor", Severity.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Check read descriptor against expected
        /// </summary>
        /// <param name="readDescriptor">Read descriptor</param>
        /// <param name="expectedDescriptor">Expected descriptor</param>
        /// <returns>true if descriptor received, false if not</returns>
        private bool CheckDescriptor(Descriptor readDescriptor, Descriptor expectedDescriptor)
        {
            if ((expectedDescriptor.Length >= 0) && (expectedDescriptor.Length != readDescriptor.Length))
            {
                Log($"Expected descriptor length {expectedDescriptor.Length}, got {readDescriptor.Length}", Severity.Error);
                return false;
            }

            if (expectedDescriptor.IsSingle != readDescriptor.IsSingle)
            {
                Log($"Expected descriptor single to be {expectedDescriptor.IsSingle}, got {readDescriptor.IsSingle}", Severity.Error);
                return false;
            }

            if (expectedDescriptor.DataType != readDescriptor.DataType)
            {
                Log($"Expected descriptor data type 0x{expectedDescriptor.DataType:X2}, got 0x{readDescriptor.DataType:X2}", Severity.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Wait for descriptor
        /// </summary>
        /// <param name="expectedDescriptor">Descriptor to wait for</param>
        /// <returns>true if descriptor received, false if not</returns>
        private bool WaitForDescriptor(Descriptor expectedDescriptor)
        {
            if (!ReadDescriptor(out Descriptor readDescriptor)) return false;
            return CheckDescriptor(readDescriptor, expectedDescriptor);
        }

        /// <summary>
        /// Read response
        /// </summary>
        /// <param name="length">Number of bytes to wait for</param>
        /// <param name="data">Received data bytes</param>
        /// <returns>true if response data received, false if not</returns>
        private bool ReadResponse(int length, out byte[] data)
        {
            int dataIndex = 0;
            int bytesRead;
            long startTime = Timestamp;
            data = new byte[length];

            // Get required number of data bytes
            while (dataIndex < length)
            {
                try
                {
                    bytesRead = port.Read(data, dataIndex, length - dataIndex);
                }
                catch (Exception ex)
                {
                    Log("Error at reading response: " + ex.Message, Severity.Error);
                    return false;
                }

                dataIndex += bytesRead;

                // Timeout ?
                if ((Timestamp - startTime) > ReceiveTimeout)
                {
                    Log("Timeout on receiving data", Severity.Error);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Flush input buffer
        /// </summary>
        private bool FlushInput()
        {
            bufferedMeasurements.Clear();

            try
            {
                port.DiscardInBuffer();
                return true;
            }
            catch (Exception ex)
            {
                Log("Error on discarding input buffer: " + ex.Message, Severity.Error);
                return false;
            }
        }

        /// <summary>
        /// Reset lidar
        /// </summary>
        /// <returns>true if success, false if not</returns>
        public bool Reset()
        {
            if (!SendCommand(Command.Reset)) return false;
            Thread.Sleep(2);

            FlushInput();
            activeMode = null;
            return true;
        }

        /// <summary>
        /// Get info
        /// </summary>
        /// <param name="info">Lidar info</param>
        /// <returns>true if success, false if not</returns>
        public bool GetInfo(out LidarInfo info)
        {
            info = null;

            if (!SendCommand(Command.GetInfo)) return false;
            if (!WaitForDescriptor(InfoDescriptor)) return false;
            if (!ReadResponse(InfoDescriptor.Length, out byte[] data)) return false;

            info = new LidarInfo()
            {
                Model = data[0],
                Firmware = $"{data[2]}.{data[1]}",
                Hardware = data[3].ToString(),
                SerialNumber = BitConverter.ToString(data, 4).Replace("-", string.Empty)
            };

            return true;
        }

        /// <summary>
        /// Get health
        /// </summary>
        /// <param name="status">Lidar health status</param>
        /// <param name="errorCode">Possible error code</param>
        /// <returns>true if success, false if not</returns>
        public bool GetHealth(out HealthStatus status, out ushort errorCode)
        {
            status = HealthStatus.Unknown;
            errorCode = 0;

            if (!SendCommand(Command.GetHealth)) return false;
            if (!WaitForDescriptor(HealthDescriptor)) return false;
            if (!ReadResponse(HealthDescriptor.Length, out byte[] data)) return false;

            status = (HealthStatus)data[0];
            errorCode = BitConverter.ToUInt16(data, 1);

            return true;
        }

        /// <summary>
        /// Get lidar configuration type
        /// </summary>
        /// <param name="configType">Configuration type</param>
        /// <param name="requestPayload">Extra request payload bytes</param>
        /// <param name="expectedResponseLength">Expected response payload length</param>
        /// <param name="responsePayload">Response payload bytes</param>
        /// <returns>true if success, false if not</returns>
        private bool GetConfigurationType(ConfigType configType, byte[] requestPayload, int? expectedResponseLength, out byte[] responsePayload)
        {
            responsePayload = new byte[0];

            Descriptor expectedDescriptor = new Descriptor(-1, true, 0x20);
            if (expectedResponseLength.HasValue)
            {
                expectedDescriptor.Length = expectedResponseLength.Value + 4;
            }

            if (!SendCommand(Command.GetConfig, BitConverter.GetBytes((uint)configType).Concat(requestPayload).ToArray())) return false;
            if (!ReadDescriptor(out Descriptor readDescriptor)) return false;
            if (!CheckDescriptor(readDescriptor, expectedDescriptor)) return false;
            if (!ReadResponse(readDescriptor.Length, out byte[] responseRaw)) return false;

            // Verify response type
            uint responseType = BitConverter.ToUInt32(responseRaw, 0);
            if (responseType != (byte)configType)
            {
                Log($"Expected configuration response type 0x{configType:X2}, got 0x{responseType:X2}", Severity.Error);
                return false;
            }

            // Get response payload bytes only
            responsePayload = new byte[responseRaw.Length - 4];
            Array.Copy(responseRaw, 4, responsePayload, 0, responseRaw.Length - 4);

            return true;
        }

        /// <summary>
        /// Get lidar configuration
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <returns>true if success, false if not</returns>
        public bool GetConfiguration(out Configuration configuration)
        {
            byte[] response;

            configuration = new Configuration();

            // Get typical mode
            if (!GetConfigurationType(ConfigType.ScanModeTypical, new byte[0], 2, out response)) return false;
            configuration.Typical = BitConverter.ToUInt16(response, 0);

            // Get number of modes
            if (!GetConfigurationType(ConfigType.ScanModeCount, new byte[0], 2, out response)) return false;
            ushort count = BitConverter.ToUInt16(response, 0);

            // Create configurations of all modes
            configuration.Modes = new Dictionary<ushort, ScanModeConfiguration>();

            for (ushort mode = 0; mode < count; mode++)
            {
                ScanModeConfiguration modeConfiguration = new ScanModeConfiguration();
                byte[] modeBytes = BitConverter.GetBytes(mode);

                // Get name
                if (!GetConfigurationType(ConfigType.ScanModeName, modeBytes, null, out response)) return false;
                modeConfiguration.Name = Encoding.ASCII.GetString(response).TrimEnd('\0');

                // Get microseconds per sample
                if (!GetConfigurationType(ConfigType.ScanModeUsPerSample, modeBytes, 4, out response)) return false;
                modeConfiguration.UsPerSample = (float)BitConverter.ToUInt32(response, 0) / 256.0f;

                // Get maximum distance
                if (!GetConfigurationType(ConfigType.ScanModeMaxDistance, modeBytes, 4, out response)) return false;
                modeConfiguration.MaxDistance = (float)BitConverter.ToUInt32(response, 0) / 256.0f;

                // Ge answer type
                if (!GetConfigurationType(ConfigType.ScanModeAnswerType, modeBytes, 1, out response)) return false;
                modeConfiguration.AnswerType = response[0];

                // Add to list
                configuration.Modes.Add(mode, modeConfiguration);
            }

            return true;
        }
    }
}