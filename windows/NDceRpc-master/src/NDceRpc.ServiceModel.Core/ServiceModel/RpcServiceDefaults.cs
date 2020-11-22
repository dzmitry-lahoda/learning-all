using System;

namespace NDceRpc.ServiceModel
{
    ///<seealso cref="System.ServiceModel.ServiceDefaults"/>
    internal static class RpcServiceDefaults
    {


        internal static TimeSpan ServiceHostCloseTimeout
        {
            get
            {
                return TimeSpan.FromSeconds(10);
            }
        }

        internal static TimeSpan CloseTimeout
        {
            get
            {
                return TimeSpan.FromMinutes(1);
            }
        }

        internal static TimeSpan OpenTimeout
        {
            get
            {
                return TimeSpan.FromMinutes(1);
            }
        }

        internal static TimeSpan ReceiveTimeout
        {
            get
            {
                return TimeSpan.FromMinutes(10);
            }
        }

        internal static TimeSpan SendTimeout
        {
            get
            {
                return TimeSpan.FromMinutes(1);
            }
        }

        internal static TimeSpan TransactionTimeout
        {
            get
            {
                return TimeSpan.FromMinutes(1);
            }
        }
    }
}
