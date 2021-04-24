using System;
using LibMint;
using LibMintModels;

namespace MintTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Issuing coin...");
            IssueCoin iC = new IssueCoin();
            Coin coin = iC.Create("Bank of Digital", "UK", 10.00M);

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Coin issued:");
            Console.WriteLine("Issuing Authority: " + coin.IssuingAuthority);
            Console.WriteLine("Serial Number: " + coin.SerialNumber);
            Console.WriteLine("Value: " + coin.Value);
            Console.WriteLine("Issuing Date: " + coin.IssueDate);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Issuing Hash: " + coin.IssuingHash);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Holder's Public Key: " + Environment.NewLine + coin.Holders[0].PublicKey);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Holder Hash: " + coin.HolderHash);

            Console.ReadLine();
        }
    }
}
