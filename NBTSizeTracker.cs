namespace GeoForge.NBT
{
    public class NBTSizeTracker(long max)
    {
        public static readonly NBTSizeTracker Infinite = new InfiniteSizeTracker();

        private class InfiniteSizeTracker : NBTSizeTracker
        {
            public InfiniteSizeTracker() : base(0L)
            {
            }

            public override void Read(long bits)
            {
            }
        }

        private readonly long _max = max;
        private long _read = 0;

        public virtual void Read(long bits)
        {
            _read += bits / 8L;

            if (_read > _max)
            {
                throw new Exception($"Tried to read NBT tag that was too big; tried to allocated: {_read}bytes where max allowed: {_max}");
            }
        }
    }
}
