﻿using System;
using System.IO;
using System.Net;

namespace ClientMonitor.Application.Domanes.Objects.HttpObject
{
    public class UDPHeader
    {
        private ushort usSourcePort;            //Sixteen bits for the source port number        
        private ushort usDestinationPort;       //Sixteen bits for the destination port number
        private ushort usLength;                //Length of the UDP header
        private short sChecksum;                //Sixteen bits for the checksum
                                                //(checksum can be negative so taken as short)              
                                                //End UDP header fields

        private byte[] byUDPData = new byte[4096];  //Data carried by the UDP packet

        public UDPHeader(byte[] byBuffer, int nReceived)
        {
            MemoryStream memoryStream = new MemoryStream(byBuffer, 0, nReceived);
            BinaryReader binaryReader = new BinaryReader(memoryStream);
            usSourcePort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            usDestinationPort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            usLength = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            sChecksum = IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            Array.Copy(byBuffer, 8, byUDPData,
                       0,
                       nReceived - 8);
        }

        public string SourcePort
        {
            get
            {
                return usSourcePort.ToString();
            }
        }

        public string DestinationPort
        {
            get
            {
                return usDestinationPort.ToString();
            }
        }

        public string Length
        {
            get
            {
                return usLength.ToString();
            }
        }

        public string Checksum
        {
            get
            {
                return string.Format("0x{0:x2}", sChecksum);
            }
        }

        public byte[] Data
        {
            get
            {
                return byUDPData;
            }
        }
    }
}
