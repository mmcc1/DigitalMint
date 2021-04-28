using LibMint;
using LibMintDBContext;
using LibMintDBContext.Tables;
using LibMintModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class TransferController : ControllerBase
    {
        private string privateECKey = @"-----BEGIN EC PRIVATE KEY-----
MHQCAQEEIFsl9XUq6Z6xnx/HNiKtUr8+8g/Ym2RtMns09Q9Z2JENoAcGBSuBBAAK
oUQDQgAEJKZlnvyvg+F++rf6IcM/wT3W8Fyd0jt4foUUfjtwGHh1PllMoVY+dDBl
w3UIU+3iq36ZZ3gindq175Dpfcy17g==
-----END EC PRIVATE KEY-----";

        [HttpPost(Name = "TransferCoin")]
        public async Task<IActionResult> TransferCoin(Transfer transfer)
        {
            Coin coin = new Coin();
            MintDBContext db = new MintDBContext();

            MintCoin mc = db.MintCoins.Where(x => x.SerialNumber == transfer.SerialNumber).Include(x => x.Holders).Single();

            coin.CountryCode = mc.CountryCode;
            coin.CurrencyCode = mc.CurrencyCode;
            coin.HolderHash = mc.HolderHash;
            coin.IssueDate = mc.IssueDate;
            coin.IssuingAuthority = mc.IssuingAuthority;
            coin.IssuingHash = mc.IssuingHash;
            coin.SerialNumber = mc.SerialNumber;
            coin.Value = mc.Value;
            coin.Holders = new List<Holder>();

            for (int i = 0; i < mc.Holders.Count; i++)
            {
                coin.Holders.Add(new Holder() { PublicKey = mc.Holders[i].PublicKey });
            }

            TransferCoin tc = new TransferCoin();
            Coin transferredCoin = tc.Create(transfer, coin, privateECKey);

            if (transferredCoin != null)
            {
                for(int i = 0; i < mc.Holders.Count; i++)
                {
                    db.MintHolders.Remove(mc.Holders[i]);
                    await db.SaveChangesAsync();
                }

                db.MintCoins.Remove(mc);
                await db.SaveChangesAsync();
            }

            MintCoin transferredMc = new MintCoin()
            {
                CountryCode = transferredCoin.CountryCode,
                CurrencyCode = transferredCoin.CurrencyCode,
                HolderHash = transferredCoin.HolderHash,
                Id = Guid.NewGuid(),
                IssueDate = DateTime.UtcNow,
                IssuingAuthority = transferredCoin.IssuingAuthority,
                IssuingHash = transferredCoin.IssuingHash,
                SerialNumber = transferredCoin.SerialNumber,
                Value = transferredCoin.Value,
                Holders = new List<MintHolder>()
            };

            for (int i = 0; i < coin.Holders.Count; i++)
            {
                transferredMc.Holders.Add(new MintHolder() { Id = Guid.NewGuid(), PublicKey = transferredCoin.Holders[i].PublicKey });
            }

            await db.MintCoins.AddAsync(transferredMc);
            await db.SaveChangesAsync();

            return Ok(transferredCoin);
        }
    }
}
