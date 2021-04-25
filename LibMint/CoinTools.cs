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
        public static string RSASign(string toBeSigned, string privateKey)
        {
            try
            {
                byte[] r = Encoding.UTF8.GetBytes(toBeSigned);

                PemReader pemReader = new PemReader(new StringReader(privateKey));
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

        public static bool RSAVerify(string signedData, string signature, string publicKey)
        {
            try
            {
                byte[] r = Convert.FromBase64String(signature);
                byte[] s = Encoding.UTF8.GetBytes(signedData);

                PemReader pemReader = new PemReader(new StringReader(publicKey));
                AsymmetricKeyParameter pKey = (AsymmetricKeyParameter)pemReader.ReadObject();
                RsaKeyParameters publicRSAKey = (RsaKeyParameters)pKey;

                ISigner sig = SignerUtilities.GetSigner("Sha512WithRsa");
                sig.Init(false, publicRSAKey);
                sig.BlockUpdate(s, 0, s.Length);
                return sig.VerifySignature(r);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ECSign(string toBeSigned, string privateKey)
        {
            try
            {
                byte[] r = Encoding.UTF8.GetBytes(toBeSigned);

                PemReader pemReader = new PemReader(new StringReader(privateKey));
                AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair)pemReader.ReadObject();
                ECKeyParameters privateRSAKey = (ECKeyParameters)keyPair.Private;

                ISigner sig = SignerUtilities.GetSigner("Sha512WithECDSA");
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

        public static bool ECVerify(string signedData, string signature, string publicKey)
        {
            try
            {
                byte[] r = Convert.FromBase64String(signature);
                byte[] s = Encoding.UTF8.GetBytes(signedData);

                PemReader pemReader = new PemReader(new StringReader(publicKey));
                AsymmetricKeyParameter pKey = (AsymmetricKeyParameter)pemReader.ReadObject();
                ECKeyParameters publicRSAKey = (ECKeyParameters)pKey;

                ISigner sig = SignerUtilities.GetSigner("Sha512WithECDSA");
                sig.Init(false, publicRSAKey);
                sig.BlockUpdate(s, 0, s.Length);
                return sig.VerifySignature(r);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
