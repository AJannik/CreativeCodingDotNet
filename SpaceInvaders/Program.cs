using System;
using NetProcessing;

namespace SpaceInvaders
{
    internal class Program : Sketch
    {
        public static void Main(string[] args)
        {
            new Program().Start();
        }

        private const int PixelPerInvader = 8;

        private new const int Width = 1000;
        private new const int Height = 1000;

        private readonly Color[] colors = new[] {new Color(120, 30, 80), new Color(0, 200, 50), 
            new Color(240, 240, 30), new Color(0, 0, 0), new Color(0, 0, 0), 
            new Color(0, 0, 0)};
        
        private readonly int[,] invader = new int[PixelPerInvader, PixelPerInvader];

        public override void Setup()
        {
            Size(Width, Height);
            NoStroke();

            GenerateInvader();
        }

        public override void Draw()
        {
            for (int y = 0; y < PixelPerInvader; y++)
            {
                for (int x = 0; x < PixelPerInvader; x++)
                {
                    Fill(colors[invader[x, y]]);
                    Rect(x * (Width / PixelPerInvader), y * (Height / PixelPerInvader), (x + 1) * (Width / PixelPerInvader), (y + 1) * (Height / PixelPerInvader));
                }
            }

            NoLoop();
        }
        
        private void GenerateInvader()
        {
            for (int y = 0; y < PixelPerInvader; y++)
            {
                for (int x = 0; x < PixelPerInvader / 2; x++)
                {
                    invader[x, y] = (int) Random(0.0, colors.Length - 1.0);
                    invader[PixelPerInvader - 1 - x, y] = invader[x, y];
                }

                if (PixelPerInvader % 2 != 0)
                {
                    invader[PixelPerInvader / 2, y] = (int) Random(0.0, colors.Length - 1.0);
                }
            }
        }
    }
}