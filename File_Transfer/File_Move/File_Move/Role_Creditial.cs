using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Principal;
using System.Data;
using System.Configuration;
using System.Web;
using System.Runtime.InteropServices;

namespace File_Move
{
    class Role_Creditial
    {
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)] // System DLL
        public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
        public string Domain  { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public WindowsImpersonationContext ImpersonateFileServer(int Flag)
        {
             int is_user = Flag;
             switch (is_user)
             {
                 case 1: //Vikalp
                    Domain   = @"\\192.168.1.131\xyz"; 
                    Username = "Administrator";
                    Password = "ispl123;";
                     break;
                 case 2: //Aditya
                     Domain   = @"\\192.168.1.210\Intellial\"; 
                     Username = "Admin";
                     Password = "ispl123;";
                     break;
                 case 3: //Chandan
                    Domain = @"\\DESKTOP-TEL8K46\Panel_intellial\"; 
                    Username = "admin";
                    Password = "admin";
                     break;
                case 4: //ankit
                    Domain = @"\\DESKTOP-PDQ262B\Demo_samir\";
                    Username = "ankit-qa";
                    Password = "ispl123;";
                    break;
                case 5 : //Default
                    Domain = @"\\192.168.1.120\Panel_Intellial\";
                    Username = "dev22";
                    Password = "ispl123;";
                    break;
            }
             IntPtr tokenHandle = new IntPtr(0);
             int dwLogonProvider = Convert.ToInt16(ConfigurationManager.AppSettings["LogonProvider"]);
             int dwLogonType =     Convert.ToInt16(ConfigurationManager.AppSettings["LogonType"]);
             bool returnValue = LogonUser(Username,Domain, Password, dwLogonType, dwLogonProvider, ref tokenHandle);
             WindowsIdentity ImpersonatedIdentity = new WindowsIdentity(tokenHandle);
             WindowsImpersonationContext MyImpersonation = ImpersonatedIdentity.Impersonate();
             return MyImpersonation;
        }

        public void savefileonserver(string folderpath, string fname,HttpPostedFileBase postedfile)
        {
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }
            postedfile.SaveAs(folderpath + @"\" + fname);
        }
    }
}
