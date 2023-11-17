﻿namespace PhysicsEngine
{
    public class CollisionPair
    {
        public readonly PhysicsObject A;
        public readonly PhysicsObject B;

        public CollisionPair(PhysicsObject A, PhysicsObject B)
        {
            this.A = A;
            this.B = B;
        }

        public static bool operator ==(CollisionPair left, CollisionPair right)
        {
            if (left.A.Aabb.Min == right.A.Aabb.Min && left.A.Aabb.Max == right.A.Aabb.Max &&
                left.B.Aabb.Min == right.B.Aabb.Min && left.B.Aabb.Max == right.B.Aabb.Max ||

                left.A.Aabb.Min == right.B.Aabb.Min && left.A.Aabb.Max == right.B.Aabb.Max &&
                left.B.Aabb.Min == right.A.Aabb.Min && left.B.Aabb.Max == right.A.Aabb.Max)
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(CollisionPair left, CollisionPair right)
        {
            if (left.A.Aabb.Min == right.A.Aabb.Min && left.A.Aabb.Max == right.A.Aabb.Max &&
                left.B.Aabb.Min == right.B.Aabb.Min && left.B.Aabb.Max == right.B.Aabb.Max ||

                left.A.Aabb.Min == right.B.Aabb.Min && left.A.Aabb.Max == right.B.Aabb.Max &&
                left.B.Aabb.Min == right.A.Aabb.Min && left.B.Aabb.Max == right.A.Aabb.Max)
            {
                return false;
            }

            return true;
        }

    }


}