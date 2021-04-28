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
MHQCAQEEIFsl9XUq6Z6xnx/HNiKtUr8+8g/Ym2RtMns09Q9Z2JENoAcGBSuBBAAK
oUQDQgAEJKZlnvyvg+F++rf6IcM/wT3W8Fyd0jt4foUUfjtwGHh1PllMoVY+dDBl
w3UIU+3iq36ZZ3gindq175Dpfcy17g==
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
