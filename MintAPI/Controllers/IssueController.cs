using LibMint;
using LibMintDBContext;
using LibMintDBContext.Tables;
using LibMintModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MintAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class IssueController : ControllerBase
    {
        private string privateECKey = @"-----BEGIN EC PRIVATE KEY-----
MHQCAQEEILLwDgKayNil3RxMTtz6isgw9p3piPjzz7EuAqghwqsLoAcGBSuBBAAK
oUQDQgAEze2Mh0XVtkiZ03rt5L0xIcQAEfxf75qjEO8C9fbVsJh1VDuOdESkNIIf
0YlNAz2PJ9IQ0ovDf+pM4Yt/m2Bacw==
-----END EC PRIVATE KEY-----";


        [HttpPost(Name = "IssueCoin")]
        public async Task<IActionResult> IssueCoin(IssueCoinRequest issueCoinRequest)
        {
            IssueCoin iC = new IssueCoin();
            Coin coin = iC.Create(issueCoinRequest.IssuingAuthority, issueCoinRequest.CountryCode, issueCoinRequest.CurrencyCode, issueCoinRequest.Value, privateECKey, issueCoinRequest.HolderPublicKeys);
            MintDBContext db = new MintDBContext();

            MintCoin mc = new MintCoin()
            {
                CountryCode = coin.CountryCode,
                CurrencyCode = coin.CurrencyCode,
                HolderHash = coin.HolderHash,
                Id = Guid.NewGuid(),
                IssueDate = DateTime.UtcNow,
                IssuingAuthority = coin.IssuingAuthority,
                IssuingHash = coin.IssuingHash,
                SerialNumber = coin.SerialNumber,
                Value = coin.Value,
                Holders = new List<MintHolder>() 
            };

            for (int i = 0; i < coin.Holders.Count; i++)
            {
                mc.Holders.Add(new MintHolder() { Id = Guid.NewGuid(), PublicKey = coin.Holders[i].PublicKey });
            }

            await db.MintCoins.AddAsync(mc);
            await db.SaveChangesAsync();

            return Ok(coin);
        }
    }
}
