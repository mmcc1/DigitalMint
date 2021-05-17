using LibMint;
using LibMintDBContext;
using LibMintDBContext.Tables;
using LibMintModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class SearchController : ControllerBase
    {
        [HttpPost(Name = "SearchCoin")]
        public async Task<IActionResult> SearchCoin(Search search)
        {
            string sig = search.Signature;
            search.Signature = null;

            if (!CoinTools.ECVerify(JsonConvert.SerializeObject(search), sig, search.PublicKey))
                return NotFound();

            MintDBContext db = new MintDBContext();

            List<Guid> mc = await db.MintHolders.Where(x => x.PublicKey == search.PublicKey).Select(x => x.MintCoinId).ToListAsync();
            List<MintCoin> mccoins = new List<MintCoin>();
            List<Coin> coins = new List<Coin>();

            for (int i = 0; i < mc.Count; i++)
            {
                mccoins.Add(await db.MintCoins.Where(x => x.Id == mc[i]).Include(x => x.Holders).SingleAsync());
            }

            for (int i = 0; i < mccoins.Count; i++)
            {
                Coin coin = new Coin();
                coin.CountryCode = mccoins[i].CountryCode;
                coin.CurrencyCode = mccoins[i].CurrencyCode;
                coin.PurposeCode = mccoins[i].PurposeCode;
                coin.HolderHash = mccoins[i].HolderHash;
                coin.Holders = new List<Holder>();
                coin.IssueDate = mccoins[i].IssueDate;
                coin.IssuingAuthority = mccoins[i].IssuingAuthority;
                coin.IssuingHash = mccoins[i].IssuingHash;
                coin.SerialNumber = mccoins[i].SerialNumber;
                coin.Value = mccoins[i].Value;

                for(int j = 0; j < mccoins[i].Holders.Count; j++)
                {
                    Holder holder = new Holder();
                    holder.PublicKey = mccoins[i].Holders[j].PublicKey;

                    coin.Holders.Add(holder);
                }

                coins.Add(coin);
            }
            
            return Ok(coins);
        }
    }
}
