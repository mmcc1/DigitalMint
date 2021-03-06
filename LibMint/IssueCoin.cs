using LibMintModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LibMint
{
    public class IssueCoin
    {
        public Coin Create(string issuingAuthority, string countryCode, string currencyCode, string purposeCode, decimal value, string privateKey, List<string> publicKey)
        {
            Coin coin = new Coin() { CountryCode = countryCode, IssueDate = DateTime.UtcNow, CurrencyCode = currencyCode, PurposeCode = purposeCode, IssuingAuthority = issuingAuthority, SerialNumber = Guid.NewGuid(), Value = value, Holders = new List<Holder>() };

            string jCoin = JsonConvert.SerializeObject(coin);
            coin.IssuingHash = CoinTools.ECSign(jCoin, privateKey);

            for(int i = 0; i < publicKey.Count; i++)
            {
                coin.Holders.Add(new Holder() { PublicKey = publicKey[i] });
            }

            jCoin = JsonConvert.SerializeObject(coin);
            coin.HolderHash = CoinTools.ECSign(jCoin, privateKey);

            return coin;
        }
    }
}
