﻿namespace Audio
{
    public readonly struct Position : IPosition
    {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public Position(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}