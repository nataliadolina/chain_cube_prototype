using System;
using System.Linq;
using UnityEngine;

[Serializable]
public struct GenericKeyValuePair<TKey, TValue>
{
    [SerializeField]
    public TKey Key;
    [SerializeField]
    public TValue Value;

    public GenericKeyValuePair(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }
}
