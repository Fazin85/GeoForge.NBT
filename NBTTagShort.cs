namespace GeoForge.NBT
{
    public class NBTTagShort : NBTPrimitive
    {
        private short _data;

        public NBTTagShort()
        {
        }

        public NBTTagShort(short data)
        {
            _data = data;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(_data);
        }

        public override void Read(BinaryReader reader, int depth, NBTSizeTracker tracker)
        {
            tracker.Read(80L);
            _data = reader.ReadInt16();
        }

        public override byte GetID()
        {
            return 2;
        }

        public override string ToString()
        {
            return _data.ToString() + "s";
        }

        public override NBTTagShort Copy()
        {
            return new(_data);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj) && _data == ((NBTTagShort)obj)._data;
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
            return _data;
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
