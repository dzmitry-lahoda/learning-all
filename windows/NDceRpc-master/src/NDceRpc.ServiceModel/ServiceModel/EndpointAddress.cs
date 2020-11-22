namespace NDceRpc.ServiceModel
{
    public class EndpointAddress
    {
        public EndpointAddress(string address)
        {
            Uri = address;
        }

        public string Uri { get; set; }

        protected bool Equals(EndpointAddress other)
        {
            return string.Equals(Uri, other.Uri);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EndpointAddress) obj);
        }

        public override int GetHashCode()
        {
            return (Uri != null ? Uri.GetHashCode() : 0);
        }
    }
}
