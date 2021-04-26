using System;
using System.Collections.Generic;
using System.Text;

namespace LibMintDBContext.Tables
{
    public class MintTransfer
    {
        public Guid Id { get; set; }
        public List<MintHolder> Holders { get; set; }
        public Guid SerialNumber { get; set; }
        public DateTime Timestamp { get; set; }
        public string Signature { get; set; }
    }
}
