namespace GeoForge.NBT
{
    public abstract class NBTBase
    {
        public static readonly string[] NbtTypes = ["END", "BYTE", "SHORT", "INT", "LONG", "FLOAT", "DOUBLE", "BYTE[]", "STRING", "LIST", "COMPOUND", "INT[]", "LONG[]"];

        public NBTBase()
        {
        }

        public abstract void Write(BinaryWriter writer);
        public abstract void Read(BinaryReader reader, int depth, NBTSizeTracker tracker);

        public abstract byte GetID();

        public static NBTBase? CreateNewByType(byte id)
        {
            return id switch
            {
                0 => new NBTTagEnd(),
                1 => new NBTTagByte(),
                2 => new NBTTagShort(),
                3 => new NBTTagInt(),
                4 => new NBTTagLong(),
                5 => new NBTTagFloat(),
                6 => new NBTTagDouble(),
                7 => new NBTTagByteArray(),
                8 => new NBTTagString(),
                9 => new NBTTagList(),
                10 => new NBTTagCompound(),
                11 => new NBTTagIntArray(),
                12 => new NBTTagLongArray(),
                _ => null,
            };
        }

        public static string GetTagTypeName(int id)
        {
            switch (id)
            {
                case 0:
                    return "TAG_End";

                case 1:
                    return "TAG_Byte";

                case 2:
                    return "TAG_Short";

                case 3:
                    return "TAG_Int";

                case 4:
                    return "TAG_Long";

                case 5:
                    return "TAG_Float";

                case 6:
                    return "TAG_Double";

                case 7:
                    return "TAG_Byte_Array";

                case 8:
                    return "TAG_String";

                case 9:
                    return "TAG_List";

                case 10:
                    return "TAG_Compound";

                case 11:
                    return "TAG_Int_Array";

                case 12:
                    return "TAG_Long_Array";

                case 99:
                    return "Any Numeric Tag";

                default:
                    return "UNKNOWN";
            }
        }

        public abstract NBTBase Copy();

        public virtual bool HasNoTags()
        {
            return false;
        }

        public override bool Equals(object? obj)
        {
            if (obj is NBTBase nbt)
            {
                return nbt.GetID() == GetID();
            }

            return false;
        }

        public override int GetHashCode()
        {
            return GetID();
        }

        public virtual string? GetString()
        {
            return ToString();
        }

        protected static int CalculateArrayHashCode<T>(T[] array)
        {
            var hashCode = new HashCode();
            foreach (T element in array)
            {
                hashCode.Add(element);
            }
            return hashCode.ToHashCode();
        }
    }
}
