using System.Collections.Generic;

namespace LibMintModels
{
    public class IssueCoinRequest
    {
        public string IssuingAuthority { get; set; }
        public string CountryCode { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Value { get; set; }
        public List<string> HolderPublicKeys { get; set; }
    }
}
