using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    //Handles URL queries
    public class HTTPQuery
    {
        QueryParameter param;
        string value;

        public HTTPQuery(QueryParameter queryParam, string queryValue)
        {
            param = queryParam;
            value = queryValue;
        }

        //Unimplemented types will fail
        //Maybe make this throw an error of some sort if false?
        public bool isValid()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a string formatted to be appended to a URL.
        /// </summary>
        /// <returns>URI ready string</returns>
        public override string ToString()
        {
            string temp = "";
            temp = temp + "?";
            temp = temp + param;
            temp = temp + "=";
            temp = temp + value;
            return temp;
        }
    }
}
