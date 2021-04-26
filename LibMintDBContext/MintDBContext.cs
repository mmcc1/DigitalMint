using LibMintDBContext.Tables;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibMintDBContext
{
    public class MintDBContext : DbContext
    {
        public DbSet<MintCoin> MintCoins { get; set; }
        public DbSet<MintHolder> MintHolders { get; set; }
        public DbSet<MintTransfer> MintTransfers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=127.0.0.1;Database=LibMint;User Id=libmint;Password=devpassword11;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
