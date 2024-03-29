﻿using System.IO;
using System.Net;

namespace ClientMonitor.Application.Domanes.Objects.HttpObject
{
    public class DNSHeader
    {
        private ushort usIdentification;        //Sixteen bits for identification
        private ushort usFlags;                 //Sixteen bits for DNS flags
        private ushort usTotalQuestions;        //Sixteen bits indicating the number of entries 
                                                //in the questions list
        private ushort usTotalAnswerRRs;        //Sixteen bits indicating the number of entries
                                                //entries in the answer resource record list
        private ushort usTotalAuthorityRRs;     //Sixteen bits indicating the number of entries
                                                //entries in the authority resource record list
        private ushort usTotalAdditionalRRs;    //Sixteen bits indicating the number of entries
                                                //entries in the additional resource record list
                                                //End DNS header fields
        public DNSHeader(byte[] byBuffer, int nReceived)
        {
            MemoryStream memoryStream = new MemoryStream(byBuffer, 0, nReceived);
            BinaryReader binaryReader = new BinaryReader(memoryStream);
            usIdentification = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            usFlags = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            usTotalQuestions = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            usTotalAnswerRRs = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            usTotalAuthorityRRs = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
            usTotalAdditionalRRs = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
        }
        public string Identification
        {
            get
            {
                return string.Format("0x{0:x2}", usIdentification);
            }
        }
        public string Flags
        {
            get
            {
                return string.Format("0x{0:x2}", usFlags);
            }
        }
        public string TotalQuestions
        {
            get
            {
                return usTotalQuestions.ToString();
            }
        }
        public string TotalAnswerRRs
        {
            get
            {
                return usTotalAnswerRRs.ToString();
            }
        }
        public string TotalAuthorityRRs
        {
            get
            {
                return usTotalAuthorityRRs.ToString();
            }
        }
        public string TotalAdditionalRRs
        {
            get
            {
                return usTotalAdditionalRRs.ToString();
            }
        }
    }
}
