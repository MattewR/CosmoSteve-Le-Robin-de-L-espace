
using System;

public class Coordinate : IEquatable<Coordinate>
{
    private int m_x = 0;
    private int m_y = 0;

    public int X { get => m_x; set => m_x = value; }
    public int Y { get => m_y; set => m_y = value; }

    public Coordinate(int x, int y)
    {
        m_x = x;
        m_y = y;

    }

    public Coordinate()
    {

    }

    public Coordinate(Coordinate clone)
    {
        m_x = clone.X;
        m_y = clone.Y;
    }

    public void Clone(Coordinate toClone)
    {
        m_x = toClone.X;
        m_y = toClone.Y;
    }

    public bool Equals(Coordinate coordinate)
    {
        int x = coordinate.X;
        int y = coordinate.Y;

        return (m_x == x && m_y == y);
    }

    public override int GetHashCode()
    {
        return m_x * 31 + m_y;
    }

}
