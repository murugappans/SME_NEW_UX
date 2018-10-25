using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace IRAS
{
    public class encrypt
    {
        private static string m_strKey = "1234567897863427";
        private static string m_strIV = "9786342712345678";

        public encrypt()
        {
            m_strKey = "1234567897863427";
            m_strIV = "9786342712345678";
        }
        // public string m_strKey = "1234567897863427";
        //public string m_strIV = "9786342712345678";
        public static string SyDecrypt(string str)
        {
            string s = "";
            try
            {
                //int i;                
                //---converts the encrypted string into a byte array--- 
                byte[] b = stringToByteArray(str);
                //---converts the key and IV from string to byte array--- 
                byte[] key = Encoding.ASCII.GetBytes(m_strKey);
                byte[] IV = Encoding.ASCII.GetBytes(m_strIV);

                //---converts the byte array into a memory stream for decryption--- 
                MemoryStream memStream = new MemoryStream(b);
                RijndaelManaged RMCrypto = new RijndaelManaged();
                CryptoStream CryptStream = new CryptoStream(memStream, RMCrypto.CreateDecryptor(key, IV), CryptoStreamMode.Read);

                //---decrypting the stream--- 
                StreamReader SReader = new StreamReader(CryptStream);
                s = SReader.ReadToEnd();
                //---converts the descrypted stream into a string--- 
                s.ToString();
                SReader.Close();
            }
            catch (Exception err)
            {
                throw err;
                //Interaction.MsgBox(err.ToString);
            }
            return s;
        }

        public static string SyEncrypt(string str)
        {
            MemoryStream memStream = new MemoryStream();
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            try
            {
                //---converts the key and IV from string to byte array--- 
                byte[] key = Encoding.ASCII.GetBytes(IRAS.encrypt.m_strKey);
                byte[] IV = Encoding.ASCII.GetBytes(IRAS.encrypt.m_strIV);

                //---creates a new instance of the RijndaelManaged class--- 
                RijndaelManaged RMCrypto = new RijndaelManaged();

                //---creates a new instance of the CryptoStream class--- 
                CryptoStream CryptStream = new CryptoStream(memStream, RMCrypto.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                StreamWriter SWriter = new StreamWriter(CryptStream);

                //---encrypting the string--- 
                SWriter.Write(str);
                SWriter.Close();
                CryptStream.Close();

                //---converts the encrypted stream into a string--- 

                byte[] b;
                b = memStream.ToArray();
                int i;
                for (i = 0; i <= b.Length - 1; i++)
                {
                    if (i != b.Length - 1)
                    {
                        s.Append(b[i] + " ");
                    }
                    else
                    {
                        s.Append(b[i]);
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
                //Interaction.MsgBox(err.ToString);
            }
            return s.ToString();

        }

        private static byte[] stringToByteArray(string str)
        {
            //e.g. "1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16" to {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16} 
            string[] s;
            s = str.Split(' ');
            byte[] b = new byte[s.Length];
            int i;
            for (i = 0; i <= s.Length - 1; i++)
            {
                b[i] = Convert.ToByte(s[i]);
            }
            return b;
        }

    }

}



