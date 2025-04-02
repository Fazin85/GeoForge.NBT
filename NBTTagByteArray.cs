using System.Text;

namespace GeoForge.NBT
{
    public class NBTTagByteArray : NBTBase
    {
        private byte[] _data;

        public NBTTagByteArray()
        {
        }

        public NBTTagByteArray(byte[] data)
        {
            _data = data;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(_data.Length);
            writer.Write(_data);
        }

        public override void Read(BinaryReader reader, int depth, NBTSizeTracker tracker)
        {
            tracker.Read(192L);
            int length = reader.ReadInt32();
            tracker.Read(8 * length);
            _data = new byte[length];
            int read = reader.Read(_data);

            if (read != length)
            {
                throw new Exception($"Failed to read {length} bytes from binaryReader");
            }
        }

        public override byte GetID()
        {
            return 7;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new("[B;");

            for (int i = 0; i < _data.Length; i++)
            {
                if (i != 0)
                {
                    stringBuilder.Append(',');
                }

                stringBuilder.Append((int)_data[i]).Append('B');
            }

            return stringBuilder.Append(']').ToString();
        }

        public override NBTBase Copy()
        {
            byte[] copy = new byte[_data.Length];

            Buffer.BlockCopy(_data, 0, copy, 0, _data.Length);

            return new NBTTagByteArray(copy);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj) && _data.SequenceEqual(((NBTTagByteArray)obj)._data);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ CalculateArrayHashCode(_data);
        }

        public byte[] GetByteArray()
        {
            return _data;
        }
    }
}
