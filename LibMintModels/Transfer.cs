using System;
using System.Collections.Generic;

namespace LibMintModels
{
    public class Transfer
    {
        public List<Holder> Holders { get; set; }
        public Guid SerialNumber { get; set; }
        public DateTime Timestamp { get; set; }
        public string Signature { get; set; }
    }
}
