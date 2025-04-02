namespace GeoForge.NBT
{
    public class NBTTagByte : NBTPrimitive
    {
        private byte _data;

        public NBTTagByte()
        {
        }

        public NBTTagByte(byte data)
        {
            _data = data;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(_data);
        }

        public override void Read(BinaryReader reader, int depth, NBTSizeTracker tracker)
        {
            tracker.Read(72L);
            _data = reader.ReadByte();
        }

        public override byte GetID()
        {
            return 1;
        }

        public override string ToString()
        {
            return _data + "b";
        }

        public override NBTTagByte Copy()
        {
            return new NBTTagByte(_data);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj) && _data == ((NBTTagByte)obj)._data;
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
            return _data;
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
