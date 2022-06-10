using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlRawSpTestApp.Parameters
{
    public interface IParameter
    {
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
        public Type ParameterType { get; set; }
        public string Operator { get; set; }
    }
}
