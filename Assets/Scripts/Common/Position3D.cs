namespace WaveFunctionCollapse
{
    public struct Position3D
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Position3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Position3D operator + (Position3D p1, Position3D p2)
        {
            return new Position3D(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }

        public static Position3D operator - (Position3D p1, Position3D p2)
        {
            return new Position3D(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }

        public static Position3D operator - (Position3D p)
        {
            return new Position3D(-p.X, -p.Y, -p.Z);
        }

        public static bool operator == (Position3D p1, Position3D p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z;
        }

        public static bool operator != (Position3D p1, Position3D p2)
        {
            return p1.X != p2.X || p1.Y != p2.Y || p1.Z != p2.Z;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if ((obj is Position3D) == false) return false;

            Position3D pos = (Position3D)obj;

            return this == pos;
        }

        public override int GetHashCode()
        {
            return X + Y + Z * 121 - 5;
        }
    }
}
