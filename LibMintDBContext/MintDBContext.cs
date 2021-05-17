using LibMintDBContext.Tables;
using Microsoft.EntityFrameworkCore;

namespace LibMintDBContext
{
    public class MintDBContext : DbContext
    {
        public DbSet<MintCoin> MintCoins { get; set; }
        public DbSet<MintHolder> MintHolders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=127.0.0.1;Database=LibMint;User Id=libmint;Password=dpassword@11;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
