namespace GeoForge.NBT
{
    public class CompressedStreamTools
    {
        public static NBTTagCompound? Read(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            else
            {
                BinaryReader reader = new(new FileStream(filePath, FileMode.Open));
                NBTTagCompound nbtTagCompound;

                try
                {
                    nbtTagCompound = Read(reader);
                }
                finally
                {
                    reader.Close();
                }

                return nbtTagCompound;
            }
        }

        public static NBTTagCompound Read(BinaryReader reader)
        {
            return Read(reader, NBTSizeTracker.Infinite);
        }

        public static NBTTagCompound Read(BinaryReader reader, NBTSizeTracker tracker)
        {
            NBTBase nbtBase = Read(reader, 0, tracker);

            if (nbtBase is NBTTagCompound nbtTagCompound)
            {
                return nbtTagCompound;
            }
            else
            {
                throw new IOException("Root tag must be a named compound tag");
            }
        }

        private static NBTBase Read(BinaryReader reader, int depth, NBTSizeTracker tracker)
        {
            byte b = reader.ReadByte();

            if (b == 0)
            {
                return new NBTTagEnd();
            }
            else
            {
                reader.ReadString();
                NBTBase? nbtBase = NBTBase.CreateNewByType(b);

                try
                {
                    nbtBase!.Read(reader, depth, tracker);
                    return nbtBase;
                }
                catch (Exception e)
                {
                    NBTLogging.Fatal(e.ToString());
                    throw;
                }
            }
        }
    }
}
