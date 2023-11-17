using System.ComponentModel.DataAnnotations;

namespace PhysicsEngine;

public struct AABB
{
    public Vec2 Min;
    public Vec2 Max;
    public float Area => (Max.X - Min.Y) * (Max.Y - Min.Y);

    public static bool operator ==(AABB left, AABB right)
    {
        return left.Min == right.Min && left.Max == right.Max;
    }

    public static bool operator !=(AABB left, AABB right)
    {
        return left.Min != right.Min || left.Max != right.Max;
    }
}
