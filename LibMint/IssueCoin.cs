using LibMintModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LibMint
{
    public class IssueCoin
    {
        public Coin Create(string issuingAuthority, string countryCode, string currencyCode, decimal value, string privateRSAKey, string publicRSAKey)
        {
            Coin coin = new Coin() { CountryCode = countryCode, IssueDate = DateTime.UtcNow, CurrencyCode = currencyCode, IssuingAuthority = issuingAuthority, SerialNumber = Guid.NewGuid(), Value = value, Holders = new List<Holder>() };
            
            string jCoin = JsonConvert.SerializeObject(coin);
            coin.IssuingHash = CoinTools.Sign(jCoin, privateRSAKey);

            coin.Holders.Add(new Holder() { PublicKey = publicRSAKey });
            jCoin = JsonConvert.SerializeObject(coin);
            coin.HolderHash = CoinTools.Sign(jCoin, privateRSAKey);

            return coin;
        }
    }
}
