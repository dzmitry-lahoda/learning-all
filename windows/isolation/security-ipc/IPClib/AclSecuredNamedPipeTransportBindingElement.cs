using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Principal;
using System.ServiceModel.Channels;

namespace IPCLib
{
    public class AclSecuredNamedPipeTransportBindingElement : NamedPipeTransportBindingElement
    {
        private static Type namedPipeChannelListenerType
            = Type.GetType("System.ServiceModel.Channels.NamedPipeChannelListener, " + typeof(NamedPipeTransportBindingElement).Assembly.FullName, true);

        public AclSecuredNamedPipeTransportBindingElement(NamedPipeTransportBindingElement inner)
            : base(inner)
        {
            
            if (inner is AclSecuredNamedPipeTransportBindingElement)
            {
                _allowedUsers = new List<SecurityIdentifier>(
                    ((AclSecuredNamedPipeTransportBindingElement)inner)._allowedUsers);
            }
        }

        public override BindingElement Clone()
        {
            return new AclSecuredNamedPipeTransportBindingElement(this);
        }

        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            IChannelListener<TChannel> listener = base.BuildChannelListener<TChannel>(context);

            PropertyInfo p = namedPipeChannelListenerType.GetProperty(
                "AllowedUsers", BindingFlags.Instance | BindingFlags.NonPublic);
            p.SetValue(listener, _allowedUsers, null);
            return listener;
        }

        public List<SecurityIdentifier> AllowedUsers { get { return _allowedUsers; } }
        public List<SecurityIdentifier> _allowedUsers = new List<SecurityIdentifier>();
    }
}
