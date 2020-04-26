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
        static CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        static CancellationToken token = cancelTokenSource.Token;

        static void Main(string[] args)
        {
            string UserName = GetUserName();

            

            Task.Factory.StartNew(new Action(()=> 
            {
                using (NamedPipeClientStream np_client = new NamedPipeClientStream(UserName))
                {
                    try
                    {
                        np_client.Connect(2000);
                        using (StreamWriter sr = new StreamWriter(np_client))
                        {

                            sr.WriteLine("");
                           
                        }

                    }
                    catch
                    {
                    }
                }

                cancelTokenSource.Cancel();
            }));

            for (int i = 0; i < 5; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Environment.Exit(0);
                }
                Thread.Sleep(1000);
            }
            Environment.Exit(0);
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
