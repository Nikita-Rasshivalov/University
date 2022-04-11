package Queues;

import java.util.ArrayDeque;
import java.util.Queue;

public class SyncQueue<T> {
    private int _maxLength;
    private Queue<T> _queue = new ArrayDeque<T>();

    public SyncQueue(int maxLength) {
        _maxLength = maxLength;
    }

    public T get() {
        while (true) {
            synchronized (_queue) {
                if (_queue.size() != 0) {
                    System.out.println("get item");
                    return _queue.poll();
                }
            }
        }
    }

    public synchronized boolean hasNext() {
        if (_queue.size() > 0) {
            return true;
        }

        return false;
    }


    public void put(T item) {
        while (true) {
            synchronized (_queue) {
                if (_queue.size() < _maxLength) {
                    System.out.println("put item");
                    _queue.add(item);
                    return;
                }
            }
        }
    }
}
