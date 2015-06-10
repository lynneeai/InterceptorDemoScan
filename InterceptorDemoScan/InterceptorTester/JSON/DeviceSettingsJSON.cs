using System.Runtime.Serialization;

namespace ConsoleApplication1
{

        //[DataContract]

    public class DeviceSettingsJSON
        {
            
			public bool isValid ()
			{
				if (seqNum != null)
				{
					return true;
				}

				return false;
			}

			//[DataMember]
			// ReSharper disable InconsistentNaming
            public string startURL;
            //[DataMember]
            public string reportURL;
            //[DataMember]
            public string scanURL;
            //[DataMember]
            public string bkupURL;
            //[DataMember]
            public string cmdURL;
            //[DataMember]
            public string capture;
            //[DataMember]
            public string captureMode;
            //[DataMember]
            public string requestTimeoutValue;
            //[DataMember]
            public string callHomeTimeoutMode;
            //[DataMember]
            public string callHomeTimeoutData;
            //[DataMember]
            public string[] dynCodeFormat;
            //[DataMember]
            public string errorLog;
            //[DataMember]
            public string wpaPSK;
            //[DataMember]
            public string ssid;
            //[DataMember]
            public string cmdChkInt;
            //[DataMember]
            public string cTime;
            //[DataMember]
            public string seqNum;
            // ReSharper restore InconsistentNaming

            public override string ToString()
            {
                return string.Format("[DeviceStatus startURL: {1}, reportURL: {2}, scanURL: {3}, bkupURL: {4}, cmdURL: {5}, capture: {6}, captureMode: {7}, requestTimeoutValue: {8}, callHomeTimeoutMode: {9}, callHomeTimeoutData: {10}, dynCodeFormat: {11}, errorLog: {12}, wpaPSK: {13}, ssid: {14}, cmdChkInt: {15}, cTime: {16}, seqNum: {17}]",
                    startURL, reportURL, scanURL, bkupURL, cmdURL, capture, captureMode, requestTimeoutValue, callHomeTimeoutMode, callHomeTimeoutData, dynCodeFormat, errorLog, wpaPSK, ssid, cmdChkInt, cTime, seqNum);
            }
        }



}