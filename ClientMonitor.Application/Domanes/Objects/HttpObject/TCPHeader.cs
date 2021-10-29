using System;
using System.IO;
using System.Net;

namespace ClientMonitor.Application.Domanes.Objects.HttpObject
{
    public class TCPHeader
    {
        private ushort usSourcePort;              //Sixteen bits for the source port number
        private ushort usDestinationPort;         //Sixteen bits for the destination port number
        private uint uiSequenceNumber = 555;          //Thirty two bits for the sequence number
        private uint uiAcknowledgementNumber = 555;   //Thirty two bits for the acknowledgement number
        private ushort usDataOffsetAndFlags = 555;      //Sixteen bits for flags and data offset
        private ushort usWindow = 555;                  //Sixteen bits for the window size
        private short sChecksum = 555;                 //Sixteen bits for the checksum
                                                       //(checksum can be negative so taken as short)
        private ushort usUrgentPointer;           //Sixteen bits for the urgent pointer
                                                  //End TCP header fields

        private byte byHeaderLength;            //Header length
        private ushort usMessageLength;           //Length of the data being carried
        private byte[] byTCPData = new byte[4096];//Data carried by the TCP packet

        public TCPHeader(byte[] byBuffer, int nReceived)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream(byBuffer, 0, nReceived);
                BinaryReader binaryReader = new BinaryReader(memoryStream);
                usSourcePort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                usDestinationPort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                uiSequenceNumber = (uint)IPAddress.NetworkToHostOrder(binaryReader.ReadInt32());
                uiAcknowledgementNumber = (uint)IPAddress.NetworkToHostOrder(binaryReader.ReadInt32());
                usDataOffsetAndFlags = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                usWindow = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                sChecksum = (short)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                usUrgentPointer = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                byHeaderLength = (byte)(usDataOffsetAndFlags >> 12);
                byHeaderLength *= 4;
                usMessageLength = (ushort)(nReceived - byHeaderLength);
                Array.Copy(byBuffer, byHeaderLength, byTCPData, 0, nReceived - byHeaderLength);
            }
            catch (Exception ex) {}
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

        public string SequenceNumber
        {
            get
            {
                return uiSequenceNumber.ToString();
            }
        }

        public string AcknowledgementNumber
        {
            get
            {
                if ((usDataOffsetAndFlags & 0x10) != 0)
                {
                    return uiAcknowledgementNumber.ToString();
                }
                else
                    return "";
            }
        }

        public string HeaderLength
        {
            get
            {
                return byHeaderLength.ToString();
            }
        }

        public string WindowSize
        {
            get
            {
                return usWindow.ToString();
            }
        }

        public string UrgentPointer
        {
            get
            {
                if ((usDataOffsetAndFlags & 0x20) != 0)
                {
                    return usUrgentPointer.ToString();
                }
                else
                    return "";
            }
        }

        public string Flags
        {
            get
            {
                int nFlags = usDataOffsetAndFlags & 0x3F;

                string strFlags = string.Format("0x{0:x2} (", nFlags);

                if ((nFlags & 0x01) != 0)
                {
                    strFlags += "FIN, ";
                }
                if ((nFlags & 0x02) != 0)
                {
                    strFlags += "SYN, ";
                }
                if ((nFlags & 0x04) != 0)
                {
                    strFlags += "RST, ";
                }
                if ((nFlags & 0x08) != 0)
                {
                    strFlags += "PSH, ";
                }
                if ((nFlags & 0x10) != 0)
                {
                    strFlags += "ACK, ";
                }
                if ((nFlags & 0x20) != 0)
                {
                    strFlags += "URG";
                }
                strFlags += ")";

                if (strFlags.Contains("()"))
                {
                    strFlags = strFlags.Remove(strFlags.Length - 3);
                }
                else if (strFlags.Contains(", )"))
                {
                    strFlags = strFlags.Remove(strFlags.Length - 3, 2);
                }

                return strFlags;
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
                return byTCPData;
            }
        }

        public ushort MessageLength
        {
            get
            {
                return usMessageLength;
            }
        }
    }
}

