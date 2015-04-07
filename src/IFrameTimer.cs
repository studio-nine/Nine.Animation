namespace Nine.Animation
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public interface IFrameTimer
    {
        /// <summary>Adds a listener that listens to the tick event.</summary>
        /// <param name="listener">A function that an elapsed time in milliseconds and returns true to unsubscribe from more events</param>
        void OnTick(Func<double, bool> listener);
    }

    public abstract class FrameTimer : IFrameTimer
    {
        private bool updating = false;
        private bool needsClear = false;
        private readonly Stopwatch watch = new Stopwatch();
        private readonly List<Func<double, bool>> listeners = new List<Func<double, bool>>();

        public virtual void OnTick(Func<double, bool> listener)
        {
            if (listeners == null)
            {
                throw new ArgumentNullException(nameof(listener));
            }
            listeners.Add(listener);
        }

        public virtual void Clear()
        {
            if (updating)
            {
                needsClear = true;
            }
            else
            {
                listeners.Clear();
            }
        }

        public virtual void UpdateFrame(double? elapsedTime = null)
        {
            var dt = 0.0;

            if (watch.IsRunning)
            {
                dt = watch.Elapsed.TotalMilliseconds;
                watch.Restart();
            }
            else
            {
                watch.Start();
            }

            dt = elapsedTime ?? dt;

            try
            {
                updating = true;
                for (var i = 0; i < listeners.Count;)
                {
                    var listener = listeners[i];
                    if (listener(dt))
                    {
                        listeners.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            finally
            {
                updating = false;
                if (needsClear)
                {
                    listeners.Clear();
                }
            }
        }
    }
}