using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Options.Helper
{
    //
    // Summary:
    //     Specifies the type of a parameter within a query relative to the System.Data.DataSet.
    public enum ParameterDirection
    {
        //
        // Summary:
        //     The parameter is an input parameter.
        Input = 1,
        //
        // Summary:
        //     The parameter is an output parameter.
        Output = 2,
        //
        // Summary:
        //     The parameter is capable of both input and output.
        InputOutput = 3,
        //
        // Summary:
        //     The parameter represents a return value from an operation such as a stored procedure,
        //     built-in function, or user-defined function.
        ReturnValue = 6
    }
}
