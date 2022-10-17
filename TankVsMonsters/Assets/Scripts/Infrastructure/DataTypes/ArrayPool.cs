using System.Collections.Generic;

namespace Infrastructure.DataTypes
{
    public class ArrayPool<T>
    {
        private readonly Queue<T[]> _arrays;
        private readonly int _length;

        public ArrayPool(int length)
        {
            _arrays = new Queue<T[]>();
            _length = length;
        }

        public T[] GetArray()
        {
            if (_arrays.Count > 0)
            {
                return _arrays.Dequeue();
            }

            return new T[_length];
        }

        public void Return(T[] array)
        {
            if (array.Length != _length)
            {
                UnityEngine.Debug.LogError($"{nameof(ArrayPool<T>)}.{nameof(Return)}> "
                                           + "Returning array with different length: "
                                           + $"array_length={array.Length}, "
                                           + $"pool_length={_length}");
            }
            
            _arrays.Enqueue(array);
        }
    }
}