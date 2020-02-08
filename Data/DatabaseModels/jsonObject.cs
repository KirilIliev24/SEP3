using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DatabaseModels
{
    public class jsonObject
    {
        public string functionName { get; set; }
        public string argument { get; set; }
        public jsonObject(string functionName, string argument)
        {
            this.functionName = functionName;
            this.argument = argument;
        }
    }
}
