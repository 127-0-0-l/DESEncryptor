using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DESEncryptor
{
    public static class DES
    {
        public static string Encrypt(string code, string Key)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = null;

                inputByteArray = Encoding.Default.GetBytes(code);
                des.Key = Encoding.Default.GetBytes(Key);
                des.IV = Encoding.Default.GetBytes(Key);

                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                cs.Close();
                des.Clear();
                ms.Close();
                StringBuilder ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                return ret.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string Decrypt(string code, string Key)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = new byte[code.Length / 2];
                for (int x = 0; x < code.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(code.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }

                des.Key = Encoding.Default.GetBytes(Key);
                des.IV = Encoding.Default.GetBytes(Key);

                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                cs.Close();
                des.Clear();
                ms.Close();
                string res;
                res = Encoding.Default.GetString(ms.ToArray());
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
