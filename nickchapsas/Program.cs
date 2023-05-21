using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

var random = new Random(420);
var items = Enumerable.Range(0, 100).Select(x => random.Next()).ToList();

Span<int> listAsSpan = CollectionsMarshal.AsSpan(items);
ref var searchSpace = ref MemoryMarshal.GetReference(listAsSpan);
for (var i = 0; i<listAsSpan.Length; i++){
    var item = Unsafe.Add(ref searchSpace, i);
    Console.WriteLine(item);
}

record Wrapper(int Number, string txt);