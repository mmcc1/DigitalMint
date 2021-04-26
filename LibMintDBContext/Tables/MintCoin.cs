using System;
using System.Collections.Generic;
using System.Text;

namespace LibMintDBContext.Tables
{
    public class MintCoin
    {
        public Guid Id { get; set; }
        public string IssuingAuthority { get; set; }
        public Guid SerialNumber { get; set; }
        public string CountryCode { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Value { get; set; }
        public string IssuingHash { get; set; }
        public DateTime IssueDate { get; set; }
        public List<MintHolder> Holders { get; set; }
        public string HolderHash { get; set; }
    }
}
