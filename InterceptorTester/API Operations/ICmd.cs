using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class ICmd : APIOperation
    {
        public bool noQuery;
        public ICmd(Uri server, string serialNum)
        {
            opHost = server;
            hOp = HTTPOperation.GET;
            opQuery = new HTTPQuery(QueryParameter.i, serialNum);
        }

        public override string getExpectedResult()
        {
            if (this.noQuery)
            {
                return "404";
            }
            if (this.opQuery.isValid())
            {
                return "200";
            }
            return "400";
        }

        public override string ToString()
        {
            return "iCmd";
        }
        
        public override Uri getUri()
        {
            if (noQuery)
            {
                return getUriNoQuery();
            }
            return new Uri(opHost, "/api/iCmd/" + opQuery.ToString());
        }

        public Uri getUriNoQuery()
        {
            return new Uri(opHost, "/api/iCmd");
        }

        public override object getJson()
        {
            return null;
        }
    }
}
