using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace InterceptorTester.Tests.AdminTests
{
	/// <summary>
	/// Class for create device status - Is not backwards compatible with IPS
	/// </summary>
	public class DeviceStatusPutJSON
	{
        public int deviceStatus;
        public int capture;
        public int captureMode;
        public int callHometimeoutMode;
        public string callHomeTimeoutData;
        public int errorLog;
        public string[] dynCodeformat;
        public int forwardType;
        public string intLocDesc;
        public string startURL;
        public string reportURL;
        public string scanURL;
        public string bkupURL;
        public string cmdURL;
        public string ForwardURL;
        public int requestTimeoutValue;
        public int maxBatchWaitTime;
        public int cmdChkInt;
        public string Ssid;

        public DeviceStatusPutJSON()
        {
            this.deviceStatus = 0;
            this.capture = 1;
            this.captureMode = 0;
            this.callHometimeoutMode = 0;
            this.callHomeTimeoutData = null;
            this.errorLog = 0;
            string[] dyn = new string[2];

            dyn[0] = "[~[1,12]/*[1,63]]";
            dyn[1] = "[1,100]";
            this.dynCodeformat = dyn;
            this.forwardType = 1;
            this.intLocDesc = "wubba";
            this.startURL = "http://api.cozumo.net/DeviceSetting";
            this.reportURL = "http://api.cozumo.net/DeviceStatus";
            this.scanURL = "http://api.cozumo.net/DeviceScan";
            this.bkupURL = "http://api.cozumo.net/DeviceBackup";
            this.cmdURL = "http://api.cozumo.net/iCmd";
            this.ForwardURL = "http://api.cozumo.net/fwd";
            this.requestTimeoutValue = 200;
            this.maxBatchWaitTime = 16;
            this.cmdChkInt = 15;
            this.Ssid = "43B81B4F768D0549AB4F178022DEB384";
        }
	}
}

