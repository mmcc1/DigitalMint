using System;
using System.Collections.Generic;
using System.Text;

namespace LibMintModels
{
    //"Bank of Digital", "UK", "GBP", 10.00M, privateECKey, publicECKey
    public class IssueCoinRequest
    {
        public string IssuingAuthority { get; set; }
        public string CountryCode { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Value { get; set; }
        public List<string> HolderPublicKeys { get; set; }
    }
}
