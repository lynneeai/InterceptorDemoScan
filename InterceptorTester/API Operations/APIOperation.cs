using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    public abstract class APIOperation
    {
        protected Uri opHost;
        protected HTTPOperation hOp;
		protected HTTPQuery opQuery = null;

        public abstract String getExpectedResult();

        public void setHost(Uri newUri)
        {
            opHost = newUri;
        }
        public void setQuery(HTTPQuery newQuery)
        {
            opQuery = newQuery;
        }
        public void setHOp(HTTPOperation newHOp)
        {
            hOp = newHOp;
        }
        public Uri getHost()
        {
            return opHost;
        }
        public HTTPQuery getQuery()
        {
            return opQuery;
        }
        public HTTPOperation getHOp()
        {
            return hOp;
        }
        public abstract override string ToString();
        public abstract Uri getUri();
        //TODO: Find a less terrible way to do this
        public abstract Object getJson();
    }
}
