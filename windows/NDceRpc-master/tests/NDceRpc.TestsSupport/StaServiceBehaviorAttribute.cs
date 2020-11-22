using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.Security.Permissions;

namespace StaThreadSyncronizer
{   
   public class StaServiceBehaviorAttribute : Attribute, IContractBehavior, IServiceBehavior
   {
      StaSynchronizationContext mStaContext;
      public StaServiceBehaviorAttribute()
      {
         mStaContext = new StaSynchronizationContext();        
      }
      #region IContractBehavior Members

      void IContractBehavior.AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
      {
         
      }

      void IContractBehavior.ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
      {
         
      }

      void IContractBehavior.ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.DispatchRuntime dispatchRuntime)
      {
         dispatchRuntime.SynchronizationContext = mStaContext;         
      }

      void IContractBehavior.Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
      {
         
      }

      #endregion

      #region IServiceBehavior Members

      void IServiceBehavior.AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
      {
         
      }

      void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
      {
         
      }

      void IServiceBehavior.Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
      {
         serviceHostBase.Closed += delegate
         {
            mStaContext.Dispose();
         };
      }

      #endregion
   }
}
