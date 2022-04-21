using System;
using UnityEngine;

namespace Extensions
{
    [Serializable]
    public struct TransformData
    {
        public Vector3 position;
        public Quaternion rotation;

        public TransformData(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }

        public TransformData(Transform transform)
        {
            position = transform.position;
            rotation = transform.rotation;
        }

        public TransformData LerpTo(TransformData other, float percentage)
        {
            var betweenPosition = Vector3.Lerp(position, other.position, percentage);
            var betweenRotation = Quaternion.Lerp(rotation, other.rotation, percentage);

            return new TransformData(betweenPosition, betweenRotation);
        }

        public override string ToString() => $"P: {position}; Q: {rotation.eulerAngles};";
    }
}