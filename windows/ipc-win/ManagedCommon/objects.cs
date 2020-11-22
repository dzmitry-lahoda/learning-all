using System;
using System.Collections.Generic;
using System.Text;

namespace managed_entities
{

    public class custom_request
    {
        //Like on potential public VBA/COM 
        public ICollection<string> m_ids = new LinkedList<string>();
       public ICollection<string> m_types = new LinkedList<string>();
        //and slower  visibly slower then
        //public ICollection<string> m_ids = new List<string>();
        //public ICollection<string> m_types = new List<string>();

        public string m_name = string.Empty;
        private int idss;
        private int typess;

        public custom_request(Type c = null)
        {
  
        }

        public int getSize()
        {
            int size = 0;

            idss = 0;
            foreach (var it in m_ids)
            {
                idss += it.Length + 1;
            }
            size += idss;
            typess = 0;
            foreach (var it in m_types)
            {
                typess += it.Length + 1;
            }




            size += typess;

            size += m_name.Length + 1;

            size += sizeof(int) * 3 + 2 * sizeof(int); // to store 3 size of bytes and 2 for storing elements count
            return size;
        }

        public void fromArray(byte[] buffer)
        {
            int pointer = 0;
            idss = BitConverter.ToInt32(buffer, 0);
            pointer += sizeof(int);

            var cids = BitConverter.ToInt32(buffer, pointer);
            pointer += sizeof(int);

            typess = BitConverter.ToInt32(buffer, pointer);
            pointer += sizeof(int);

            var ctypess = BitConverter.ToInt32(buffer, pointer);
            pointer += sizeof(int);


            var ns = BitConverter.ToInt32(buffer, pointer);
            pointer += sizeof(int);


            for (var i = 0; i < cids; i++)
            {
                int next_null = pointer;
                while (true)
                {

                    if (buffer[next_null] == 0)
                    {
                        break;
                    }
                    next_null++;

                }
                string s = Encoding.ASCII.GetString(buffer, pointer, next_null - pointer);
                m_ids.Add(s);
                pointer += (next_null - pointer + 1);
            }

            for (var i = 0; i < ctypess; i++)
            {
                int next_null = pointer;
                while (true)
                {

                    if (buffer[next_null] == 0)
                    {
                        break;
                    }
                    next_null++;

                }
                string s = Encoding.ASCII.GetString(buffer, pointer, next_null - pointer);
                m_types.Add(s);
                pointer += (next_null - pointer + 1);
            }

            m_name = Encoding.ASCII.GetString(buffer, pointer, ns - 1);
        }

        public void toArray(out byte[] buffer, int size)
        {
            int pointer = 0;
            buffer = new byte[size];

            Array.Copy(BitConverter.GetBytes(idss), 0, buffer, pointer, sizeof(int));
            pointer += sizeof(int);

            int cids = m_ids.Count;
            Array.Copy(BitConverter.GetBytes(cids), 0, buffer, pointer, sizeof(int));
            pointer += sizeof(int);

            Array.Copy(BitConverter.GetBytes(typess), 0, buffer, pointer, sizeof(int));
            pointer += sizeof(int);

            int ctypes = m_types.Count;
            Array.Copy(BitConverter.GetBytes(ctypes), 0, buffer, pointer, sizeof(int));
            pointer += sizeof(int);

            var sn = Encoding.ASCII.GetBytes(m_name);
            int ns = sn.Length + 1;
            Array.Copy(BitConverter.GetBytes(ns), 0, buffer, pointer, sizeof(int));
            pointer += sizeof(int);


            foreach (var it in m_ids)
            {
                var s = Encoding.ASCII.GetBytes(it);
                Array.Copy(s, 0, buffer, pointer, s.Length);

                pointer += s.Length + 1;

            }

            foreach (var it in m_types)
            {
                var s = Encoding.ASCII.GetBytes(it);
                Array.Copy(s, 0, buffer, pointer, s.Length);
                pointer += s.Length + 1;
            }


            Array.Copy(sn, 0, buffer, pointer, sn.Length);
            pointer += sn.Length;
        }
    }

    public class custom_response
    {

        //NOTE: just for simplicy - in real we should store somehow column and row info
        private List<string> m_data = new List<string>();

        public int getSize()
        {
            return 0;
        }

        public void fromArray(byte[] buffer)
        {
            int pointer = 0; 
	int size = BitConverter.ToInt32(buffer,0);
            pointer += sizeof (int);


            for (var i = 0; i < size; i++)
            {
                int next_null = pointer;
                while (true)
                {

                    if (buffer[next_null] == 0)
                    {
                        break;
                    }
                    next_null++;

                }
                string s = Encoding.ASCII.GetString(buffer, pointer, next_null - pointer);
                m_data.Add(s);
                pointer += (next_null - pointer + 1);
            }
	
        }

        public void toArray(out byte[] buffer, int size)
        {
            buffer = null;
            size = 0;
        }
    }

}
