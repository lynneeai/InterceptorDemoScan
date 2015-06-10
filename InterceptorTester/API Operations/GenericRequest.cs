using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication1
{
    class GenericRequest : APIOperation
    {
        Object data;
        string query;

        public GenericRequest(Uri server, string query, Object data)
        {
            this.opHost = server;
            this.query = query;
            this.data = data;
        }

        public override string getExpectedResult()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override Uri getUri()
        {
            return new Uri(opHost, query);
        }

        public override object getJson()
        {
            return data;
        }
    }
}
