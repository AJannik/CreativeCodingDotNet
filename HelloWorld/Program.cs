using System;
using System.Numerics;
using NetProcessing;

namespace TestCC
{
    internal class Program : Sketch
    {
        public static void Main(string[] args)
        {
            new Program().Start();
        }

        private readonly int framerate = 60;
        private readonly int width = 600;
        private readonly int height = 600;
        private readonly Circle[] circles = new Circle[80];
        private float deltaTime;
        private readonly float vel = 60f;
        private readonly float radiusFactor = 1.3f;
        private readonly int radiusMin = 10;
        private readonly int radiusMax = 40;

        public override void Setup() {
            Size(width, height);
            FrameRate(framerate);
            deltaTime = 1f / framerate;

            for (int i = 0; i < circles.Length; i++)
            {
                SpawnCircle(i);
            }
        }

        private void SpawnCircle(int i)
        {
            Color color = new((int) Random(0, 255), (int) Random(0, 255), (int) Random(0, 255));
            int radius = (int) Random(radiusMin, radiusMax);
            Vector2 pos = new((float) Random(width / 5 * 2, width / 5 * 3), (float) Random(height / 5 * 2, height / 5 * 3));
            circles[i] = new Circle(pos, radius, color);
        }

        public override void Draw()
        {
            Background(255);
            foreach (Circle c in circles)
            {
                DrawCircle(c);
            }
        }

        private void DrawCircle(Circle circle)
        {
            EllipseMode(Parameter.Radius);
            Fill(circle.Color, 255);

            Vector2 dir = GetDirectionVector(circle);

            if (dir != Vector2.Zero)
            {
                Vector2 v = Vector2.Normalize(dir) * deltaTime * vel;
                circle.Pos -= v;
            }

            ClampPositionToScreen(circle);
            Ellipse((int) circle.Pos.X, (int) circle.Pos.Y, circle.Radius, circle.Radius);
        }

        private void ClampPositionToScreen(Circle circle)
        {
            if (circle.Pos.X < 0)
            {
                circle.SetX(0);
            }

            if (circle.Pos.X >= width)
            {
                circle.SetX(width - 1);
            }

            if (circle.Pos.Y < 0)
            {
                circle.SetY(0);
            }

            if (circle.Pos.Y >= height)
            {
                circle.SetY(height - 1);
            }
        }

        private Vector2 GetDirectionVector(Circle current)
        {
            Vector2 dir = Vector2.Zero;
            foreach (Circle circle in circles)
            {
                if (current == circle)
                {
                    continue;
                }

                if (CheckCollision(current, circle))
                {
                    dir += circle.Pos - current.Pos;
                }
            }

            return dir;
        }

        private bool CheckCollision(Circle circle1, Circle circle2)
        {
            float squaredDistanceCircles = Vector2.DistanceSquared(circle1.Pos, circle2.Pos);
            float squaredRadii = (circle1.Radius + circle2.Radius) * (circle1.Radius + circle2.Radius);

            return squaredDistanceCircles < squaredRadii * radiusFactor;
        }
    }
}