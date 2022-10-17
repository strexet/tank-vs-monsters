using System;

namespace Infrastructure.DataTypes
{
    public class PooledArray<T> : IDisposable
    {
        private readonly ArrayPool<T> _pool;

        public int Length => RawData.Length;
        public T[] RawData { get; }

        public PooledArray(int length, ArrayPool<T> pool)
        {
            RawData = new T[length];
            _pool = pool;
        }

        public T this[int i]
        {
            get => RawData[i];
            set => RawData[i] = value;
        }

        public void Dispose() => _pool.Return(this);
    }
}