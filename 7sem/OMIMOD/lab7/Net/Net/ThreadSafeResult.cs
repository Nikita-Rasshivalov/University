namespace Net
{
    public class ThreadSafeResult
    {
        private Semaphore _semaphore = new Semaphore(1, 1);
        private double _result;

        public double GetAndClear()
        {
            _semaphore.WaitOne();

            double result = _result;
            _result = 0.0;

            _semaphore.Release();

            return result;
        }

        public void Push(double value)
        {
            _semaphore.WaitOne();

            _result += value;

            _semaphore.Release();
        }
    }
}
