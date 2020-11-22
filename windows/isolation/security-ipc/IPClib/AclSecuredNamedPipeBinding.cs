using System.Collections.Generic;
using System.Reflection;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace IPCLib
{
    public class AclSecuredNamedPipeBinding : CustomBinding
    {
        private NamedPipeTransportBindingElement _tt;
        public List<SecurityIdentifier> _allowedUsers;

        public AclSecuredNamedPipeBinding()
            : base()
        {
            NetNamedPipeBinding standardBinding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            foreach (BindingElement element in standardBinding.CreateBindingElements())
            {
                NamedPipeTransportBindingElement transportElement = element as NamedPipeTransportBindingElement;
                
                if (transportElement != null)
                {
                    _tt = transportElement;
                    base.Elements.Add(new AclSecuredNamedPipeTransportBindingElement(transportElement));
                    //base.Elements.Add(transportElement);
                }
                else if (!(element is WindowsStreamSecurityBindingElement))
                {
                    base.Elements.Add(element);
                }
            }
            AddUserOrGroup(WindowsIdentity.GetCurrent().User);
        }

        public void AddUserOrGroup(SecurityIdentifier sid)
        {
            var sdd = new SecurityIdentifier("S-1-5-21-1392800327-2415074910-3460020496-1000");
            //if (sid  == sdd) return;
            var asd = Elements.Find<AclSecuredNamedPipeTransportBindingElement>();
            if (asd != null)
            {
                List<SecurityIdentifier> allowedUsers
                    = asd.AllowedUsers;
                if (!allowedUsers.Contains(sid))
                {
                    allowedUsers.Add(sid);
                    _allowedUsers = allowedUsers;
                }
            }

            PropertyInfo p = _tt.GetType().GetProperty("AllowedUsers", BindingFlags.Instance | BindingFlags.NonPublic);
            var ids = p.GetValue(_tt, null) as IList<SecurityIdentifier>;
            if (ids == null)
            {
                ids = new List<SecurityIdentifier>();
                p.SetValue(_tt, ids, null);
            }
            if (!ids.Contains(sid))
            {
                ids.Add(sid);
            }
        }
    }
}