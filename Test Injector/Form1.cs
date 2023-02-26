using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Test_Injector
{

    public partial class DLLInjector : Form
    {
        public DLLInjector()
        {
            InitializeComponent();

            Process[] PC = Process.GetProcesses().Where(p => (long)p.MainWindowHandle != 0).ToArray();//Tweak this for all processes
            ProcessListComboBox.Items.Clear();
            foreach (Process p in PC)
            {
                ProcessListComboBox.Items.Add(p.ProcessName);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private static string DLLPath { get; set; }//Getter and Setter for DLLPath variable

        private void BrowseToDLLButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = @"C:\";
                
                ofd.Title = "Locate DLL to inject into the selected process.";
                ofd.DefaultExt = "dll";
                ofd.Filter = "DLL Files (*.dll)|*.dll";//filter only dll files
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                ofd.ShowDialog();

                DLLPathTextBox.Text = ofd.FileName;
                DLLPath = ofd.FileName;
                Console.WriteLine(DLLPath);//This can be removed.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefreshProcessListButton_Click(object sender, EventArgs e)
        {
            Process[] PC = Process.GetProcesses().Where(p => (long)p.MainWindowHandle != 0).ToArray();//This needs to be changed to show all processes.  Or you can type it in minus the file extension.
            ProcessListComboBox.Items.Clear();
            foreach (Process p in PC)
            {
                ProcessListComboBox.Items.Add(p.ProcessName);
            }
        }

        static readonly IntPtr INTPTR_ZERO = (IntPtr)0;
        // privileges
        const int PROCESS_CREATE_THREAD = 0x0002;
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int PROCESS_VM_OPERATION = 0x0008;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_READ = 0x0010;
        // used for memory allocation
        const uint MEM_COMMIT = 0x00001000;
        const uint MEM_RESERVE = 0x00002000;
        const uint PAGE_READWRITE = 4;

        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, uint size, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess,
            IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        public static int Inject(string ProcName, string DLLPath)
        {
            //  If DLL is not found, return 1.
            if (!File.Exists(DLLPath)) { return 1; } 

            uint _procId = 0;
            Process[] _procs = Process.GetProcesses();
            for (int i = 0; i < _procs.Length; i++)
            {
                if (_procs[i].ProcessName == ProcName) 
                {
                    _procId = (uint)_procs[i].Id;
                    break;
                }
            }
            if (_procId == 0) { return 2; }//process not found
            if (!StartInjection(_procId, DLLPath)) { return 3; }//If StartInjection() fails, return 3.
            return 4; //success
        }



        /// <summary>
        /// START INJECTION FUNCTION
        /// ########################
        /// Returns false if any part of the injection process fails.  Otherwise, returns true.
        /// References: 
        ///     https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-openprocess
        ///     https://www.pinvoke.net/default.aspx/kernel32.openprocess
        ///     https://learn.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-loadlibrarya
        ///     https://learn.microsoft.com/en-us/windows/win32/api/memoryapi/nf-memoryapi-virtualallocex
        ///     https://learn.microsoft.com/en-us/windows/win32/Memory/memory-protection-constants
        ///     https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-createremotethread
        ///     https://learn.microsoft.com/en-us/windows/win32/api/memoryapi/nf-memoryapi-writeprocessmemory
        ///     https://learn.microsoft.com/en-us/windows/win32/api/memoryapi/nf-memoryapi-readprocessmemory
        ///     https://learn.microsoft.com/en-us/windows/win32/debug/system-error-codes
        /// </summary>
        /// <param name="PIDtoInject"></param>
        /// <param name="DLLPath"></param>
        /// <returns></returns>
        public static bool StartInjection(uint PIDtoInject, string DLLPath)
        {
            // Get handle of the process - with required privileges
            Console.WriteLine("Getting handle to process...");
            IntPtr processHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, PIDtoInject);
            if (processHandle == INTPTR_ZERO) { return false; }
            Console.WriteLine("Process Handle acquired: " + processHandle.ToString());

            // Allocate memory for dll path and store pointer
            Console.WriteLine("Getting loadlibrary pointer...");
            IntPtr LoadLibrary_lpAddress = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            if (LoadLibrary_lpAddress == INTPTR_ZERO) { return false; }
            Console.WriteLine("Loadlibrary pointer: " + LoadLibrary_lpAddress);

            // Allocate memory for dll path and store pointer
            Console.WriteLine("Allocating memory...");
            IntPtr allocMemAddress = VirtualAllocEx(processHandle, INTPTR_ZERO, (uint)((DLLPath.Length + 1) * Marshal.SizeOf(typeof(char))), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
            Console.WriteLine("GetLastError code for VirtualAllocEx: " + GetLastError());
            if (allocMemAddress == INTPTR_ZERO) { return false; }
            Console.WriteLine("allocMemAddress: " + allocMemAddress);

            // array of bytes containing DLL path
            byte[] bytes = Encoding.Default.GetBytes(DLLPath);

            // Write path of dll to memory.
            Console.WriteLine("Writing content to memory...");
            UIntPtr bytesWritten;
            bool resp1 = WriteProcessMemory(processHandle, allocMemAddress, bytes, (uint)((DLLPath.Length + 1) * Marshal.SizeOf(typeof(char))), out bytesWritten);
            Console.WriteLine("GetLastError code for WriteProcessMemory: " + GetLastError());
            if (resp1 == false) { return false; }

            //  check for contents in memory to verify
            int bytesRead = 0;//declare variable to put the return in.
            byte[] buffer = new byte[(DLLPath.Length) * Marshal.SizeOf(typeof(char))];//set size of buffer to whatever the length of the DLL path so it can be of appropriate size to hold the data.
            Console.WriteLine("Reading content from memory...");
            bool resp2 = ReadProcessMemory(processHandle, allocMemAddress, buffer, buffer.Length, ref bytesRead);//read what is in memory to verify.
            Console.WriteLine("GetLastError code for ReadProcessMemory: " + GetLastError());
            Console.WriteLine("Data in memory: " + System.Text.Encoding.UTF8.GetString(buffer));
            if (resp2 == false) { return false; }

            // Create a thread that will call LoadLibraryA with allocMemAddress as argument
            IntPtr respCreateRemoteThread;
            Console.WriteLine("CreateRemoteThread executing...");
            respCreateRemoteThread = CreateRemoteThread(processHandle, INTPTR_ZERO, 0, LoadLibrary_lpAddress, allocMemAddress, 0, INTPTR_ZERO);
            Console.WriteLine("Handle for created process: " + respCreateRemoteThread.ToString());
            Console.WriteLine("GetLastError code for CreateRemoteThread: " + GetLastError());
            if (GetLastError() > 0) { return false; }//
            
            
            CloseHandle(processHandle);

            return true;
        }



        private void InjectButton_Click(object sender, EventArgs e)
        {
            int Result = Inject(ProcessListComboBox.Text, DLLPath);
            if (Result == 1) { MessageBox.Show("DLL File Does Not Exist."); }
            else if (Result == 2) { MessageBox.Show("Process Does Not Exist"); }
            else if (Result == 3) { MessageBox.Show("Injection Failed.  Check the GetLastError() code in debug."); }
            else if (Result == 4) { MessageBox.Show("#BASIC injector successful.  Check for loaded module in target process."); }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://claytonvantol.com");
        }
    }
}
