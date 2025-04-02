using System.Text;

namespace GeoForge.NBT
{
    public class NBTTagString : NBTBase
    {
        private string _data;

        public NBTTagString()
        {
        }

        public NBTTagString(string data)
        {
            _data = data;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(_data);
        }

        public override void Read(BinaryReader reader, int depth, NBTSizeTracker tracker)
        {
            tracker.Read(288L);
            _data = reader.ReadString();
            tracker.Read(16 * _data.Length);
        }

        public override byte GetID()
        {
            return 8;
        }

        public override string ToString()
        {
            return QuoteAndEscape(_data);
        }

        public override NBTTagString Copy()
        {
            return new(_data);
        }

        public override bool HasNoTags()
        {
            return _data.Length == 0;
        }

        public override bool Equals(object? obj)
        {
            if (!base.Equals(obj))
            {
                return false;
            }
            else
            {
                NBTTagString nbt = (NBTTagString)obj;
                return _data == null && nbt._data == null || _data == nbt._data;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ _data.GetHashCode();
        }

        public string GetString()
        {
            return _data;
        }

        public static string QuoteAndEscape(string str)
        {
            StringBuilder stringBuilder = new("\"");

            for (int i = 0; i < str.Length; i++)
            {
                char chr = str[i];

                if (chr == '\\' || chr == '"')
                {
                    stringBuilder.Append('\\');
                }

                stringBuilder.Append(chr);
            }

            return stringBuilder.Append('"').ToString();
        }
    }
}
