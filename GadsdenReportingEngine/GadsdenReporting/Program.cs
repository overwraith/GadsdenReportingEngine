using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GadsdenReporting.Models;

namespace GadsdenReporting {
    class Program {
        static void Main(string[] args) {
            FtpCredential credential = new FtpCredential();
            credential.SetPassword("HelloWorld");
            String password = credential.GetPassword();
        }
    }//end class
}//end namespace
