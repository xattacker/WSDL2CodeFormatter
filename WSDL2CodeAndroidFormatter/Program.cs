using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WSDL2Code
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo dir = new DirectoryInfo(".");
            CheckFiles(dir);

            System.Console.WriteLine("..... press any key to close.......");
            System.Console.ReadKey();
        }

        private static void CheckFiles(DirectoryInfo dir)
        {
            DirectoryInfo[] subs = dir.GetDirectories();
            if (subs != null && subs.Length > 0)
            {
                foreach (DirectoryInfo sub in subs)
                {
                    // check sub dir by recursive
                    CheckFiles(sub);
                }
            }

            Console.WriteLine("check folder: " + dir.Name);

            FileInfo[] files = dir.GetFiles("*.java", SearchOption.AllDirectories);
            if (files != null && files.Length > 0)
            {
                string keyword = @"    @Override
    public String getInnerText() {
        return null;
    }
    
    
    @Override
    public void setInnerText(String s) {
    }";
                string replaced = @"    public String getInnerText() {
        return null;
    }
    
    public void setInnerText(String s) {
    }";

                foreach (FileInfo file in files)
                {
                    string content = File.ReadAllText(file.FullName);
                    if (content.Contains(keyword))
                    {
                        content = content.Replace(keyword, replaced);
                        File.WriteAllText(file.FullName, content);
                        Console.WriteLine(file.Name + " was replaced.");
                    }
                }
            }
        }
    }
}
