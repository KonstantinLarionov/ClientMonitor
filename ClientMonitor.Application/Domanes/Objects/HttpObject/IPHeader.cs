using System;
using System.IO;
using System.Net;

namespace ClientMonitor.Application.Domanes.Objects.HttpObject
{
    public class IPHeader
    {
        //IP Header fields
        private byte byVersionAndHeaderLength;   //Eight bits for version and header length
        private byte byDifferentiatedServices;    //Eight bits for differentiated services (TOS)
        private ushort usTotalLength;              //Sixteen bits for total length of the datagram (header + message)
        private ushort usIdentification;           //Sixteen bits for identification
        private ushort usFlagsAndOffset;           //Eight bits for flags and fragmentation offset
        private byte byTTL;                      //Eight bits for TTL (Time To Live)
        private byte byProtocol;                 //Eight bits for the underlying protocol
        private short sChecksum;                  //Sixteen bits containing the checksum of the header
                                                  //(checksum can be negative so taken as short)
        private uint uiSourceIPAddress;          //Thirty two bit source IP Address
        private uint uiDestinationIPAddress;     //Thirty two bit destination IP Address
                                                 //End IP Header fields

        private byte byHeaderLength;             //Header length
        private byte[] byIPData = new byte[4096];  //Data carried by the datagram


        public IPHeader(byte[] byBuffer, int nReceived)
        {

            try
            {
                MemoryStream memoryStream = new MemoryStream(byBuffer, 0, nReceived);
                BinaryReader binaryReader = new BinaryReader(memoryStream);
                byVersionAndHeaderLength = binaryReader.ReadByte();
                byDifferentiatedServices = binaryReader.ReadByte();
                usTotalLength = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                usIdentification = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                usFlagsAndOffset = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                byTTL = binaryReader.ReadByte();
                byProtocol = binaryReader.ReadByte();
                sChecksum = IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                uiSourceIPAddress = (uint)(binaryReader.ReadInt32());
                uiDestinationIPAddress = (uint)(binaryReader.ReadInt32());
                byHeaderLength = byVersionAndHeaderLength;
                byHeaderLength <<= 4;
                byHeaderLength >>= 4;
                byHeaderLength *= 4;
                Array.Copy(byBuffer,
                           byHeaderLength,
                           byIPData, 0,
                           usTotalLength - byHeaderLength);
            }
            catch (Exception ex) {}
        }
        public string Version
        {
            get
            {
                if ((byVersionAndHeaderLength >> 4) == 4)
                {
                    return "IP v4";
                }
                else if ((byVersionAndHeaderLength >> 4) == 6)
                {
                    return "IP v6";
                }
                else
                {
                    return "Unknown";
                }
            }
        }
        public string HeaderLength
        {
            get
            {
                return byHeaderLength.ToString();
            }
        }

        public ushort MessageLength
        {
            get
            {
                return (ushort)(usTotalLength - byHeaderLength);
            }
        }

        public string DifferentiatedServices
        {
            get
            {
                return string.Format("0x{0:x2} ({1})", byDifferentiatedServices,
                    byDifferentiatedServices);
            }
        }

        public string Flags
        {
            get
            {
                int nFlags = usFlagsAndOffset >> 13;
                if (nFlags == 2)
                {
                    return "Don't fragment";
                }
                else if (nFlags == 1)
                {
                    return "More fragments to come";
                }
                else
                {
                    return nFlags.ToString();
                }
            }
        }

        public string FragmentationOffset
        {
            get
            {
                int nOffset = usFlagsAndOffset << 3;
                nOffset >>= 3;

                return nOffset.ToString();
            }
        }

        public string TTL
        {
            get
            {
                return byTTL.ToString();
            }
        }

        public Protocol ProtocolType
        {
            get
            {
                if (byProtocol == 6)
                {
                    return Protocol.TCP;
                }
                else if (byProtocol == 17)
                {
                    return Protocol.UDP;
                }
                else
                {
                    return Protocol.Unknown;
                }
            }
        }

        public string Checksum
        {
            get
            {
                return string.Format("0x{0:x2}", sChecksum);
            }
        }

        public IPAddress SourceAddress
        {
            get
            {
                return new IPAddress(uiSourceIPAddress);
            }
        }

        public IPAddress DestinationAddress
        {
            get
            {
                return new IPAddress(uiDestinationIPAddress);
            }
        }

        public string TotalLength
        {
            get
            {
                return usTotalLength.ToString();
            }
        }

        public string Identification
        {
            get
            {
                return usIdentification.ToString();
            }
        }

        public byte[] Data
        {
            get
            {
                return byIPData;
            }
        }
    }
}
