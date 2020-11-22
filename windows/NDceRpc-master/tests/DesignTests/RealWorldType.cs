using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace NDceRpc.ServiceModel.IntegrationTests
{
    [DataContract]
    public sealed class UserCredentials
    {
   
        [DataMember(Order = 1)]
        public string Username { get; set; }


        [DataMember(Order = 2)]
        public string Password { get; set; }
    }

    [DataContract]
    public sealed class ServerConnectionDetails
    {

        [DataMember]
        public string Address { get; set; }


        [DataMember]
        public int Port { get; set; }
    }

    [DataContract]
    public sealed class ProxyDetails
    {

        [DataMember(Order = 1)]
        public ServerConnectionDetails ProxyConnectionDetails
        {
            get;
            set;
        }


        [DataMember(Order = 2)]
        public UserCredentials ProxyUserCredentials
        {
            get;
            set;
        }

   
        [DataMember(Order = 3)]
        public bool EnableExpect100Continue
        {
            get;
            set;
        }


        [DataMember(Order = 4)]
        public bool EnableUseCurrentUserCredentials
        {
            get;
            set;
        }

    }

    public enum AuthenticationState
    {
   
        Undefined,

   
        Offline,

   
        Authenticated,

   
        NotAuthenticated
    }

    [DataContract]
    public class UserInfo
    {

        /// <summary>
        /// Gets or sets the authentication state.
        /// </summary>
        /// <value>The state.</value>
        [DataMember(Order = 1)]
        public AuthenticationState State
        {
            get;
            set;
        }

        [DataMember(Order = 2)]
        public string Token
        {
            get;
            set;
        }


        [DataMember(Order = 3)]
        public DateTime TokenCreationTime
        {
            get;
            set;
        }


        [DataMember(Order = 4)]
        public DateTime TokenExpiryTime
        {
            get;
            set;
        }


        [DataMember(Order = 5)]
        public string UserId
        {
            get;
            set;
        }


        [DataMember(Order = 7)]
        public string UserLogin { get; set; }


        [DataMember(Order = 8)]
        public List<string> Entitlements
        {
            get;
            set;
        }



        [DataMember(Order = 11)]
        public bool UseProxy
        {
            get;
            set;
        }


        [DataMember(Order = 12)]
        public ProxyDetails ProxyDetails
        {
            get;
            set;
        }



    }
}
