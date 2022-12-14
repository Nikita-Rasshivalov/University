using Common;
using Common.SharedMemory;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace Common.SharedMemory;

public class FiguresDataWriter : IAsyncDataWriter<ImageFigures>
{
    public async Task WriteAsync(ImageFigures obj, string fileName, int offset = 0)
    {
        if (obj == null) throw new ArgumentNullException(nameof(obj));
        if (fileName == null) throw new ArgumentNullException(nameof(fileName));

        await Task.Run(() =>
        {
            var int32Capacity = Marshal.SizeOf(typeof(Int32));
            var array = FigureDataConverter.ObjectToByteArray(obj);
            var arrayCapacity = array.Length;
            var mmf = MemoryMappedFile.CreateOrOpen(fileName, int32Capacity + arrayCapacity);
            using var accessor = mmf.CreateViewAccessor(0, int32Capacity + arrayCapacity, MemoryMappedFileAccess.Write);
            accessor.Write(0, array.Length);
            accessor.WriteArray(int32Capacity, array, 0, array.Length);
        });
    }
}