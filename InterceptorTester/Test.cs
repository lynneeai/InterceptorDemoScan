using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    //TODO: Clean this (and all dependent files)
    public class Test
    {
        APIOperation operation;
        String expectedResult;
        String actualResult;
		String testName;
		String type = "unit";

		public Test(APIOperation nOperation)
        {
            operation = nOperation;
            actualResult = "?";
        }

        //TODOIF: Change if there's overlap between operation and HTTP query type.
        //Returns operation being run.
        public override string ToString()
        {
            //string testString = "PLACEHOLDER TEST TYPE";
            //return testString

            //This is going to break. If and when it does, use above code.
            return operation.ToString();
        }
			

        public APIOperation getOperation()
        {
            return operation;
        }

		public void setType(string testType)
		{
			type = testType;
		}

		public string getType()
		{
			return type;
		}


		public void setExpectedResult (String result)
        {
            expectedResult = result;
        }

        public String getExpectedResult ()
        {
			return expectedResult;
        }

        public void setActualResult (String result)
        {
            actualResult = result;
        }

        public String getActualResult ()
        {
            return actualResult;
        }

		public void setTestName(string name)
		{
			testName = name;
		}

		public String getTestName()
		{
			return testName;
		}

        //Pass/Fail
        public String result()
        {
            if (expectedResult.Equals(actualResult))
            {
                return "Pass";
            }
            else
            {
                return "Fail";
            }
        }
    }
}
