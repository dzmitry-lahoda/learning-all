//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.InteropServices;
//using System.ServiceModel;
//using System.ServiceModel.Channels;
//using System.Text;

//namespace WCFLib
//{public class PipeConnectionListenerWrapper
//{
//    ﻿﻿public IEnumerable<SafeHandle> PendingPipes
//    {
//        get 
//        {
//            IList pendingAccepts = null;
//            FieldInfo pendingAcceptField = _wrappedType.GetField("pendingAccepts", 
//                BindingFlags.Instance | BindingFlags.NonPublic);
//            if (null != pendingAcceptField)
//            {
//               pendingAccepts = pendingAcceptField.GetValue(_pipeConnectionListener) as IList;
//           }
//           foreach (object pendingAccept in pendingAccepts)
//           {
//               var pipeHandle = pendingAccept.GetType().GetField("pipeHandle", 
//                   BindingFlags.Instance|BindingFlags.NonPublic).GetValue(pendingAccept);
//               yield return pipeHandle as SafeHandle;
//           }
//       }
//   }
//}

//    public class NetNamedPipeSecurityExtensionsBindingElement ﻿: BindingElement
//    {
//        IntegrityLabel _pipeMandatoryIntegrityLabel;
//     ﻿
//     public NetNamedPipeSecurityExtensionsBindingElement(
//                     ﻿﻿      IntegrityLabel pipeIntegrityLabelRequired)
//     {
//         _pipeMandatoryIntegrityLabel = new IntegrityLabel(pipeIntegrityLabelRequired.Level, 
//                                                           pipeIntegrityLabelRequired.Policy);
//     }
  
//     public override BindingElement Clone()
//     {
//         return new NetNamedPipeSecurityExtensionsBindingElement(_pipeMandatoryIntegrityLabel);
//     }
  
//     public override T GetProperty<T>(BindingContext context)
//     {
//         if (typeof(T) == typeof(IntegrityLabel))
//         {
//             return new IntegrityLabel(_pipeMandatoryIntegrityLabel.Level, 
//                                       _pipeMandatoryIntegrityLabel.Policy) as T;
//         }
//         return context.GetInnerProperty<T>();
//     }
  
//     public override IChannelListener<TChannel> BuildChannelListener<TChannel>(
//                                                             BindingContext context)
//     {
//         IChannelListener<TChannel> listener = context.BuildInnerChannelListener<TChannel>();
//         listener.Opened += new EventHandler(﻿OnChannelListenerOpened);
//         return listener;
//     }
  
//     void OnChannelListenerOpened(object sender, EventArgs e)
//     {
//         ChannelListenerBase listener = sender as ChannelListenerBase;
//         if (null != listener)
//         {
//            // Find a suitable pipe handle and apply the integrity label
//         }
//     }
//}
//        public class TransportManagerTableWrapper
//    {
//       private object _transportManagerTable;
//       private IList _tableContents;
//        private PropertyInfo _contentKeyProperty;
//        private PropertyInfo _contentValueProperty;
//        private Type _contentKeyType;
//        private HostNameComparisonMode _hostNameComparisonMode;
     
//       public TransportManagerTableWrapper(ChannelListenerBase channelListener)
//       {
//           PropertyInfo pi = channelListener.GetType().GetProperty("TransportManagerTable", 
//               BindingFlags.Instance | BindingFlags.NonPublic);
//           _transportManagerTable = pi.GetValue(channelListener, null);
//           _tableContents = _transportManagerTable.GetType().GetMethod("GetAll").Invoke(
//                       _transportManagerTable, null) as IList;
//           Type tableContentsItemType = _tableContents[0].GetType();
//           var contentItemPairTypes = tableContentsItemType.GetGenericArguments();
//           _contentKeyProperty = tableContentsItemType.GetProperty("Key");
//           _contentValueProperty = tableContentsItemType.GetProperty("Value");
//           _contentKeyType = contentItemPairTypes[0];
//           PropertyInfo hncmProperty = channelListener.GetType().GetProperty(
//                       "HostNameComparisonMode", BindingFlags.Instance|BindingFlags.Public);
//          _hostNameComparisonMode = (HostNameComparisonMode)hncmProperty.GetValue(
//                       channelListener, null);
//       }
    
//       public TransportManagerWrapper GetTransportManager(Uri listenUri)
//       {
//           object keyBaseUriWithWildcard = CreateBaseUriWithWildcard(listenUri);
//           object transportManager = null;
//           foreach (object kvPair in _tableContents)
//           {
//               if (_contentKeyProperty.GetValue(kvPair, null).Equals(keyBaseUriWithWildcard))
//               {
//                   transportManager = _contentValueProperty.GetValue(kvPair, null);
//                   break;
//               }
//           }
//           if (listenUri.Scheme == Uri.UriSchemeNetPipe)
//           {
//               return new NamedPipeTransportManagerWrapper(transportManager);
//           }
//           return new TransportManagerWrapper(transportManager);
//       }
    
//       private object CreateBaseUriWithWildcard(Uri listenUri)
//       {
//           BindingFlags ctorBindingFlags = BindingFlags.Instance;
//           ctorBindingFlags |= (Environment.Version.Major >= 4) ? 
//               BindingFlags.Public : BindingFlags.NonPublic;
//           ConstructorInfo ctor = _contentKeyType.GetConstructor(
//               ctorBindingFlags, null, 
//               new Type[] { typeof(Uri), typeof(HostNameComparisonMode) }, 
//               null);         
//           return ctor.Invoke(new object[]{listenUri, _hostNameComparisonMode});
//       }
//   }
//    public class NamedPipeTransportM﻿anagerWrapper﻿﻿
//{
//      public PipeConnectionListenerWrapper PipeConnectionListener
//    {
//        get
//        {
//            FieldInfo connectionListenerFieldInfo = WrappedType.GetField("connectionListener", 
//                BindingFlags.Instance | BindingFlags.NonPublic);
//            if (null != connectionListenerFieldInfo)
//            {
//                object connectionListener = connectionListenerFieldInfo.GetValue(
//                                                                         _transportManager);
    
//               // connectionListener may be wrapping another IConnectionListener type
//               // we need the innermost IConnectionListener
//               int recursionCountLeft = 10;
//               while (recursionCountLeft > 0 
//                      && null == connectionListener.GetType().GetField(
//                                             "pendingAccepts", 
//                                             BindingFlags.Instance | BindingFlags.NonPublic))
//               {
//                   recursionCountLeft--;
//                   FieldInfo[] fields = connectionListener.GetType().GetFields(
//                                             BindingFlags.Instance | BindingFlags.NonPublic);
//                   foreach (FieldInfo f in fields)
//                   {
//                       if ("IConnectionListener".Equals(f.FieldType.Name))
//                       {
//                           connectionListener = f.GetValue(connectionListener);
//                           break;
//                       }
//                   }
//               }
//               return new PipeConnectionListenerWrapper(connectionListener);
//           }
//           else
//           {
//               // unsupported transport manager type
//               // e.g. HostedNamedPipeTransportManager
//               throw new Exception();
//           }
//       }
//   }

//}

  
//}
