namespace GeoForge.NBT
{
    public abstract class NBTPrimitive : NBTBase
    {
        public abstract long GetLong();
        public abstract int GetInt();
        public abstract short GetShort();
        public abstract byte GetByte();
        public abstract double GetDouble();
        public abstract float GetFloat();
    }
}
