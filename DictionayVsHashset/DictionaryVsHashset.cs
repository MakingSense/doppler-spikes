// See http://www.pobox.com/~skeet/csharp/benchmark.html for how to run this code
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DictionaryVsHashset
{
    static int _items = 10_000_000;
    static readonly IEqualityComparer<string> _equalityComparer = StringComparer.InvariantCultureIgnoreCase;

    static KeyValuePair<string, bool>[] Source;
    static string[] SourceKeys;
    static int count;

    public static void Init(string[] args)
    {
        if (args.Length > 0)
        {
            _items = int.Parse(args[0]);
        }

        Console.WriteLine("Prepearing data . . .");
        var random = new Random();
        Source = Enumerable.Range(0, _items).Select(_ =>
            new KeyValuePair<string, bool>(Guid.NewGuid().ToString(), random.Next(0, 1) == 0)
        ).ToArray();
        SourceKeys = Source.Select(x => x.Key).ToArray();
        Console.WriteLine($"Data ready, {_items} items.");
    }

    public static void Check()
    {
        if (count != _items)
        {
            throw new Exception("Generated structure doesn't have the right size.");
        }
    }

    public static void Reset()
    {
        count = 0;
    }

    [Benchmark]
    public static void Hashset_with_constructor_with_select()
    {
        var generated = new HashSet<string>(Source.Select(x => x.Key), _equalityComparer);
        count = generated.Count;
    }

    [Benchmark]
    public static void Hashset_with_constructor_without_select()
    {
        var generated = new HashSet<string>(SourceKeys, _equalityComparer);
        count = generated.Count;
    }

    [Benchmark]
    public static void Hashset_with_Add()
    {
        var generated = new HashSet<string>(_equalityComparer);
        foreach (var item in SourceKeys)
        {
            generated.Add(item);
        }
        count = generated.Count;
    }

    [Benchmark]
    public static void Hashset_with_UnionWith()
    {
        var generated = new HashSet<string>(_equalityComparer);
        generated.UnionWith(SourceKeys);
        count = generated.Count;
    }

    [Benchmark]
    public static void Dictionary_with_Add()
    {
        var generated = new Dictionary<string, bool>(_equalityComparer);
        foreach (var item in Source)
        {
            generated.Add(item.Key, item.Value);
        }
        count = generated.Count;
    }


    [Benchmark]
    public static void Dictionary_with_Add_with_predefined_capacity()
    {
        var generated = new Dictionary<string, bool>(_items, _equalityComparer);
        foreach (var item in Source)
        {
            generated.Add(item.Key, item.Value);
        }
        count = generated.Count;
    }

    [Benchmark]
    public static void Dictionary_with_Indexer()
    {
        var generated = new Dictionary<string, bool>(_equalityComparer);
        foreach (var item in Source)
        {
            generated[item.Key] = item.Value;
        }
        count = generated.Count;
    }

    [Benchmark]
    public static void Dictionary_with_ToDictionary()
    {
        var generated = Source.ToDictionary(
            x => x.Key,
            x => x.Value,
            _equalityComparer);
        count = generated.Count;
    }
}