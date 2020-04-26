using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace mTool
{
    public static class Shutter
    {

        static NamedPipeServerStream cp_server;

        static public void StartWaiter(string username)
        {

            Task.Factory.StartNew(new Action(() =>
                {
                    try
                    {
                        using (cp_server = new NamedPipeServerStream(username))
                        {
                            cp_server.WaitForConnection();
                            using (StreamReader reader = new StreamReader(cp_server))
                            {
                                var line = reader.ReadLine();
                                StartShutter(username);
                                //MessageBox.Show("Shutter");
                            }
                        }
                    }
                    catch
                    {
                        Environment.Exit(0);
                    }

                }));

        }

        static public void StartShutter(string username)
        {

            bool ExplorerPresents = false;
            bool c1Presents = false;

            while (true)
            {
                ExplorerPresents = false;
                c1Presents = false;

                ExplorerPresents = GetOner(username, "EXPLORER.EXE");

                //MessageBox.Show(ExplorerPresents.ToString());

                c1Presents = GetOner(username, "1CV8.EXE") || GetOner(username, "1CV8C.EXE") || GetOner(username, "1CV8S.EXE");

                //MessageBox.Show(c1Presents.ToString());

                if (!ExplorerPresents && !c1Presents)
                {
                    try
                    {
                        cp_server.Close();
                    }
                    catch { }

                    System.Diagnostics.Process.Start("Shutdown.exe", "-l");

                    Environment.Exit(0);
                }

                Thread.Sleep(500);

            };

        }

        static public bool GetOner(string owner, string proc_name)
        {
            string query = "Select * From Win32_Process Where Name = '" + proc_name + "'";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection processList = searcher.Get();

            foreach (ManagementObject obj in processList)
            {
                string[] argList = new string[] { string.Empty, string.Empty };
                int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
                if (returnVal == 0)
                {
                    if (argList[0].ToUpper() == owner.ToUpper()) return true;
                }
            }
            return false;
        }

        public static string GetUserName()
        {
            string UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string[] splitter = { "\\" };
            string[] str_arr_username = UserName.Split(splitter, StringSplitOptions.RemoveEmptyEntries);

            return str_arr_username[1];

        }

    }
}
