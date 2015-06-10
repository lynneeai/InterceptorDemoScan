using System.Runtime.Serialization;

namespace ConsoleApplication1
{

    //[DataContract]

    public class InterceptorIdJSON
    {

        public InterceptorIdJSON(string intSerial, string key)
        {
            this.intSerial = intSerial;
            this.key = key;
        }

        public string intSerial;
        public string key;

        // ReSharper restore InconsistentNaming

        public override string ToString()
        {
            return string.Format("[IdList intSerial: {0}, key: {1} ]", intSerial, key);
        }
    }

}

