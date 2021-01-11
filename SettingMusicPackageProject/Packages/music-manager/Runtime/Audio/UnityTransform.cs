using UnityEngine;

namespace Audio
{
    public sealed class UnityTransform : ITransform
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

        public override bool Equals(object obj)
        {
            bool equals;
            
            if (obj == null)
            {
                equals = _transform.Equals(null);
            }
            else
            {
                equals = ReferenceEquals(this, obj);
            }

            return equals;
        }

        public override int GetHashCode()
        {
            return (_transform != null ? _transform.GetHashCode() : 0);
        }
    }
}