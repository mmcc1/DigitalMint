using LibMintModels;
using Newtonsoft.Json;

namespace LibMint
{
    public class TransferCoin
    {
        public Coin Create(Transfer transfer, Coin coin, string privateRSAKey)
        {
            string sig = transfer.Signature;
            transfer.Signature = null;
            string jTransfer = JsonConvert.SerializeObject(transfer);
            bool canTransfer = false;

            for (int i = 0; i < coin.Holders.Count; i++)
            {
                if (CoinTools.ECVerify(jTransfer, sig, coin.Holders[i].PublicKey))
                    canTransfer = true;
            }

            if (canTransfer)
            {
                //Perform transfer
                coin.Holders = transfer.Holders;
                coin.HolderHash = null;
                string jCoin = JsonConvert.SerializeObject(coin);
                coin.HolderHash = CoinTools.ECSign(jCoin, privateRSAKey);
            }

            return coin;
        }
    }
}
