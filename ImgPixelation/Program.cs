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
        
        // Sets Framerate, best results with 30 or 60
        private readonly int framerate = 30;
        
        // Screen Size
        private readonly int width = 1000; 
        private readonly int height = 1000;
        
        // Radius per Circle
        private readonly int radius = 5;
        
        // Adds a empty Frame around your Image
        // Unit in Pixels
        private readonly int padding = 50;
        
        // Filename - file needs to be in "Res" folder
        private readonly string imgName = "doge.png";
        
        // Animation Controls
        // Activates/ Deactivates Animation
        private readonly bool doWave = true;
        // Wave height
        private readonly float amplitude = 10f;
        // Wave speed
        private readonly float waveSpeed = 20f;
        // Wave length
        private readonly float frequency = 1f / 50f;
        
        
        // internal Data, dont Touch!
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