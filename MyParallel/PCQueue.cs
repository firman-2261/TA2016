using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace MyParallel
{
    public class PCQueue : IDisposable
    {
        BlockingCollection<Task> _taskQ = new BlockingCollection<Task>();

        public PCQueue(int jlhParallelThread)
        {
            //int minWorker, minIOC;
            //ThreadPool.GetMinThreads(out minWorker, out minIOC);
            //ThreadPool.SetMinThreads(jlhParallelThread, minIOC);
            for (int i = 0; i < jlhParallelThread; i++)
            {
                Task.Factory.StartNew(consume);
            }
        }

        public Task enqueue(Action action,CancellationToken cancelToken = default(CancellationToken))
        {
            var task = new Task(action, cancelToken);
            _taskQ.Add(task);
            return task;
        }

        public Task<TResult> enqueueReturn<TResult>(Func<TResult> func, CancellationToken cancelToken = default(CancellationToken))
        {
            var task = new Task<TResult>(func, cancelToken);
            _taskQ.Add(task);
            return task;
        }

        private void consume()
        {
            foreach (var task in _taskQ.GetConsumingEnumerable())
            {
                try
                {
                    if (!task.IsCanceled)
                    {
                        task.RunSynchronously();
                    }
                }
                catch (InvalidOperationException) { } // race condition
            }
        }

        public void Dispose()
        {
            _taskQ.CompleteAdding();
        }
    }
}
