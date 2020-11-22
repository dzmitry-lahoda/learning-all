using System;
using System.IO;

namespace NDceRpc.ServiceModel.Channels
{
    public class ProtobufMessageEncodingBindingElement : MessageEncoder
    {
        private static Messages _proto;


        static ProtobufMessageEncodingBindingElement()
        {
            //TODO: use http://blogs.msdn.com/b/microsoft_press/archive/2010/02/03/jeffrey-richter-excerpt-2-from-clr-via-c-third-edition.aspx
            //TODO: or http://research.microsoft.com/en-us/people/mbarnett/ilmerge.aspx
            //NOTE: used compiled messages to save start uo time
            _proto = new Messages();

        }

        public override void WriteObject(Stream stream, object graph)
        {
            if (graph == null) return;
            _proto.Serialize(stream, graph);
        }

        public override object ReadObject(Stream data, Type type)
        {
            if (data.Length == 0 && typeof(object) == type)
                return null;
            return _proto.Deserialize(data, null, type);
        }




    }

}
