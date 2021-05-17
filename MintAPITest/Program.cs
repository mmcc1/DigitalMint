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
MHQCAQEEIFsl9XUq6Z6xnx/HNiKtUr8+8g/Ym2RtMns09Q9Z2JENoAcGBSuBBAAK
oUQDQgAEJKZlnvyvg+F++rf6IcM/wT3W8Fyd0jt4foUUfjtwGHh1PllMoVY+dDBl
w3UIU+3iq36ZZ3gindq175Dpfcy17g==
-----END EC PRIVATE KEY-----";

        private static string publicECKey = @"-----BEGIN PUBLIC KEY-----
MFYwEAYHKoZIzj0CAQYFK4EEAAoDQgAEJKZlnvyvg+F++rf6IcM/wT3W8Fyd0jt4
foUUfjtwGHh1PllMoVY+dDBlw3UIU+3iq36ZZ3gindq175Dpfcy17g==
-----END PUBLIC KEY-----";

        private static string transferPrivateKey = @"-----BEGIN EC PRIVATE KEY-----
MHQCAQEEIL2UJl3yXLMkM3HLBHk5mk3fp+n4Pk9crMfNF41mFcecoAcGBSuBBAAK
oUQDQgAEKNuT1qkJTXEIKqHe6JfPbE+W0IyqZb1oDXlqtGxOoljpJ7a9TRkeAm9o
BgjOBx1K4SvZf7WCvfA8NjrBpAwirQ==
-----END EC PRIVATE KEY-----";

        private static string transferPublicKey = @"-----BEGIN PUBLIC KEY-----
MFYwEAYHKoZIzj0CAQYFK4EEAAoDQgAEKNuT1qkJTXEIKqHe6JfPbE+W0IyqZb1o
DXlqtGxOoljpJ7a9TRkeAm9oBgjOBx1K4SvZf7WCvfA8NjrBpAwirQ==
-----END PUBLIC KEY-----";
        #endregion

        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the Digital Mint. Press ENTER when API has fully started.");
            Console.ReadLine();

            Coin coin = await IssueCoin();
            PrintCoinDetails("Coin Issued.", coin);

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Press ENTER to transfer coin.");
            Console.ReadLine();

            Coin transferredCoin = await TransferCoin(coin.SerialNumber, coin.IssuingAuthority, new List<string>() { transferPublicKey });
            PrintCoinDetails("Coin Transferred.", transferredCoin);

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Press ENTER to search for coin.");
            Console.ReadLine();

            List<Coin> coins = await SearchCoin(transferPublicKey);

            for(int i = 0; i < coins.Count; i++)
            {
                Console.WriteLine(Environment.NewLine);
                PrintCoinDetails("Found Coin.", coins[i]);
            }

            Console.ReadLine();
        }

        #region Mint Methods

        #region Issue Coin

        private static async Task<Coin> IssueCoin()
        {
            IssueCoinRequest icr = new IssueCoinRequest();
            icr.CountryCode = "UK";
            icr.CurrencyCode = "GBP";
            icr.IssuingAuthority = "Bank of Digital";
            icr.PurposeCode = "General";
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

        #endregion

        #region Transfer Coin

        private static async Task<Coin> TransferCoin(Guid serialNumber, string issuingAuthority, List<string> holdersPublicKey)
        {
            Transfer transfer = new Transfer();
            transfer.SerialNumber = serialNumber;
            transfer.IssuingAuthority = issuingAuthority;
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

        #region Search Coin

        private static async Task<List<Coin>> SearchCoin(string publicKey)
        {
            Search search = new Search();
            search.PublicKey = publicKey;
            search.Timestamp = DateTime.UtcNow;
            search.Signature = CoinTools.ECSign(JsonConvert.SerializeObject(search), transferPrivateKey);

            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://localhost:44341/api/Search"))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(search));
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                request.Headers.Add("x-api-key", "86A5DFC6-0EB7-4249-B32A-366F2F8F9A51");

                HttpResponseMessage response = null;

                try
                {
                    using (HttpClient client = new HttpClient())
                        response = await client.SendAsync(request);

                    if (response != null)
                        return JsonConvert.DeserializeObject<List<Coin>>(await response.Content.ReadAsStringAsync());
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
            Console.WriteLine("PurposeCode: " + coin.PurposeCode);
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
