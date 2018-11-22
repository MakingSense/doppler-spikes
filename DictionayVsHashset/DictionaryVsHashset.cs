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
}