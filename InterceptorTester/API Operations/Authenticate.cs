using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
	class Authenticate : APIOperation
	{
        AuthenticateJSON json;
		public Authenticate(Uri server, AuthenticateJSON json)
		{
			opHost = server;
			hOp = HTTPOperation.POST;
            this.json = json;
		}

		public override string getExpectedResult()
		{
			if (opQuery.isValid())
			{
				return "200";
			}
			else
			{
				return "400";
			}
		}

		public override string ToString()
		{
			return "Authenticate";
		}

		public override Uri getUri()
		{
			return new Uri(opHost, "/api/Authenticate/");
		}

		public override object getJson()
		{
			return json;
		}
	}
}


