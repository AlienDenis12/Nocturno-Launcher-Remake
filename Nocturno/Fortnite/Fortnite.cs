using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Nocturno
{
    class Fortnite
    {

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(nint h, string m, string c, int type);

        public static void Inject(int pid, string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    Thread.Sleep(3000);
                    Environment.Exit(0);
                    return;
                }

                nint hProcess = Win32.OpenProcess(1082, false, pid);
                nint procAddress = Win32.GetProcAddress(Win32.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                uint num = checked((uint)((path.Length + 1) * Marshal.SizeOf(typeof(char))));
                nint intPtr = Win32.VirtualAllocEx(hProcess, 0, num, 12288U, 4U);
                nuint uintPtr;
                Win32.WriteProcessMemory(hProcess, intPtr, Encoding.Default.GetBytes(path), num, out uintPtr);
                Win32.CreateRemoteThread(hProcess, 0, 0U, procAddress, intPtr, 0U, 0);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);

            }
        }



        public static async Task DownloadFIle(string url, string destinationPath)
        {
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(url, destinationPath);
            }
        }

        public static void Launch(string path, string email, string password)
        {
            try
            {
                string Appdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Nocturno";
                string Paks = Properties.Settings.Default.gamePath + "\\FortniteGame\\Content\\Paks";

                if (!Directory.Exists(Appdata))
                {
                    Directory.CreateDirectory(Appdata);
                }

                if (!File.Exists(Appdata + "\\FortniteClient-Win64-Shipping_BE.exe"))
                {
                    DownloadFIle("http://104.194.10.244:3550/download/FortniteClient-Win64-Shipping_BE.exe", Appdata + "\\FortniteClient-Win64-Shipping_BE.exe");
                }
                if (!File.Exists(Appdata + "\\FortniteLauncher.exe"))
                {
                    DownloadFIle("http://104.194.10.244:3550/download/FortniteLauncher.exe", Appdata + "\\FortniteLauncher.exe");
                }
                if (!File.Exists(Appdata + "\\Redirect.dll"))
                {
                    DownloadFIle("http://104.194.10.244:3550/download/Redirect.dll", Appdata + "\\Redirect.dll");
                }


                string FortniteLauncher = "FortniteLauncher.exe";
                string FortniteClient = "FortniteClient-Win64-Shipping_BE.exe";

                Process[] runningProcesses = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(FortniteLauncher));
                if (runningProcesses.Length == 0)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = Path.Combine(Appdata, FortniteLauncher),
                        CreateNoWindow = true,
                        UseShellExecute = false
                    });
                }

                runningProcesses = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(FortniteClient));
                if (runningProcesses.Length == 0)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = Path.Combine(Appdata, FortniteClient),
                        CreateNoWindow = true,
                        UseShellExecute = false
                    });
                }

                
                ProcessStartInfo startInfo;

                startInfo = new ProcessStartInfo
                {
                    FileName = path + "\\FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe",
                    Arguments = $"-AUTH_LOGIN={email} -AUTH_PASSWORD={password} -AUTH_TYPE=epic -epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -skippatchcheck -nobe -fltoken=3db3ba5dcbd2e16703f3978d -caldera=eyJhbGciOiJFUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2NvdW50X2lkIjoiYmU5ZGE1YzJmYmVhNDQw7DBjnzDnXyyEnX7OljJm-j2d88G_WgwQ9wrE6lwMEHZHjBd1ISJdUO1UVUqkfLdU5nofBQ -fromfl=eac",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                Process proc = new Process
                {
                    StartInfo = startInfo
                };

                proc.Start();

                Inject(proc.Id, Path.Combine(Appdata, "Redirect.dll"));

            }
            catch (Exception ex)
            {
                MessageBox(IntPtr.Zero, ex.Message, "Error", 0);
            }
        }
    }
}
