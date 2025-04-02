namespace GeoForge.NBT
{
    public class NBTTagLong : NBTPrimitive
    {
        private long _data;

        public NBTTagLong()
        {
        }

        public NBTTagLong(long data)
        {
            _data = data;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(_data);
        }

        public override void Read(BinaryReader reader, int depth, NBTSizeTracker tracker)
        {
            tracker.Read(128L);
            _data = reader.ReadInt64();
        }

        public override byte GetID()
        {
            return 4;
        }

        public override string ToString()
        {
            return _data.ToString() + "L";
        }

        public override NBTTagLong Copy()
        {
            return new(_data);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj) && _data == ((NBTTagLong)obj)._data;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ (int)(_data ^ _data >>> 32);
        }

        public override long GetLong()
        {
            return _data;
        }

        public override int GetInt()
        {
            return (int)(_data & -1L);
        }

        public override short GetShort()
        {
            return (short)(int)(_data & 65535L);
        }

        public override byte GetByte()
        {
            return (byte)(int)(_data & 255L);
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
