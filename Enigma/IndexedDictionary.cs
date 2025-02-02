// ReSharper disable MemberCanBePrivate.Global

namespace Enigma;

public sealed class IndexedDictionary<TKey, TValue> where TKey : notnull where TValue : notnull
{
    public readonly Dictionary<TKey, int> KeyIndex = [];
    public readonly Dictionary<TValue, int> ValueIndex = [];
    public readonly List<(TKey Key, TValue Value)> KeyValues = [];

    public int Count => KeyValues.Count;

    public void Clear()
    {
        KeyIndex.Clear();
        ValueIndex.Clear();
        KeyValues.Clear();
    }

    public void Add(TKey key, TValue value)
    {
        if (KeyIndex.ContainsKey(key))
            throw new ArgumentException("Key already exists", nameof(key));

        if (ValueIndex.ContainsKey(value))
            throw new ArgumentException("Value already exists", nameof(value));

        KeyValues.Add((key, value));

        var idx = KeyValues.Count - 1;

        KeyIndex[key] = idx;
        ValueIndex[value] = idx;
    }

    public void AddRange(Dictionary<TKey, TValue> dictionary, int ringPosition = 0)
    {
        if (ringPosition <= 0 || ringPosition >= dictionary.Count)
        {
            foreach (var kvp in dictionary)
                Add(kvp.Key, kvp.Value);
        }
        else
        {
            var counter = ringPosition;

            foreach (var kvp in dictionary)
            {
                Add(kvp.Key, dictionary.Skip(counter).First().Value);
                    
                counter++;

                if (counter >= dictionary.Count)
                    counter = 0;
            }
        }
    }

    public bool TryAdd(TKey key, TValue value)
    {
        if (KeyIndex.ContainsKey(key) || ValueIndex.ContainsKey(value))
            return false;
        
        KeyValues.Add((key, value));

        var idx = KeyValues.Count - 1;

        KeyIndex[key] = idx;
        ValueIndex[value] = idx;

        return true;
    }

    public TValue this[TKey key]
    {
        get
        {
            var index = KeyIndex[key];
            return KeyValues[index].Value;
        }
        set
        {
            var index = KeyIndex[key];
            var oldValue = KeyValues[index].Value;

            if (EqualityComparer<TValue>.Default.Equals(oldValue, value) == false)
            {
                if (ValueIndex.ContainsKey(value))
                    throw new ArgumentException("Another entry already has this value.", nameof(value));

                ValueIndex.Remove(oldValue);
                KeyValues[index] = (key, value);
                ValueIndex[value] = index;
            }
            else
            {
                KeyValues[index] = (key, value);
            }
        }
    }

    public TValue this[int index]
    {
        get => KeyValues[index].Value;
        set
        {
            var (oldKey, oldValue) = KeyValues[index];

            if (EqualityComparer<TValue>.Default.Equals(oldValue, value) == false)
            {
                if (ValueIndex.ContainsKey(value))
                    throw new ArgumentException("Another entry already has this value.", nameof(value));

                ValueIndex.Remove(oldValue);
                KeyValues[index] = (oldKey, value);
                ValueIndex[value] = index;
            }
            else
            {
                KeyValues[index] = (oldKey, value);
            }
        }
    }

    public int IndexOfKey(TKey key)
    {
        return KeyIndex.GetValueOrDefault(key, -1);
    }

    public int IndexOfValue(TValue value)
    {
        return ValueIndex.GetValueOrDefault(value, -1);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        if (KeyIndex.TryGetValue(key, out var index))
        {
            value = KeyValues[index].Value;
            return true;
        }

        value = default!;

        return false;
    }

    public bool TryGetKey(TValue value, out TKey key)
    {
        if (ValueIndex.TryGetValue(value, out var index))
        {
            key = KeyValues[index].Key;
            return true;
        }

        key = default!;

        return false;
    }

    public bool Remove(TKey key)
    {
        if (KeyIndex.TryGetValue(key, out var index) == false)
            return false;

        var (existingKey, existingValue) = KeyValues[index];

        KeyIndex.Remove(existingKey);
        ValueIndex.Remove(existingValue);

        var lastIndex = KeyValues.Count - 1;

        if (index == lastIndex)
        {
            KeyValues.RemoveAt(lastIndex);
            return true;
        }

        var (movedKey, movedValue) = KeyValues[lastIndex];

        KeyValues[index] = (movedKey, movedValue);
        KeyIndex[movedKey] = index;
        ValueIndex[movedValue] = index;

        KeyValues.RemoveAt(lastIndex);

        return true;
    }
}