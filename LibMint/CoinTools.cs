using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.IO;
using System.Text;

namespace LibMint
{
    public static class CoinTools
    {
        public static string Sign(string signature, string privateKey)
        {
            try
            {
                byte[] r = Encoding.UTF8.GetBytes(signature);

                StringReader stringReader = new StringReader(privateKey);
                PemReader pemReader = new PemReader(stringReader);
                AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair)pemReader.ReadObject();
                RsaKeyParameters privateRSAKey = (RsaKeyParameters)keyPair.Private;


                ISigner sig = SignerUtilities.GetSigner("Sha512WithRsa");
                sig.Init(true, privateRSAKey);
                sig.BlockUpdate(r, 0, r.Length);
                byte[] signedBytes = sig.GenerateSignature();

                return Convert.ToBase64String(signedBytes);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
