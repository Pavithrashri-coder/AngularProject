using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Options.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DBParameterAttribute : System.Attribute
    {
        public string? ParameterType { get; set; }

        public ParameterDirection ParameterDirection { get; set; } = ParameterDirection.Input;
        public int Size { get; set; }
    }
}
