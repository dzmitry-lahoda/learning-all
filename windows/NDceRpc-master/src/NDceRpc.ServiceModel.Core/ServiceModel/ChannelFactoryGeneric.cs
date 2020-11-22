namespace NDceRpc.ServiceModel
{
    public class ChannelFactory<TService> : ChannelFactory
    {
        private ServiceEndpoint _endpoint;


        public ChannelFactory(Binding binding)
            : base(binding, typeof(TService), false)
        {

        }



        public TService CreateChannel(EndpointAddress createEndpoint)
        {
            return base.CreateChannel<TService>(createEndpoint);

        }

        public void Dispose()
        {
            base.Dispose();
        }
    }
}