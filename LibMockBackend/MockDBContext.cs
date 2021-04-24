using LibMintModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibMockBackend
{
    public class MockDBContext : IMockDBContext
    {
        private List<Coin> coins;

        public MockDBContext()
        {
            coins = new List<Coin>();
        }

        public void AddCoin(Coin coin)
        {
            coins.Add(coin);
        }

        public Coin GetCoin(Guid serialNumber)
        {
            return coins.Where(x => x.SerialNumber == serialNumber).Single();
        }

        public void ResetDB()
        {
            coins.Clear();
        }
    }
}
