namespace GeoForge.NBT
{
    public class NBTLogging
    {
        public static Action<string> Warn = Console.WriteLine;
        public static Action<string> Fatal = Console.WriteLine;
    }
}
