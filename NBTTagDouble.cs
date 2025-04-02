namespace GeoForge.NBT
{
    public class NBTTagDouble : NBTPrimitive
    {
        private double _data;
        public NBTTagDouble()
        {
        }
        public NBTTagDouble(double value)
        {
            _data = value;
        }
        public override void Write(BinaryWriter writer)
        {
            writer.Write(_data);
        }

        public override void Read(BinaryReader reader, int depth, NBTSizeTracker tracker)
        {
            tracker.Read(128L);
            _data = reader.ReadDouble();
        }

        public override byte GetID()
        {
            return 6;
        }

        public override string ToString()
        {
            return _data + "d";
        }

        public override NBTTagDouble Copy()
        {
            return new(_data);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj) && _data == ((NBTTagDouble)obj)._data;
        }

        public override int GetHashCode()
        {
            long i = BitConverter.DoubleToInt64Bits(_data);
            return base.GetHashCode() ^ (int)(i ^ (i >> 32));
        }

        public override long GetLong()
        {
            return (long)Math.Floor(_data);
        }

        public override int GetInt()
        {
            return (int)Math.Floor(_data);
        }

        public override short GetShort()
        {
            return (short)((int)Math.Floor(_data) & 65535);
        }

        public override byte GetByte()
        {
            return (byte)((int)Math.Floor(_data) & 255);
        }

        public override double GetDouble()
        {
            return _data;
        }

        public override float GetFloat()
        {
            return (float)_data;
        }
    }
}
