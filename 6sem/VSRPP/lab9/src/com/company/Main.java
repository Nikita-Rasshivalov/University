package com.company;

import Functions.MulFunction;
import Functions.SumFunction;
import Queues.SyncQueue;
import Threads.ConsumerThread;
import Threads.FunctionThread;

public class Main {

    public static void main(String[] args) {
        var queue = new SyncQueue<Integer>(6);
        var consumer = new ConsumerThread(queue);
        var mulFunction = new FunctionThread<Integer>(new MulFunction(5, 4), queue);
        var sumFunction = new FunctionThread<Integer>(new SumFunction(7, 1), queue);
        var consumerThread = new Thread(consumer, "consumer");
        var sumFunctionThread = new Thread(sumFunction, "sumFunction");
        var mulFunctionThread = new Thread(mulFunction, "subFunction");
        sumFunctionThread.start();
        mulFunctionThread.start();
        consumerThread.start();
    }
}
