namespace GeoForge.NBT
{
    public class NBTTagEnd : NBTBase
    {
        public override void Read(BinaryReader reader, int depth, NBTSizeTracker tracker)
        {
            tracker.Read(64L);
        }

        public override void Write(BinaryWriter writer)
        {
        }

        public override byte GetID()
        {
            return 0;
        }

        public override string ToString()
        {
            return "END";
        }

        public override NBTTagEnd Copy()
        {
            return new();
        }
    }
}
