using UnityEngine;

namespace Extensions
{
    public static class TransformExtensions
    {
        public static TransformData GetTransformData(this Transform transform) => new(transform);

        public static void CopyFrom(this Transform transform, TransformData transformData)
        {
            transform.position = transformData.position;
            transform.rotation = transformData.rotation;
        }

        public static void CopyFrom(this Transform target, Transform source)
        {
            var data = source.GetTransformData();
            target.CopyFrom(data);
        }

        public static void CopyTo(this Transform source, Transform target) => target.CopyFrom(source);
    }
}