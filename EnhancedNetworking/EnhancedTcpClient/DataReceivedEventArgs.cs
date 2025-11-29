
namespace MorskoyGoy.EnhancedNetworking
{
    using System;
    using System.Collections.Generic;


    public class DataReceivedEventArgs : EventArgs
    {

        public DataReceivedEventArgs(byte[] rawData)
        {
            this.RawData = rawData;
        }

  
        public byte[] RawData
        {
            get;
            set;
        }


        public string Text
        {
            get
            {
                return System.Text.Encoding.UTF8.GetString(this.RawData);
            }
        }
    }
}