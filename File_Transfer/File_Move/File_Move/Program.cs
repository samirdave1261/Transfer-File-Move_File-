using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Principal;
using System.Net.Sockets;
using System.Configuration;
using static File_Move.Role_Creditial;
using System.Net;

namespace File_Move
{
    //Lan based Data Transfer >> One Server to another Server
    //Developed By:-SAMIR DAVE
    class Program
    {
        static void Main() 
        {
            try
            {
                Role_Creditial obj_impesonat = new Role_Creditial();
                WindowsImpersonationContext impersonate = null;
                var _MachineName = Environment.UserDomainName;

                Console.WriteLine("Enter File Path:");
                string Sender_File = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Enter Receiver Folder Name:");
                string Receiver_File_Path = $@"\{Convert.ToString(Console.ReadLine())}\";
                Console.WriteLine($"Select Machine :-- {ConfigurationManager.AppSettings["Machine_users"]}");
                int Machine = Convert.ToInt32(Console.ReadLine());

                string _FileName = string.Empty;
                string file = string.Empty;
                string New_Filepath = string.Empty;
                FileInfo _file = new FileInfo(Sender_File);
                if (Directory.Exists(_file.DirectoryName))
                {
                    _FileName = _file.Name;
                }
                impersonate = obj_impesonat.ImpersonateFileServer(Machine);
                New_Filepath = obj_impesonat.Domain + Receiver_File_Path;

                if (!Directory.Exists(New_Filepath))
                {
                    Directory.CreateDirectory(New_Filepath);
                    if (Directory.Exists(New_Filepath))
                    {
                        New_Filepath += _FileName;
                        Console.WriteLine("-----------File Path Created SucessFully--------");
                    }
                }
                else if (Directory.Exists(New_Filepath))
                {
                    New_Filepath += '1';
                    Directory.CreateDirectory(New_Filepath);
                    if (Directory.Exists(New_Filepath))
                    {
                        New_Filepath += _FileName;
                        Console.WriteLine("-----------File Path Created SucessFully---------");
                    }
                }
                if (impersonate != null)
                {
                    Console.WriteLine("====>File Send From====>'" + Sender_File + "'====>TO<==== '" + New_Filepath + "'<==== ");
                    File.Copy(Sender_File, New_Filepath);
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("File Transfer Suceessfully");
                    Console.WriteLine("------------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
