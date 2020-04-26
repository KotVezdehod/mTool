using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace tKiller
{
    class Program
    {
        static void Main(string[] args)
        {
            string UserName = GetUserName();

            Task.Factory.StartNew(new Action(()=> 
            {
                using (NamedPipeClientStream np_client = new NamedPipeClientStream(UserName))
                {
                    using (StreamWriter sr = new StreamWriter(np_client))
                    {
                        np_client.Connect(2000);
                        sr.WriteLine("");
                    }
                }

                Environment.Exit(0);
            }));

            Console.ReadLine();
        }


        static string GetUserName()
        {
            string UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string[] splitter = { "\\" };
            string[] str_arr_username = UserName.Split(splitter, StringSplitOptions.RemoveEmptyEntries);

            return str_arr_username[1];

        }
    }
}
