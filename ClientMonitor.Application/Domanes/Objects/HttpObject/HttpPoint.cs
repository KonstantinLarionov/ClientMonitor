using System;
using System.Collections.Generic;
using System.Text;

namespace ClientMonitor.Application.Domanes.Objects.HttpObject
{
    public enum HttpProtocol
    {
        TCP,
        UDP
    }

    public class TCPPoint
    {
        public TCPPoint() { }
        public TCPPoint(string sourcePort, string destinationPort, string sequenceNumber, string acknowledgementNumber, string headerLength, string windowSize, string urgentPointer, string flags, string checksum, string data, ushort messageLength, DateTime dateTime)
        {
            SourcePort = sourcePort;
            DestinationPort = destinationPort;
            SequenceNumber = sequenceNumber;
            AcknowledgementNumber = acknowledgementNumber;
            HeaderLength = headerLength;
            WindowSize = windowSize;
            UrgentPointer = urgentPointer;
            Flags = flags;
            Checksum = checksum;
            // Data = data;
            MessageLength = messageLength;
            DateTime = dateTime;
        }
        public int Id { get; set; }
        public string SourcePort { get; set; }
        public string DestinationPort { get; set; }
        public string SequenceNumber { get; set; }
        public string AcknowledgementNumber { get; set; }
        public string HeaderLength { get; set; }
        public string WindowSize { get; set; }
        public string UrgentPointer { get; set; }
        public string Flags { get; set; }
        public string Checksum { get; set; }
        //        public string Data { get; set; }
        public ushort MessageLength { get; set; }
        public DateTime DateTime { get; set; }
    }
    public class UDPPoint
    {
        public UDPPoint() { }
        public UDPPoint(string sourcePort, string destinationPort, string length, string checksum, string data, DateTime dateTime)
        {
            SourcePort = sourcePort;
            DestinationPort = destinationPort;
            Length = length;
            Checksum = checksum;
            //Data = data;
            DateTime = dateTime;
        }

        public int Id { get; set; }
        public string SourcePort { get; set; }
        public string DestinationPort { get; set; }
        public string Length { get; set; }
        public string Checksum { get; set; }
        // public string Data { get; set; }
        public DateTime DateTime { get; set; }
    }
    public class HttpPoint
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int LenghtData { get; set; }
        public UDPPoint UDPPoint { get; set; }
        public TCPPoint TCPPoint { get; set; }
        public HttpProtocol Protocol { get; set; }
        public DateTime DateTime { get; set; }
    }
    public class HttpPointImport
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int LenghtData { get; set; }
        public string Protocol { get; set; }
        public string DateTime { get; set; }
    }
}
