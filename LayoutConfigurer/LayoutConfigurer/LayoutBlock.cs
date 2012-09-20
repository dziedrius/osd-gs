using System.Drawing;

namespace LayoutConfigurer.Model
{
    public class LayoutBlock
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }

        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle GetRect()
        {
            return new Rectangle(Left, Top, Width, Height);
        }

        public void SetRect(Rectangle rectangle)
        {
            Left = rectangle.X;
            Top = rectangle.Y;
            Width = rectangle.Width;
            Height = rectangle.Height;
        }
    }
}