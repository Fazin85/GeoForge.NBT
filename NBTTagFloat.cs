namespace GeoForge.NBT
{
    public class NBTTagFloat : NBTPrimitive
    {
        private float _data;

        public NBTTagFloat()
        {
        }

        public NBTTagFloat(float data)
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
            _data = reader.ReadSingle();
        }

        public override byte GetID()
        {
            return 5;
        }

        public override string ToString()
        {
            return _data + "f";
        }

        public override NBTTagFloat Copy()
        {
            return new(_data);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj) && _data == ((NBTTagFloat)obj)._data;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ BitConverter.SingleToInt32Bits(_data);
        }

        public override long GetLong()
        {
            return (long)_data;
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
            return _data;
        }
    }
}
