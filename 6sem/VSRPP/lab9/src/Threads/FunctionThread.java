package Threads;

import Functions.Function;
import Queues.SyncQueue;

import javax.management.InvalidApplicationException;

public class FunctionThread<T> implements Runnable {
    public Function<T> _function;
    private SyncQueue<T> _queue;

    public FunctionThread(Function<T> function, SyncQueue<T> queue) {
        _function = function;
        _queue = queue;
    }


    @Override
    public void run() {
        while (_function.hasNext()) {
            T result = null;
            try {
                result = _function.execute();
            } catch (InvalidApplicationException e) {
                e.printStackTrace();
            }
            _queue.put(result);
            System.out.println("Put result from " + _function.getName());
        }
    }
}
