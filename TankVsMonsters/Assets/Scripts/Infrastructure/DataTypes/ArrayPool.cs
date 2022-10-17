using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.DataTypes
{
    public class ArrayPool<T>
    {
        private readonly Queue<PooledArray<T>> _arrays;
        private readonly int _length;

        public ArrayPool(int length)
        {
            _arrays = new Queue<PooledArray<T>>();
            _length = length;
        }

        public PooledArray<T> GetArray()
        {
            if (_arrays.Count > 0)
            {
                return _arrays.Dequeue();
            }

            return new PooledArray<T>(_length, this);
        }

        public void Return(PooledArray<T> array)
        {
            if (array.Length != _length)
            {
                Debug.LogError($"[ArrayPool]<color=red>{nameof(ArrayPool<T>)}.{nameof(Return)}></color> "
                               + "Returning array with different length: "
                               + $"array_length={array.Length}, "
                               + $"pool_length={_length}");
            }

            _arrays.Enqueue(array);
        }
    }
}