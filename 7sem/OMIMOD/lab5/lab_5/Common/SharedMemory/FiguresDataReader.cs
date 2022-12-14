using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace Common.SharedMemory;

public class FiguresDataReader : IAsyncDataReader<ImageFigures>
{
    public async Task<ImageFigures> ReadAsync(string fileName, int offset = 0)
    {
        if (fileName == null) throw new ArgumentNullException(nameof(fileName));

        ImageFigures? data = null;
        int int32Capacity = Marshal.SizeOf(typeof(Int32));
        var mmf = MemoryMappedFile.OpenExisting(fileName);
        int size;
        using (var accessor = mmf.CreateViewAccessor(0, int32Capacity, MemoryMappedFileAccess.Read))
        {
            accessor.Read(0, out size);
        }
        using (var accessor = mmf.CreateViewAccessor(int32Capacity, size, MemoryMappedFileAccess.Read))
        {
            var byteArray = new byte[size];
            accessor.ReadArray(0, byteArray, 0, size);
            data = FigureDataConverter.ByteArrayToObject(byteArray);
        }

        return data;
    }
}