using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace Common.SharedMemory;

public class IntWriter : IAsyncDataWriter<int>
{
    public async Task WriteAsync(int obj, string fileName, int offset = 0)
    {
        if (fileName == null) throw new ArgumentNullException(nameof(fileName));

        var capacity = Marshal.SizeOf(typeof(int));
        var mmf = MemoryMappedFile.CreateOrOpen(fileName, capacity + capacity * offset);
        using var accessor = mmf.CreateViewAccessor(offset, capacity, MemoryMappedFileAccess.Write);
        await Task.Run(() => accessor.Write(0, obj));
    }
}