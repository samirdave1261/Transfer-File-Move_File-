using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace File_Move
{
    class File_read
    {
        static void file_Util()
        {
            //Create an object of FileInfo for specified path            
            FileInfo fi = new FileInfo(@"D:\Dave Tricks\samir.txt");

            //Open a file for Read\Write
            FileStream fs = fi.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);

            //Create an object of StreamReader by passing FileStream object on which it needs to operates on
            StreamReader sr = new StreamReader(fs);

            //Use the ReadToEnd method to read all the content from file
            string fileContent = sr.ReadToEnd();

            //Close the StreamReader object after operation
            sr.Close();
            fs.Close();
        }
    }
}
