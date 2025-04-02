using System.Text;
using System.Text.RegularExpressions;

namespace GeoForge.NBT
{
    public class NBTTagCompound : NBTBase
    {
        private static readonly Regex SimpleValue = new Regex(@"[A-Za-z0-9._+-]+", RegexOptions.Compiled);
        private readonly Dictionary<string, NBTBase> _tagMap = [];

        public override void Write(BinaryWriter writer)
        {
            foreach (string s in _tagMap.Keys)
            {
                NBTBase nbt = _tagMap[s];
                WriteEntry(s, nbt, writer);
            }

            writer.Write((byte)0);
        }

        public override void Read(BinaryReader reader, int depth, NBTSizeTracker tracker)
        {
            tracker.Read(384L);

            if (depth > 512)
            {
                throw new Exception("Tried to read NBT tag with too high complexity, depth > 512");
            }
            else
            {
                _tagMap.Clear();
                byte b;

                while ((b = ReadType(reader)) != 0)
                {
                    string s = ReadKey(reader);
                    tracker.Read(224 + 16 * s.Length);
                    NBTBase nbtBase = ReadNBT(b, reader, depth + 1, tracker);

                    if (_tagMap.TryAdd(s, nbtBase))
                    {
                        tracker.Read(288L);
                    }
                }
            }
        }

        public Dictionary<string, NBTBase>.KeyCollection GetKeys()
        {
            return _tagMap.Keys;
        }

        public override byte GetID()
        {
            return 10;
        }

        public int GetSize()
        {
            return _tagMap.Count;
        }

        public void SetTag(string key, NBTBase value)
        {
            _tagMap[key] = value;
        }

        public void SetByte(string key, byte value)
        {
            _tagMap[key] = new NBTTagByte(value);
        }

        public void SetShort(string key, short value)
        {
            _tagMap[key] = new NBTTagShort(value);
        }

        public void SetInteger(string key, int value)
        {
            _tagMap[key] = new NBTTagInt(value);
        }

        public void SetLong(string key, long value)
        {
            _tagMap[key] = new NBTTagLong(value);
        }

        public void SetFloat(string key, float value)
        {
            _tagMap[key] = new NBTTagFloat(value);
        }

        public void SetDouble(string key, double value)
        {
            _tagMap[key] = new NBTTagDouble(value);
        }

        public void SetString(string key, string value)
        {
            _tagMap[key] = new NBTTagString(value);
        }

        public void SetByteArray(string key, byte[] value)
        {
            _tagMap[key] = new NBTTagByteArray(value);
        }

        public void SetIntArray(string key, int[] value)
        {
            _tagMap[key] = new NBTTagIntArray(value);
        }

        public void SetBoolean(string key, bool value)
        {
            SetByte(key, (byte)(value ? 1 : 0));
        }

        public NBTBase GetTag(string key)
        {
            return _tagMap[key];
        }

        public byte GetTagId(string key)
        {
            _tagMap.TryGetValue(key, out NBTBase? nbtbase);
            return nbtbase == null ? (byte)0 : nbtbase.GetID();
        }

        public bool HasKey(string key)
        {
            return _tagMap.ContainsKey(key);
        }

        public bool HasKey(string key, int type)
        {
            int i = GetTagId(key);

            if (i == type)
            {
                return true;
            }
            else if (type != 99)
            {
                return false;
            }
            else
            {
                return i == 1 || i == 2 || i == 3 || i == 4 || i == 5 || i == 6;
            }
        }

        public byte GetByte(string key)
        {
            try
            {
                if (HasKey(key, 99))
                {
                    return ((NBTPrimitive)_tagMap[key]).GetByte();
                }
            }
            catch (InvalidCastException)
            {
            }

            return 0;
        }

        public short GetShort(string key)
        {
            try
            {
                if (HasKey(key, 99))
                {
                    return ((NBTPrimitive)_tagMap[key]).GetShort();
                }
            }
            catch (InvalidCastException)
            {
            }

            return 0;
        }

        public int GetInteger(string key)
        {
            try
            {
                if (HasKey(key, 99))
                {
                    return ((NBTPrimitive)_tagMap[key]).GetInt();
                }
            }
            catch (InvalidCastException)
            {
            }

            return 0;
        }

        public long GetLong(string key)
        {
            try
            {
                if (HasKey(key, 99))
                {
                    return ((NBTPrimitive)_tagMap[key]).GetLong();
                }
            }
            catch (InvalidCastException)
            {
            }

            return 0L;
        }

        public float GetFloat(string key)
        {
            try
            {
                if (HasKey(key, 99))
                {
                    return ((NBTPrimitive)_tagMap[key]).GetFloat();
                }
            }
            catch (InvalidCastException)
            {
            }

            return 0.0F;
        }

        public double GetDouble(string key)
        {
            try
            {
                if (HasKey(key, 99))
                {
                    return ((NBTPrimitive)_tagMap[key]).GetDouble();
                }
            }
            catch (InvalidCastException)
            {
            }

            return 0.0D;
        }

        public string GetString(string key)
        {
            if (HasKey(key, 8))
            {
                return _tagMap[key].GetString()!;
            }

            return "";
        }

        public byte[] GetByteArray(string key)
        {
            try
            {
                if (HasKey(key, 7))
                {
                    return ((NBTTagByteArray)_tagMap[key]).GetByteArray();
                }
            }
            catch (InvalidCastException e)
            {
                NBTLogging.Fatal(e.ToString());
                throw;
            }

            return [];
        }

        public int[] GetIntArray(string key)
        {
            try
            {
                if (HasKey(key, 11))
                {
                    return ((NBTTagIntArray)_tagMap[key]).GetIntArray();
                }
            }
            catch (InvalidCastException e)
            {
                NBTLogging.Fatal(e.ToString());
                throw;
            }

            return [];
        }

        public NBTTagCompound GetCompoundTag(string key)
        {
            try
            {
                if (HasKey(key, 10))
                {
                    return (NBTTagCompound)_tagMap[key];
                }
            }
            catch (InvalidCastException e)
            {
                NBTLogging.Fatal(e.ToString());
                throw;
            }

            return new NBTTagCompound();
        }

        public NBTTagList GetTagList(string key, int type)
        {
            try
            {
                if (GetTagId(key) == 9)
                {
                    NBTTagList nbttaglist = (NBTTagList)_tagMap[key];

                    if (!nbttaglist.HasNoTags() && nbttaglist.GetTagType() != type)
                    {
                        return new NBTTagList();
                    }

                    return nbttaglist;
                }
            }
            catch (InvalidCastException e)
            {
                NBTLogging.Fatal(e.ToString());
                throw;
            }

            return new NBTTagList();
        }

        public bool GetBoolean(string key)
        {
            return GetByte(key) != 0;
        }

        public void RemoveTag(string key)
        {
            _tagMap.Remove(key);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new("{");
            IEnumerable<string> strings = _tagMap.Keys;

            foreach (string s in strings)
            {
                if (stringBuilder.Length != 1)
                {
                    stringBuilder.Append(',');
                }

                stringBuilder.Append(HandleEscape(s)).Append(':').Append(_tagMap[s]);
            }

            return stringBuilder.Append('}').ToString();
        }

        public override bool HasNoTags()
        {
            return _tagMap.Count == 0;
        }

        public override NBTTagCompound Copy()
        {
            NBTTagCompound nbtTagCompound = new();

            foreach (string s in _tagMap.Keys)
            {
                nbtTagCompound.SetTag(s, _tagMap[s].Copy());
            }

            return nbtTagCompound;
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj) && _tagMap.Values.SequenceEqual(((NBTTagCompound)obj)._tagMap.Values);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ _tagMap.GetHashCode();
        }

        private static void WriteEntry(string name, NBTBase data, BinaryWriter writer)
        {
            writer.Write(data.GetID());

            if (data.GetID() != 0)
            {
                writer.Write(name);
                data.Write(writer);
            }
        }

        private static byte ReadType(BinaryReader reader)
        {
            return reader.ReadByte();
        }

        private static string ReadKey(BinaryReader reader)
        {
            return reader.ReadString();
        }

        static NBTBase ReadNBT(byte id, BinaryReader reader, int depth, NBTSizeTracker tracker)
        {
            NBTBase? nbtBase = CreateNewByType(id);

            try
            {
                nbtBase!.Read(reader, depth, tracker);
            }
            catch (Exception e)
            {
                NBTLogging.Fatal(e.ToString());
                throw;
            }

            return nbtBase;
        }

        public void Merge(NBTTagCompound other)
        {
            foreach (string s in other._tagMap.Keys)
            {
                NBTBase nbtBase = other._tagMap[s];

                if (nbtBase.GetID() == 10)
                {
                    if (HasKey(s, 10))
                    {
                        NBTTagCompound nbtTagCompound = GetCompoundTag(s);
                        nbtTagCompound.Merge((NBTTagCompound)nbtBase);
                    }
                    else
                    {
                        SetTag(s, nbtBase.Copy());
                    }
                }
                else
                {
                    SetTag(s, nbtBase.Copy());
                }
            }
        }

        protected static string HandleEscape(string str)
        {
            return SimpleValue.Match(str).Success ? str : NBTTagString.QuoteAndEscape(str);
        }
    }
}
