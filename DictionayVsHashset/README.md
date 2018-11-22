# Dictionary vs. Hashset

This is a benchmark project to verify the performance difference between using .NET _Hashset<string>_ or _Dictionary<string, bool>_.

Obviously, both structures are different, but in theory it should not be big performance differences creating them and neither accessing items. Since there was an issue, we want to validate that hypothesis.

Thanks @jskeet, we are using his _[micro-benchmarks micro-framework](http://www.pobox.com/~skeet/csharp/benchmark.html)_.

### Example

```bash
$ ./DictionayVsHashset.exe -version -runtwice -pause 10000000

Operating System: Microsoft Windows NT 6.2.9200.0
Runtime version: 4.0.30319.42000
Benchmarking type DictionaryVsHashset
Prepearing data . . .
Data ready, 10000000 items.
Run #1
  Hashset_with_constructor_with_select 00:00:20.1050123
  Hashset_with_constructor_without_select 00:00:19.2730098
  Hashset_with_Add     00:00:19.4835150
  Hashset_with_UnionWith 00:00:19.3709982
  Dictionary_with_Add  00:00:19.6131113
  Dictionary_with_Add_with_predefined_capacity 00:00:18.5786162
  Dictionary_with_Indexer 00:00:19.5450038
  Dictionary_with_ToDictionary 00:00:19.7909977
Run #2
  Hashset_with_constructor_with_select 00:00:19.5120046
  Hashset_with_constructor_without_select 00:00:18.4909928
  Hashset_with_Add     00:00:19.3271103
  Hashset_with_UnionWith 00:00:20.7540945
  Dictionary_with_Add  00:00:19.5980020
  Dictionary_with_Add_with_predefined_capacity 00:00:18.1024183
  Dictionary_with_Indexer 00:00:18.9190028
  Dictionary_with_ToDictionary 00:00:21.8258926
Press ENTER to continue . . .
```

### Conclusion

There are not surprises, performance is virtually the same for all cases, lightly better using dictionaries with predefined capacity.


