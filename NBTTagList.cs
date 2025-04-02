using System.Text;

namespace GeoForge.NBT
{
    public class NBTTagList : NBTBase
    {
        private List<NBTBase> _tagList = [];

        private byte _tagType = 0;

        public override void Write(BinaryWriter writer)
        {
            if (_tagList.Count == 0)
            {
                _tagType = 0;
            }
            else
            {
                _tagType = _tagList[0].GetID();
            }

            writer.Write(_tagType);
            writer.Write(_tagList.Count);

            for (int i = 0; i < _tagList.Count; i++)
            {
                _tagList[i].Write(writer);
            }
        }

        public override void Read(BinaryReader reader, int depth, NBTSizeTracker tracker)
        {
            tracker.Read(296L);

            if (depth > 512)
            {
                throw new Exception("Tried to read NBT tag with too high complexity, depth > 512");
            }
            else
            {
                _tagType = reader.ReadByte();
                int count = reader.ReadInt32();

                if (_tagType == 0 && count > 0)
                {
                    throw new Exception("Missing type on ListTag");
                }
                else
                {
                    tracker.Read(32L * count);
                    _tagList = new List<NBTBase>(count);

                    for (int i = 0; i < count; i++)
                    {
                        NBTBase nbt = CreateNewByType(_tagType)!;
                        nbt.Read(reader, depth + 1, tracker);
                        _tagList.Add(nbt);
                    }
                }
            }
        }

        public override byte GetID()
        {
            return 9;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new("[");

            for (int i = 0; i < _tagList.Count; i++)
            {
                if (i != 0)
                {
                    stringBuilder.Append(',');
                }

                stringBuilder.Append(_tagList[i]);
            }

            return stringBuilder.Append(']').ToString();
        }

        public void AppendTag(NBTBase nbt)
        {
            if (nbt.GetID() == 0)
            {
                NBTLogging.Warn("Invalid TagEnd added to ListTag");
            }
            else
            {
                if (_tagType == 0)
                {
                    _tagType = nbt.GetID();
                }
                else if (_tagType != nbt.GetID())
                {
                    NBTLogging.Warn("Adding mismatched tag types to tag list");
                    return;
                }

                _tagList.Add(nbt);
            }
        }

        public void Set(int index, NBTBase nbt)
        {
            if (nbt.GetID() == 0)
            {
                NBTLogging.Warn("Invalid TagEnd added to ListTag");
            }
            else if (index >= 0 && index < _tagList.Count)
            {
                if (_tagType == 0)
                {
                    _tagType = nbt.GetID();
                }
                else if (_tagType != nbt.GetID())
                {
                    NBTLogging.Warn("Adding mismatched tag types to tag list");
                    return;
                }

                _tagList.Insert(index, nbt);
            }
            else
            {
                NBTLogging.Warn("index out of bounds to set tag in tag list");
            }
        }

        public NBTBase? RemoveTag(int index)
        {
            NBTBase? value = null;
            try
            {
                value = _tagList[index];
                _tagList.RemoveAt(index);
            }
            catch (Exception)
            {
            }

            return value;
        }

        public override bool HasNoTags()
        {
            return _tagList.Count == 0;
        }

        public NBTTagCompound GetCompoundTagAt(int index)
        {
            if (index >= 0 && index < _tagList.Count)
            {
                NBTBase nbtbase = _tagList[index];

                if (nbtbase.GetID() == 10)
                {
                    return (NBTTagCompound)nbtbase;
                }
            }

            return new();
        }

        public int GetIntAt(int index)
        {
            if (index >= 0 && index < _tagList.Count)
            {
                NBTBase nbtbase = _tagList[index];

                if (nbtbase.GetID() == 3)
                {
                    return ((NBTTagInt)nbtbase).GetInt();
                }
            }

            return 0;
        }

        public int[] GetIntArrayAt(int index)
        {
            if (index >= 0 && index < _tagList.Count)
            {
                NBTBase nbtbase = _tagList[index];

                if (nbtbase.GetID() == 11)
                {
                    return ((NBTTagIntArray)nbtbase).GetIntArray();
                }
            }

            return [];
        }

        public double GetDoubleAt(int index)
        {
            if (index >= 0 && index < _tagList.Count)
            {
                NBTBase nbtbase = _tagList[index];

                if (nbtbase.GetID() == 6)
                {
                    return ((NBTTagDouble)nbtbase).GetDouble();
                }
            }

            return 0.0D;
        }

        public float GetFloatAt(int index)
        {
            if (index >= 0 && index < _tagList.Count)
            {
                NBTBase nbtbase = _tagList[index];

                if (nbtbase.GetID() == 5)
                {
                    return ((NBTTagFloat)nbtbase).GetFloat();
                }
            }

            return 0.0f;
        }

        public string GetStringTagAt(int index)
        {
            if (index >= 0 && index < _tagList.Count)
            {
                NBTBase nbtbase = _tagList[index];
                return nbtbase.GetID() == 8 ? nbtbase.GetString()! : nbtbase.ToString()!;
            }
            else
            {
                return "";
            }
        }

        public NBTBase Get(int index)
        {
            return index >= 0 && index < _tagList.Count ? _tagList[index] : new NBTTagEnd();
        }

        public int TagCount()
        {
            return _tagList.Count;
        }

        public override NBTTagList Copy()
        {
            NBTTagList nbtTagList = new()
            {
                _tagType = _tagType
            };

            foreach (NBTBase nbtBase in _tagList)
            {
                NBTBase nbtBaseCopy = nbtBase.Copy();
                nbtTagList._tagList.Add(nbtBaseCopy);
            }

            return nbtTagList;
        }

        public override bool Equals(object? obj)
        {
            if (!base.Equals(obj))
            {
                return false;
            }
            else
            {
                NBTTagList nbtTagList = (NBTTagList)obj;
                return _tagType == nbtTagList._tagType && _tagList.SequenceEqual(nbtTagList._tagList);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ _tagList.GetHashCode();
        }

        public int GetTagType()
        {
            return _tagType;
        }
    }
}
