using System;
using UnityEngine;

//public class Direction : MonoBehaviour
public readonly struct Direction
{
    private const int DirectionNumber = 6;
    
    public readonly int _number;
        
    public static readonly Direction North = new(0);
    public static readonly Direction Neast = new(1);
    public static readonly Direction Seast = new(2);
    public static readonly Direction South = new(3);
    public static readonly Direction Swest = new(4);
    public static readonly Direction Nwest = new(5);
    
    private Direction(int number)
    {
        _number = (number % DirectionNumber + DirectionNumber) % DirectionNumber;
    }
    
    public static implicit operator int(Direction it) => it._number;
    public static explicit operator Angle(Direction it) => it._number * Angle.MaxAngle / DirectionNumber;

    public static bool operator ==(Direction a, Direction b) => a._number == b._number;
    public static bool operator !=(Direction a, Direction b) => !(a == b);

    public Direction RelativeOf(Direction relativeNorth) => new Direction(_number + relativeNorth._number);

    public int Rotor(Direction other)
    {
        return (other._number - _number) switch
        {
            <= DirectionNumber / 2 and var it => it,
            var it => it - DirectionNumber
        };
    }
    
    public readonly struct RotationDirection
    {
        public static readonly RotationDirection Left = new(-1);
        public static readonly RotationDirection Right = new(1);

        public static implicit operator int(RotationDirection it) => it._direction;

        public static RotationDirection Shortest(Angle from, Angle to) => from.Rotor(to) < 0 ? Left : Right;
        public static RotationDirection Shortest(Direction from, Direction to) => from.Rotor(to) < 0 ? Left : Right;

        private readonly int _direction;

        private RotationDirection(int direction)
        {
            if (Mathf.Abs(direction) != 1) throw new ArgumentException($"RotationDirection({direction}");
            _direction = direction;
        }
    } 
    
    public readonly struct Angle
    {
        private readonly float _angle; //double
        public const float MaxAngle = 360f; //double

        public Angle(float angle = 0f) //double
        {
            _angle = (angle % MaxAngle + MaxAngle) % MaxAngle;
        }

        public static implicit operator Angle(float it) => new(it); //double
        public static implicit operator float(Angle it) => it._angle;//double

        public override string ToString() => $"{_angle}°";

        public static Angle operator +(Angle a, Angle b) => a._angle + b._angle;
        public static Angle operator -(Angle a, Angle b) => a._angle - b._angle;

        public bool Equals(Angle other, float error = 0.001f) //double
        {
            return Mathf.Abs(this - other) < error;
        }
        
        public float Rotor(Angle other)//double
        {
            return (other - this)._angle switch
            {
                <= MaxAngle / 2 and var it => it,
                var it => it - MaxAngle
            };
        }
        
        public Angle Between(Angle other)
        {
            return Mathf.Abs(Rotor(other));
        }
    }
}
