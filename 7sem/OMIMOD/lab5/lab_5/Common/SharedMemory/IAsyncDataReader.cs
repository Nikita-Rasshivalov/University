namespace Common.SharedMemory;

public interface IAsyncDataReader<T>
{
    Task<T> ReadAsync(string fileName, int offset = 0);
}