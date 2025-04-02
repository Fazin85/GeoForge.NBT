using System.Text;

namespace GeoForge.NBT
{
    public class NBTTagLongArray : NBTBase
    {
        private long[] _longArray;

        public NBTTagLongArray()
        {
        }

        public NBTTagLongArray(long[] array)
        {
            _longArray = array;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(_longArray.Length);

            for (int i = 0; i < _longArray.Length; i++)
            {
                writer.Write(_longArray[i]);
            }
        }

        public override void Read(BinaryReader reader, int depth, NBTSizeTracker tracker)
        {
            tracker.Read(192L);
            int length = reader.ReadInt32();
            tracker.Read(length * 64);
            _longArray = new long[length];

            for (int i = 0; i < length; i++)
            {
                _longArray[i] = reader.ReadInt64();
            }
        }

        public override byte GetID()
        {
            return 12;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new("[L;");

            for (int i = 0; i < _longArray.Length; i++)
            {
                if (i != 0)
                {
                    stringBuilder.Append(',');
                }

                stringBuilder.Append(_longArray[i]);
            }

            return stringBuilder.Append(']').ToString();
        }

        public override NBTTagLongArray Copy()
        {
            long[] newLongArray = new long[_longArray.Length];
            Buffer.BlockCopy(_longArray, 0, newLongArray, 0, _longArray.Length * sizeof(long));
            return new(newLongArray);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj) && _longArray.SequenceEqual(((NBTTagLongArray)obj)._longArray);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ CalculateArrayHashCode(_longArray);
        }

        public long[] GetLongArray()
        {
            return _longArray;
        }
    }
}
