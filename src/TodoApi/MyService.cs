using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApi
{
    public class MyService : IMyService
    {
        public string GetValue()
        {
            return "This is the value from MyService";
        }
    }
}
