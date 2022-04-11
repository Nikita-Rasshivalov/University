package Threads;

import Functions.Function;
import Queues.SyncQueue;

public class ConsumerThread implements Runnable {
    private SyncQueue<Integer> _queue;

    public ConsumerThread(SyncQueue<Integer> queue) {
        _queue = queue;
    }

    @Override
    public void run() {
        while (true) {
            System.out.println("Result:" + _queue.get());
        }
    }
}
