using System;
using System.Collections.Concurrent;
using System.Threading;
using PlayWell.Core;

namespace Playwell.Messaging
{

    /// <summary>
    /// Asynchronous message processing agent. Encapsulates a message queue with a consuming Thread.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public sealed class ThreadBackedAgent<T, U> : IDisposable
    {

        private enum ExecutionState
        {
            NotStarted,
            Starting,
            Running,
            Stopping,
            Stopped
        }

        private readonly BlockingCollection<U> _mailbox;
        private readonly Func<T, U, T> _messageHandler;
        private readonly Thread _thread;

        private readonly object _lockExecutionState = new object();
        private ExecutionState _executionState = ExecutionState.NotStarted;

        private readonly ManualResetEventSlim _shutdownEvent = new ManualResetEventSlim(false);

        /// <summary>
        /// Creates an instance of ThreadBackedAgent with unbounded capacity and default initial state.
        /// </summary>
        /// <param name="messageHandler"></param>
        public ThreadBackedAgent(Func<T, U, T> messageHandler)
            : this(messageHandler, default(T))
        {
        }

        /// <summary>
        /// Creates an instance of ThreadBackedAgent with unbounded capacity and <paramref name="initialState"/>.
        /// </summary>
        /// <param name="messageHandler"></param>
        /// <param name="initialState"></param>
        public ThreadBackedAgent(Func<T, U, T> messageHandler, T initialState)
            : this(messageHandler, initialState, null)
        {
        }

        /// <summary>
        /// Creates an instance of ThreadBackedAgent with <paramref name="maximumCapacity"/> and <paramref name="initialState"/>.
        /// </summary>
        /// <param name="messageHandler"></param>
        /// <param name="initialState"></param>
        /// <param name="maximumCapacity"></param>
        public ThreadBackedAgent(Func<T, U, T> messageHandler, T initialState, int maximumCapacity)
            : this(messageHandler, initialState, (int?)maximumCapacity)
        {
        }

        private ThreadBackedAgent(Func<T, U, T> messageHandler, T initialState, int? maximumCapacity)
        {
            _mailbox =
                maximumCapacity.HasValue
                    ? new BlockingCollection<U>(new ConcurrentQueue<U>(), maximumCapacity.Value)
                    : new BlockingCollection<U>(new ConcurrentQueue<U>());

            _messageHandler = messageHandler.ThrowIfNull(nameof(messageHandler));
            Value = initialState;

            _thread = new Thread(Go);
        }

        /// <summary>
        /// Starts the agent. Queued items will be processed.
        /// </summary>
        public void Start()
        {
            if (_executionState == ExecutionState.NotStarted)
            {
                lock (_lockExecutionState)
                {
                    if (_executionState == ExecutionState.NotStarted)
                    {
                        _executionState = ExecutionState.Starting;
                        _thread.Start();
                        _executionState = ExecutionState.Running;
                    }
                }
            }
        }

        /// <summary>
        /// Stops the agent. Calling threads will block until all queued items have completed processing.
        /// </summary>
        public void Stop()
        {
            if (_executionState == ExecutionState.NotStarted
                ||_executionState == ExecutionState.Running)
            {
                lock (_lockExecutionState)
                {
                    if (_executionState == ExecutionState.NotStarted)
                    {
                        _mailbox.CompleteAdding();
                        _executionState = ExecutionState.Stopped;
                    }
                    else if (_executionState == ExecutionState.Running)
                    {
                        _executionState = ExecutionState.Stopping;
                        _mailbox.CompleteAdding();
                        _shutdownEvent.Wait();
                        _executionState = ExecutionState.Stopped;
                    }
                }
            }
        }

        private void Go()
        {
            foreach (var item in _mailbox.GetConsumingEnumerable())
            {
                try
                {
                    var nextValue = _messageHandler(Value, item);
                    Value = nextValue;
                }
                catch (Exception exception)
                {
                    //todo notify of errors
                    Console.WriteLine(exception);
                }
            }

            _shutdownEvent.Set();
        }

        /// <summary>
        /// Enqueues an item to be processed. 
        /// If a capacity was specified, calling threads may block until space is available. 
        /// </summary>
        /// <param name="item"></param>
        public void Send(U item)
        {
            _mailbox.Add(item);
        }

        /// <summary>
        /// Enqueues an item to be processed. 
        /// If a capacity was specified, calling threads may block until space is available. 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="cancellationToken"></param>
        public void Send(U item, CancellationToken cancellationToken)
        {
            _mailbox.Add(item, cancellationToken);
        }

        /// <summary>
        /// Attempts to enqueue an item to be processed.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool TrySend(U item)
        {
            return _mailbox.TryAdd(item);
        }

        /// <summary>
        /// Attempts to enqueue an item to be processed.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool TrySend(U item, TimeSpan timeout)
        {
            return _mailbox.TryAdd(item, timeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="millisecondsTimeout"></param>
        /// <returns></returns>
        public bool TrySend(U item, int millisecondsTimeout)
        {
            return _mailbox.TryAdd(item, millisecondsTimeout);
        }

        /// <summary>
        /// Attempts to enqueue an item to be processed.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="millisecondsTimeout"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public bool TrySend(U item, int millisecondsTimeout, CancellationToken cancellationToken)
        {
            return _mailbox.TryAdd(item, millisecondsTimeout, cancellationToken);
        }

        /// <summary>
        /// Gets the current state.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Disposes the agent.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            Stop();
            _mailbox.Dispose();
            _shutdownEvent.Dispose();
        }

        ~ThreadBackedAgent()
        {
            Dispose(false);
        }

    }

}
