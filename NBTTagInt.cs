namespace GeoForge.NBT
{
    public class NBTTagInt : NBTPrimitive
    {
        private int _data;

        public NBTTagInt()
        {
        }

        public NBTTagInt(int data)
        {
            _data = data;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(_data);
        }

        public override void Read(BinaryReader reader, int depth, NBTSizeTracker tracker)
        {
            tracker.Read(96L);
            _data = reader.ReadInt32();
        }

        public override byte GetID()
        {
            return 3;
        }

        public override string ToString()
        {
            return _data.ToString();
        }

        public override NBTTagInt Copy()
        {
            return new(_data);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj) && _data == ((NBTTagInt)obj)._data;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ _data;
        }

        public override long GetLong()
        {
            return _data;
        }

        public override int GetInt()
        {
            return _data;
        }

        public override short GetShort()
        {
            return (short)(_data & 65535);
        }

        public override byte GetByte()
        {
            return (byte)(_data & 255);
        }

        public override double GetDouble()
        {
            return _data;
        }

        public override float GetFloat()
        {
            return _data;
        }
    }
}
