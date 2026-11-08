using System;
using System.Security.Cryptography;
using System.Text;

namespace gxViewer.SGR.Services
{
    public static class GxCrypto
    {
        // Mismos valores del JS
        private static readonly byte[] Des = HexToBytes(
            "000102030405060708093A0B0C0D0E0F101112131415161718191A1B1C1D1E1F");

        private static readonly byte[] Cab = HexToBytes(
            "202122232425262728592A2B2C2D2E2F");

        private const string Password =
            "032nf02nf0saifnaief0aiseganhiramhia0hrnai0hnairdmhid0am";

        /// <summary>
        /// Descifra el pSource (hex) usando el iv del JSON y el iv del servidor.
        /// Equivalente a fnCiGX(oet, vi) en el JS.
        /// </summary>
        public static string DescifrarSource(string pSourceHex, string ivJsonHex, string ivServidorHex)
        {
            // 1) Cifrar el password con AES(des, cab) — igual que CryptoJS.AES.encrypt(password, des, {iv:cab})
            byte[] keyDerivada = AesEncryptPassword();

            // 2) La "clave" real del descifrado es el iv del servidor (data[0].iv)
            byte[] key = HexToBytes(ivServidorHex);

            // 3) El IV del descifrado es el iv que vino en el JSON
            byte[] iv = HexToBytes(ivJsonHex);

            // 4) Descifrar pSourceHex
            byte[] cipher = HexToBytes(pSourceHex);
            return AesDecrypt(cipher, key, iv);
        }

        private static byte[] AesEncryptPassword()
        {
            byte[] pass = Encoding.UTF8.GetBytes(Password);
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Key = Des;
                aes.IV = Cab;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                using (var enc = aes.CreateEncryptor())
                    return enc.TransformFinalBlock(pass, 0, pass.Length);
            }
        }

        private static string AesDecrypt(byte[] cipher, byte[] key, byte[] iv)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                using (var dec = aes.CreateDecryptor())
                {
                    byte[] plain = dec.TransformFinalBlock(cipher, 0, cipher.Length);
                    return Encoding.UTF8.GetString(plain);
                }
            }
        }

        public static byte[] HexToBytes(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex)) return new byte[0];
            hex = hex.Replace(" ", "").Replace("\r", "").Replace("\n", "");
            int len = hex.Length / 2;
            byte[] b = new byte[len];
            for (int i = 0; i < len; i++)
                b[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            return b;
        }
    }
}