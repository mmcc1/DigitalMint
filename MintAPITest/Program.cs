using LibMint;
using LibMintModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MintAPITest
{
    class Program
    {
        #region ECDSA Test Keys

        private static string privateECKey = @"-----BEGIN EC PRIVATE KEY-----
MHQCAQEEILLwDgKayNil3RxMTtz6isgw9p3piPjzz7EuAqghwqsLoAcGBSuBBAAK
oUQDQgAEze2Mh0XVtkiZ03rt5L0xIcQAEfxf75qjEO8C9fbVsJh1VDuOdESkNIIf
0YlNAz2PJ9IQ0ovDf+pM4Yt/m2Bacw==
-----END EC PRIVATE KEY-----";

        private static string publicECKey = @"-----BEGIN PUBLIC KEY-----
        MFYwEAYHKoZIzj0CAQYFK4EEAAoDQgAEze2Mh0XVtkiZ03rt5L0xIcQAEfxf75qj
        EO8C9fbVsJh1VDuOdESkNIIf0YlNAz2PJ9IQ0ovDf+pM4Yt/m2Bacw==
        -----END PUBLIC KEY-----";

        private static string transferPublicKey = @"-----BEGIN PUBLIC KEY-----
MFYwEAYHKoZIzj0CAQYFK4EEAAoDQgAExqbGohLO0Go0n8T0N0FZ9yO+WIduen9R
vL44upPLty5RHLf7TEdFavwezAyv2Lyr7Qc3mqwbh/wQDXjeo/K8ow==
-----END PUBLIC KEY-----
";
        #endregion

        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the Digital Mint. Press ENTER when API has fully started.");
            Console.ReadLine();

            Coin coin = await IssueCoin();
            PrintCoinDetails("Coin Issued.", coin);

            Console.WriteLine("Press ENTER to transfer.");
            Console.ReadLine();

            Coin transferredCoin = await TransferCoin(coin.SerialNumber, new List<string>() { transferPublicKey });
            PrintCoinDetails("Coin Transferred.", transferredCoin);

            Console.ReadLine();
        }

        #region Mint Methods

        private static async Task<Coin> IssueCoin()
        {
            IssueCoinRequest icr = new IssueCoinRequest();
            icr.CountryCode = "UK";
            icr.CurrencyCode = "GBP";
            icr.IssuingAuthority = "Bank of Digital";
            icr.Value = 10.00M;
            icr.HolderPublicKeys = new List<string>();
            icr.HolderPublicKeys.Add(publicECKey);

            using(var request = new HttpRequestMessage(new HttpMethod("POST"), "https://localhost:44341/api/Issue"))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(icr));
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                request.Headers.Add("x-api-key", "86A5DFC6-0EB7-4249-B32A-366F2F8F9A51");

                HttpResponseMessage response = null;

                try
                {
                    using (HttpClient client = new HttpClient())
                        response = await client.SendAsync(request);

                    if (response != null)
                        return JsonConvert.DeserializeObject<Coin>(await response.Content.ReadAsStringAsync());
                    else
                        throw new Exception();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
            }  
        }

        private static async Task<Coin> TransferCoin(Guid serialNumber, List<string> holdersPublicKey)
        {
            Transfer transfer = new Transfer();
            transfer.SerialNumber = serialNumber;
            transfer.Timestamp = DateTime.UtcNow;
            transfer.Holders = new List<Holder>();

            for(int i = 0; i < holdersPublicKey.Count; i++)
            {
                transfer.Holders.Add(new Holder() { PublicKey = holdersPublicKey[i] });
            }

            transfer.Signature = CoinTools.ECSign(JsonConvert.SerializeObject(transfer), privateECKey);

            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://localhost:44341/api/Transfer"))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(transfer));
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                request.Headers.Add("x-api-key", "86A5DFC6-0EB7-4249-B32A-366F2F8F9A51");

                HttpResponseMessage response = null;

                try
                {
                    using (HttpClient client = new HttpClient())
                        response = await client.SendAsync(request);

                    if (response != null)
                        return JsonConvert.DeserializeObject<Coin>(await response.Content.ReadAsStringAsync());
                    else
                        throw new Exception();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
            }
        }

        #endregion

        #region Helpers

        private static void PrintCoinDetails(string msg, Coin coin)
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(msg);
            Console.WriteLine("Issuing Authority: " + coin.IssuingAuthority);
            Console.WriteLine("Issuing Country: " + coin.CountryCode);
            Console.WriteLine("Serial Number: " + coin.SerialNumber);
            Console.WriteLine("CurrencyCode: " + coin.CurrencyCode);
            Console.WriteLine("Value: " + coin.Value);
            Console.WriteLine("Issuing Date: " + coin.IssueDate);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Issuing Hash: " + coin.IssuingHash);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Holder's Public Key: " + Environment.NewLine + coin.Holders[0].PublicKey);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Holder Hash: " + coin.HolderHash);
        }

        #endregion
    }
}
