using UnityEngine;

namespace Audio
{
    public class UnityTransform : ITransform
    {
        private readonly Transform _transform;

        public UnityTransform(Transform transform)
        {
            _transform = transform;
        }

        public IPosition Position
        {
            get
            {
                var vector3 = _transform.position;
                return new Position(vector3.x, vector3.y, vector3.z);
            }
        }
    }
}