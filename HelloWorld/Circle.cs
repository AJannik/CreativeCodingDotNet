using System.Numerics;
using NetProcessing;

namespace TestCC
{
    public class Circle
    {
        public int Radius { get; }
        public Vector2 Pos { get; set; }
        public Sketch.Color Color { get; }

        public Circle(Vector2 pos, int radius, Sketch.Color color)
        {
            Radius = radius;
            Pos = pos;
            Color = color;
        }

        public void SetX(int x)
        {
            Pos = new Vector2(x, Pos.Y); 
        }

        public void SetY(int y)
        {
            Pos = new Vector2(Pos.X, y);
        }
    }
}