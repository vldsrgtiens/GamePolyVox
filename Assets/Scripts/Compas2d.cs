using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compas2d : MonoBehaviour
{
    public enum RotationDirection { Left = -1, Right = 1 }
    public enum TypeDirection { North=0, NorthEast=1, SouthEast=2, South=3, SouthWest=4, NorthWest=5, NorthNorth=6}

    public readonly struct Angle
    {
        private const float MaxAngle = 360f;
        private readonly float _angle;

        public Angle(float angle = 0f)
        {
            _angle = (MaxAngle + angle % MaxAngle) % MaxAngle;
        }

        public static implicit operator Angle(float it) => new(it);
        public static implicit operator int(Angle it) => it._angle;

        public override string ToString() => $"{_angle}°";

        public static Angle operator +(Angle a, Angle b) => a._angle + b._angle;

        public static Angle operator -(Angle a, Angle b) => a._angle - b._angle;

        public int Difference(Angle other)
        {
            return (other - this)._angle switch
            {
                <= MaxAngle / 2 and var it => it,
                var it => it - MaxAngle
            };
        }

        public RotationDirection ShortestRotationDirection(Angle other) =>
            Difference(other) < 0 ? RotationDirection.Left : RotationDirection.Right;
    }
}
