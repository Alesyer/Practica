using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Practica.WebAngular8.Util
{
    public class Encryption
    {
        private static byte[] key = { };
        private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private static string EncryptionKey = "78YiaLqErtE";

        public Encryption()
        {
        }

        public static string Decrypt(string Input)
        {
            Byte[] inputByteArray = new Byte[Input.Length];
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = null;
            Encoding encoding = Encoding.UTF8;
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 8));
                inputByteArray = Convert.FromBase64String(Input);
                cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return encoding.GetString(ms.ToArray());
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                ms.Dispose();
                cs.Dispose();
                inputByteArray = null;
                des = null;
                ms = null;
                cs = null;
            }
        }
        public static string Encrypt(string Input)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            Byte[] inputByteArray;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = null;
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes
                (EncryptionKey.Substring(0, 8));
                inputByteArray = Encoding.UTF8.GetBytes(Input);
                cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                ms.Dispose();
                cs.Dispose();
                inputByteArray = null;
                des = null;
                ms = null;
                cs = null;
            }
        }

        public static string Encriptar(string entrada)
        {
            entrada = Encryption.Encrypt(entrada);
            entrada = HttpUtility.UrlEncode(entrada);
            return entrada;
        }

        public static string Desencriptar(string entrada)
        {
            entrada = HttpUtility.UrlDecode(entrada);
            entrada = entrada.Replace(" ", "+");
            entrada = entrada.Replace("%3d", "=");
            entrada = Encryption.Decrypt(entrada);
            return entrada;
        }


        public static string EncriptarSHA256(string entrada)
        {
            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();
            Byte[] input = Encoding.UTF8.GetBytes(entrada);
            Byte[] hash = provider.ComputeHash(input);
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < hash.Length; ++i) output.Append(hash[i].ToString("x2").ToLower());
            return output.ToString();
        }
    }
}
