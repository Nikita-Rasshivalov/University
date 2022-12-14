using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace Common.SharedMemory;

public class IntReader : IAsyncDataReader<int>
{
    public async Task<int> ReadAsync(string fileName, int offset = 0)
    {
        if (fileName == null) throw new ArgumentNullException(nameof(fileName));

        int result = default;
        var capacity = Marshal.SizeOf(typeof(int));
        var mmf = MemoryMappedFile.OpenExisting(fileName);
        using var accessor = mmf.CreateViewAccessor(offset, capacity, MemoryMappedFileAccess.Read);
        accessor.Read(0, out result);
        return result;
    }
}