namespace Common.SharedMemory;

public interface IAsyncDataWriter<T>
{
    Task WriteAsync(T obj, string fileName, int offset = 0);
}