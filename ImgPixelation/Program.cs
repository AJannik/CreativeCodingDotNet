using System;
using System.Numerics;
using NetProcessing;
using TestCC;

namespace ImgPixelation
{
    internal class Program : Sketch
    {
        public static void Main(string[] args)
        {
            new Program().Start();
        }
        
        private readonly int framerate = 30;
        private readonly int width = 1000;
        private readonly int height = 1000;
        private readonly int radius = 5;
        private readonly int padding = 50;
        private readonly string imgName = "doge.png";
        private readonly bool doWave = true;
        private readonly float amplitude = 10f;
        private readonly float waveSpeed = 20f;
        private readonly float frequency = 1f / 50f;
        
        private float time = 0f;
        private int Diameter => radius * 2;
        private float deltaTime;
        private PImage image;
        private Circle[,] circles;

        public override void Setup() {
            Size(width, height);
            FrameRate(framerate);
            NoStroke();
            deltaTime = 1f / framerate;
            image = LoadImage(Environment.CurrentDirectory + "/../../Res/" + imgName);
            image.Resize(width - 2 * padding, height - 2 * padding);
            circles = new Circle[image.Width / Diameter, image.Height / Diameter];
            
            GenerateCircles();
        }

        public override void Draw()
        {
            Background(0);
            EllipseMode(Parameter.Radius);

            foreach (Circle circle in circles)
            {
                Fill(circle.Color);
                double y = 0;
                if (doWave)
                {
                    y = Math.Sin(circle.Pos.X * frequency + time * waveSpeed) * amplitude;
                }
                
                Ellipse((int) circle.Pos.X, (int)(circle.Pos.Y + y), circle.Radius, circle.Radius);
            }

            time += deltaTime;
        }
        
        private void GenerateCircles()
        {
            for (int x = 0; x < circles.GetLength(0); x++)
            {
                for (int y = 0; y < circles.GetLength(1); y++)
                {
                    circles[x, y] =
                        new Circle(new Vector2(
                                x * Diameter + radius + padding,
                                y * Diameter + radius + padding),
                            radius,
                            image.Get(x * Diameter + radius, y * Diameter + radius));
                }
            }
        }
    }
}