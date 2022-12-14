namespace Common;

public class SynchronizedQueue<T> where T : class
{
    private Mutex _mutex = new();
    private static int queueResource = 0;
    private Queue<T> _storageQueue = new Queue<T>();
    private bool _isActive = true;

    public void Enqueue(T item)
    {
        try
        {
            _mutex.WaitOne();

            if (!_isActive)
            {
                throw new InvalidOperationException("Storage is not active and new items cannot be added.");
            }
            _storageQueue.Enqueue(item);
        }
        finally
        {
            _mutex.ReleaseMutex();
        }
    }

    public T Dequeue()
    {
        try
        {
            _mutex.WaitOne();

            if (_storageQueue.Count == 0)
            {
                return null;
            }

            return _storageQueue.Dequeue();
        }
        finally
        {
            _mutex.ReleaseMutex();
        }
    }

    public void Stop()
    {
        try
        {
            _mutex.WaitOne();

            _isActive = false;
        }
        finally
        {
            _mutex.ReleaseMutex();
        }
    }

    public bool IsActive
    {
        get
        {
            try
            {
                _mutex.WaitOne();

                return _isActive;
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }
    }
}