using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class MobileProviderEventArgs
    {

        public MobileProviderEventArgs(string message)
        {
            Message = message;
        }

        public string Message;

    }
}