using System.Text;

namespace GeoForge.NBT
{
    public class NBTTagIntArray : NBTBase
    {
        private int[] _intArray;

        public NBTTagIntArray()
        {
        }

        public NBTTagIntArray(int[] array)
        {
            _intArray = array;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(_intArray.Length);

            for (int i = 0; i < _intArray.Length; i++)
            {
                writer.Write(_intArray[i]);
            }
        }

        public override void Read(BinaryReader reader, int depth, NBTSizeTracker tracker)
        {
            tracker.Read(192L);
            int length = reader.ReadInt32();
            tracker.Read(length * 32);
            _intArray = new int[length];

            for (int i = 0; i < length; i++)
            {
                _intArray[i] = reader.ReadInt32();
            }
        }

        public override byte GetID()
        {
            return 11;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new("[I;");

            for (int i = 0; i < _intArray.Length; i++)
            {
                if (i != 0)
                {
                    stringBuilder.Append(',');
                }

                stringBuilder.Append(_intArray[i]);
            }

            return stringBuilder.Append(']').ToString();
        }

        public override NBTTagIntArray Copy()
        {
            int[] newIntArray = new int[_intArray.Length];
            Buffer.BlockCopy(_intArray, 0, newIntArray, 0, _intArray.Length * sizeof(int));
            return new(newIntArray);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj) && _intArray.SequenceEqual(((NBTTagIntArray)obj)._intArray);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ CalculateArrayHashCode(_intArray);
        }

        public int[] GetIntArray()
        {
            return _intArray;
        }
    }
}
