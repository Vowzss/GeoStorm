using System;
using System.Numerics;
using Raylib_cs;

namespace GeoStorm.MathExtend
{
    class Maths
    {
        static public Random rand = new Random();

        public static Vector2 Rotate(Vector2 point, Vector2 origin, float radAngle)
        {
            Vector2 op = point - origin;
            Vector2 opP;
            opP.X = MathF.Cos(radAngle) * op.X - MathF.Sin(radAngle) * op.Y;
            opP.Y = MathF.Sin(radAngle) * op.X + MathF.Cos(radAngle) * op.Y;
            return opP + origin;
            
        }

        public static float Lerp(float t, float a, float b)
        {
            return a * (1 - t) + b * t;
        }

        public static Color ColorLerp(float t, Color a, Color b)
        {
            return new Color((int)(a.r * (1 - t) + b.r * t), (int)(a.g * (1 - t) + b.g * t), (int)(a.b * (1 - t) + b.b * t), (int)(a.a * (1 - t) + b.a * t));
        }

        public static Vector2 RandomVector(int widht, int height)
        {
            return new Vector2(rand.Next(0, widht), rand.Next(0, height));
        }

        public static Vector2 Projection(Vector2 p, Vector2 a, Vector2 b)
        {
            Vector2 axis = b - a;
            Vector2 point = p - a;
            float dp = Vector2.Dot(point, axis);
            return new Vector2((dp / (axis.X * axis.X + axis.Y * axis.Y)) * axis.X,
                (dp / (axis.X * axis.X + axis.Y * axis.Y)) * axis.Y) + a;
        }
    }
}
