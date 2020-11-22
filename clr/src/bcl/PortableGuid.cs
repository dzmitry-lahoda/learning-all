using System;
using System.Runtime.InteropServices;

namespace extensions
{

    //// TODO: all publics and statics and hash and equals
    //// TODO: ensure that null string works fine
    //// TODO: null string to Guid.Empty ? 
    /// <summary>
    /// Represents a Globally Unique Identifier and formated string with minimal overhead. Compatible with <see cref="Guid"/>
    /// </summary>
    /// <remarks>
    /// We do not store string because of size see `uuid.fsx`
    /// </remarks>
    /// <seealso href=" http://referencesource.microsoft.com/#mscorlib/system/guid.cs,b622ef5f6b76c10a"/>
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    [System.Runtime.InteropServices.ComVisible(true)]
    // ensure DebuggerDisplay to stirng.
    public struct PortableGuid : IComparable
        , IComparable<Guid>, IEquatable<Guid>
        , IComparable<PortableGuid>, IEquatable<PortableGuid>
        , IComparable<string>, IEquatable<string>
    {
        private Guid _guid;


        // prevents and drops non essential parts like brackets ("{", "}"),  dash ("-"), whitespaces
        private const string format = "N";

        public PortableGuid(byte[] b)
        {
            _guid = new Guid(b);
        }

        [CLSCompliant(false)]
        public PortableGuid(uint a, ushort b, ushort c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
        {
            _guid = new Guid(a, b, c, d, e, f, g, h, i, j, k);
        }

        [System.Diagnostics.DebuggerStepThrough]

        public PortableGuid(int a, short b, short c, byte[] d)
        {
            _guid = new Guid(a, b, c, d);
        }

        [System.Diagnostics.DebuggerStepThrough]
        public PortableGuid(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
        {
            _guid = new Guid(a, b, c, d, e, f, g, h, i, j, k);
        }

        [System.Diagnostics.DebuggerStepThrough]
        public PortableGuid Parse(string value)
        {
            return new PortableGuid(value);
        }

        [System.Diagnostics.DebuggerStepThrough]

        public PortableGuid ParseExact(string value)
        {
            return new PortableGuid(value);
        }


        [System.Diagnostics.DebuggerStepThrough]
        public PortableGuid(Guid guid)
        {
            _guid = guid;
        }

        [System.Diagnostics.DebuggerStepThrough]
        public PortableGuid(string guid)
        {
            _guid = Guid.ParseExact(guid, format);
        }

        public Guid Guid
        {
            get { return _guid; }
        }

        [System.Diagnostics.DebuggerStepThrough]

        public static implicit operator PortableGuid(Guid d)
        {
            return new PortableGuid(d);
        }

        [System.Diagnostics.DebuggerStepThrough]

        public static implicit operator Guid(PortableGuid d)
        {
            return d.Guid;
        }

        [System.Diagnostics.DebuggerStepThrough]

        public static implicit operator String(PortableGuid d)
        {
            return d.Guid.ToString(format);
        }

        public static explicit operator PortableGuid(String d)
        {
            return new PortableGuid(d);
        }

        public override string ToString()
        {
            return this.Guid.ToString(format);
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(Guid other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Guid other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(PortableGuid other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(PortableGuid other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(string other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(string other)
        {
            throw new NotImplementedException();
        }
    }
}
