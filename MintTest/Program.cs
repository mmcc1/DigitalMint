using System;
using System.Collections.Generic;
using LibMint;
using LibMintModels;
using Newtonsoft.Json;

namespace MintTest
{
    class Program
    {
        private static string privateRSAKey = @"-----BEGIN RSA PRIVATE KEY-----
MIIEoQIBAAKCAQBkgS5bdHmfPFhLIOdxCpd/rEC92l1WvFr3cGUfySR2TP61whCj
PXzCkxe4vs6uCKhfoRPiKQV7a24VxNb+QRkmryTzpFlJutNhfMeiuBJRu/yrp/b7
kfNc1hlpDbMUR6jDH7/74xwJPrwTD5TaE1HM3FXVdYG+zPIwgGRPgrRz6M1MgIZS
wGMMDZdFpQfIagsAPKtmJQ47FfvzGRZ1wtYd0Y/fLWvJ0AQN2r+IENBtOGzDFw3U
7w5dzeYlde8fUuDJxNN9Y9N4DBFdHp/rIKg2fcfNA7jSKdhkgeGBkMgVcrrQLnI2
VXCT6c0fsTSW7EtDfCeP10SRF1/YDZuNFSI9AgMBAAECggEAMWCeK+RvlGILZu3F
h24SvHEeZagQz3o+nu6jYBhsR4rQYO/1SJ3+24F02Bk2ZZ5vSnxSznwk61v+e8d3
cPb+qljGKMWH8IdPLsglNLaGmY1oN9WPrE0qaWPunARVBROnwCWrJs+PiKM/t4Wi
Nfnj6Ggf632NvvXlzxplLgMjMQaBMmWsmUEsthBz60LcL3TRDqCPu3QHkimlmVpy
BEQn56HhtUVhWIF3b0NouqvCIoHDxTxmwnIMri6dvgDOIw1BicitV9ZNt75x+CsV
87LWjHMIiLtj15SEM9VVug5FZ8xY6TtnzqKngnS26iMBNzpM+hcg8Kg2IyvVO/4/
jIJvwQKBgQC/ZhRGq5F6g5vdm4JrqqBS7nuJbp/7HFpMND0huHDiUxprfkEmFkxh
ofM2kW7S0vC2YRMwuCUJ+1iLI6XtmFUq1unnO3M9V0ZVFhrMBX5uoWFfmvS+qSUP
rZPeTifa7WemD1FkIG8BFGms0sj2PS8c1SaeR2Cuv0dKwxAlSWyJiQKBgQCGbVdQ
cBHvlVztE6bud+DmRJhLY/Rs7kmDzSuYpcze0b7gIrkcpOj506vvk2s0BA71F0Kg
nbJxJlcMMZvAjfHkInJsqLLkSLdIfi9lrT2EoLjzViPNwVZca/03ytMzfRpbXB/R
TyyPe7bRGogTZgDC9v3g3u5VfJCFOLWO/DqKFQKBgFzKw5fDhCNOfRmCVEeYkGPE
hRYLEPqwM0LwrJaVkfzX514n9rPdaaiH2J8jAQSrCdKR27jp/eMJ/VJvtPksulWr
nfjiBKu/jTShI9q36yT9jnTIblGlNXeVYrQLVh04reB/WqldZBO07sq/4ngnD5k9
6Zwg5MmzqH+hdElvgHRpAoGAEIpjtAIWMuSuCn3gjHcztSG7m4sxCDZJ1W3pb173
VnN4iRu8q0mfYR4qidAZ4MyfColSKXE+A640B8wS3h6ZJPkYG0amvHA2HTVpn5kx
eKBOIaL3xNmFRtoCzlqmoi9CjvtG7vpPAVi3pgMdu0VR0quRkZncuagaIEpaDL38
dgECgYApGDM/qVHXTllimhpHBvmwTY/DeoG8oY8SxEs9iBSJLDfLccoxNf+xI6Cr
a2kq4Jg/ryD1p2PBZ7ESuLymWJhcAgnvIHult6YyjyIC06FwCrRC8oKzItZidRnF
tIhVv/HG/dF2blEWpYdLAM40ykSRJ4YkPriOiLNjQ2qewW3O/A==
-----END RSA PRIVATE KEY-----";

        private static string publicRSAKey = @"-----BEGIN PUBLIC KEY-----
MIIBITANBgkqhkiG9w0BAQEFAAOCAQ4AMIIBCQKCAQBkgS5bdHmfPFhLIOdxCpd/
rEC92l1WvFr3cGUfySR2TP61whCjPXzCkxe4vs6uCKhfoRPiKQV7a24VxNb+QRkm
ryTzpFlJutNhfMeiuBJRu/yrp/b7kfNc1hlpDbMUR6jDH7/74xwJPrwTD5TaE1HM
3FXVdYG+zPIwgGRPgrRz6M1MgIZSwGMMDZdFpQfIagsAPKtmJQ47FfvzGRZ1wtYd
0Y/fLWvJ0AQN2r+IENBtOGzDFw3U7w5dzeYlde8fUuDJxNN9Y9N4DBFdHp/rIKg2
fcfNA7jSKdhkgeGBkMgVcrrQLnI2VXCT6c0fsTSW7EtDfCeP10SRF1/YDZuNFSI9
AgMBAAE=
-----END PUBLIC KEY-----";

        private static string transferPublicKey = @"-----BEGIN PUBLIC KEY-----
MIIBITANBgkqhkiG9w0BAQEFAAOCAQ4AMIIBCQKCAQBqNTz1F9yp8rplV2DUBX3X
VSbrr9iO9oDlWgvcmTpePHP+eyQe+tG6jT9yMzdXnM6WZjscpxg77mSGvo6rB/Cb
GbD/cIGA6WtHGxlOX9O4oPxdTx3Fikse9Mq5myzyRVAghDhoF71GAV7RHcxCGH/1
5WSaTIJz4tYfoqKPwMX1O+hQI1YkD3pvjiWJJKoa2lmBxVH+udrMJ2RHRcGY5ygr
fPkusmaW0RrnSXosTtg1nA7Lcok4UIPquyKtQwEKw/k3QP/XgyuXyPqQB6rif59m
jc6pz989OCVjHjIO3LFIqRWK3RZQGLt9e99GW7vzN21UoXsiJ9m3WojDbMU0yEqR
AgMBAAE=
-----END PUBLIC KEY-----";

        static void Main(string[] args)
        {
            //Issue Coin
            Console.WriteLine("Issuing coin...");
            IssueCoin iC = new IssueCoin();
            Coin coin = iC.Create("Bank of Digital", "UK", 10.00M, privateRSAKey, publicRSAKey);

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Coin issued:");
            Console.WriteLine("Issuing Authority: " + coin.IssuingAuthority);
            Console.WriteLine("Issuing Country: " + coin.CountryCode);
            Console.WriteLine("Serial Number: " + coin.SerialNumber);
            Console.WriteLine("Value: " + coin.Value);
            Console.WriteLine("Issuing Date: " + coin.IssueDate);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Issuing Hash: " + coin.IssuingHash);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Holder's Public Key: " + Environment.NewLine + coin.Holders[0].PublicKey);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Holder Hash: " + coin.HolderHash);


            //Verify Holder Hash
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Verifying Holder Hash...");
            string hh = coin.HolderHash;
            coin.HolderHash = null;
            string jCoin = JsonConvert.SerializeObject(coin);

            if (CoinTools.Verify(jCoin, hh, publicRSAKey))
                Console.WriteLine("Holder Hash Verified");
            else
                Console.WriteLine("Invalid Holder Hash");


            //Verify Issue Hash
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Verifying Issue Hash...");
            Holder[] holders = coin.Holders.ToArray();
            coin.Holders.Clear();
            string hash = coin.IssuingHash;
            coin.IssuingHash = null;
            jCoin = JsonConvert.SerializeObject(coin);

            if (CoinTools.Verify(jCoin, hash, publicRSAKey))
                Console.WriteLine("Issue Hash Verified");
            else
                Console.WriteLine("Invalid Issue Hash");


            //Restore Coin
            coin.Holders.AddRange(holders);
            coin.HolderHash = hh;
            coin.IssuingHash = hash;


            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Transferring coin...");

            //Transfer Coin
            Transfer transfer = new Transfer() { Timestamp = DateTime.UtcNow, SerialNumber = coin.SerialNumber, Holders = new List<Holder>() };
            transfer.Holders.Add(new Holder() { PublicKey = transferPublicKey });
            string jTransfer = JsonConvert.SerializeObject(transfer);
            
            transfer.Signature = CoinTools.Sign(jTransfer, privateRSAKey);

            TransferCoin tc = new TransferCoin();
            coin = tc.Create(transfer, coin, privateRSAKey, publicRSAKey);

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Coin Transferred:");
            Console.WriteLine("Issuing Authority: " + coin.IssuingAuthority);
            Console.WriteLine("Issuing Country: " + coin.CountryCode);
            Console.WriteLine("Serial Number: " + coin.SerialNumber);
            Console.WriteLine("Value: " + coin.Value);
            Console.WriteLine("Issuing Date: " + coin.IssueDate);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Issuing Hash: " + coin.IssuingHash);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Holder's Public Key: " + Environment.NewLine + coin.Holders[0].PublicKey);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Holder Hash: " + coin.HolderHash);

            //Verify Holder Hash
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Verifying Holder Hash...");
            hh = coin.HolderHash;
            coin.HolderHash = null;
            jCoin = JsonConvert.SerializeObject(coin);

            if (CoinTools.Verify(jCoin, hh, publicRSAKey))
                Console.WriteLine("Holder Hash Verified");
            else
                Console.WriteLine("Invalid Holder Hash");


            //Verify Issue Hash
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Verifying Issue Hash...");
            holders = coin.Holders.ToArray();
            coin.Holders.Clear();
            hash = coin.IssuingHash;
            coin.IssuingHash = null;
            jCoin = JsonConvert.SerializeObject(coin);

            if (CoinTools.Verify(jCoin, hash, publicRSAKey))
                Console.WriteLine("Issue Hash Verified");
            else
                Console.WriteLine("Invalid Issue Hash");

            Console.ReadLine();
        }
    }
}
