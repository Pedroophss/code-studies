using System;

namespace DataStructs.HashTable
{
    public class MultipleArraysHashTable<TKey, TValue>
    {
        private int LEVEL_LENGTH = 10;

        private class Entry
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Entry[] HashColision { get; set; }

            public Entry(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }

        private Entry[] Table { get; set; }

        public MultipleArraysHashTable()
        {
            Table = new Entry[LEVEL_LENGTH];
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            Add(key, value, Table);
        }

        private void Add(TKey key, TValue value, Entry[] table, int level = 0)
        {
            var hash = Math.Abs(key.GetHashCode());
            var indexGetter = (int)Math.Pow(10, level + 1);

            if (indexGetter > hash)
                throw new ArgumentOutOfRangeException();

            var index = hash % indexGetter / Math.Max(level * 10, 1);
            if (table[index] == null)
                table[index] = new Entry(key, value);

            else
            {
                var levelTable = table[index];
                if (levelTable.Key.Equals(key))
                    throw new DuplicateWaitObjectException();

                if (levelTable.HashColision == null)
                    levelTable.HashColision = new Entry[LEVEL_LENGTH];

                Add(key, value, levelTable.HashColision, ++level);
            }
        }

        public TValue Get(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            return Get(key, Table);
        }

        private TValue Get(TKey key, Entry[] table, int level = 0)
        {
            var hash = Math.Abs(key.GetHashCode());
            var indexGetter = (int)Math.Pow(10, level + 1);

            if (indexGetter > hash)
                return default;

            var index = hash % indexGetter / Math.Max(level * 10, 1);
            if (table[index] == null)
                return default;

            else
            {
                var levelTable = table[index];
                if (levelTable.Key.Equals(key))
                    return levelTable.Value;

                if (levelTable.HashColision == null)
                    return default;

                return Get(key, levelTable.HashColision, ++level);
            }
        }
    }
}
