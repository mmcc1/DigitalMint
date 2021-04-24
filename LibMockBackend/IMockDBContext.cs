using LibMintModels;
using System;

namespace LibMockBackend
{
    public interface IMockDBContext
    {
        void AddCoin(Coin coin);
        Coin GetCoin(Guid serialNumber);
        void ResetDB();
    }
}