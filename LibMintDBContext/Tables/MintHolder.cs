using System;

namespace LibMintDBContext.Tables
{
    public class MintHolder
    {
        public Guid Id { get; set; }
        public string PublicKey { get; set; }
        public Guid MintCoinId { get; set; }
    }
}
