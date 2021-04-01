using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace ConcurrentAplication
{
    class Program
    {
        public static Queue<int> genericQueue = new Queue<int>();
        public static ConcurrentQueue<int> concurrentQueue = new ConcurrentQueue<int>();

        public static void EnqueueNotConcurrent(){
            for (int i = 0; i < 100; i++)
            {
                genericQueue.Enqueue(i);
                Thread.Sleep(100);
            }
        }

        public static void EnqueueConcurrent()
        {
            for (int i = 0; i < 100; i++)
            {
                concurrentQueue.Enqueue(i);
                Thread.Sleep(100);
            }
        }

        static void Main(string[] args)
        {
            Thread threadNotConcurrent = new Thread(new ThreadStart(EnqueueNotConcurrent));
            threadNotConcurrent.Start();
            // Take out comment below to try genericQueuePeek
            // Console.WriteLine("Generic Peek first " + genericQueue.Peek());

            Thread threadConcurrent = new Thread(new ThreadStart(EnqueueConcurrent));
            threadConcurrent.Start();
            int result;
            if (concurrentQueue.TryPeek(out result))
            {
                Console.WriteLine("Concurrent Peek first " + result);
            }else
            {
                Console.WriteLine("Concurrent Not peeked ");
            }
        }
    }
}
