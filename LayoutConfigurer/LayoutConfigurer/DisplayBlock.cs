using System;
using System.Drawing;
using System.Windows.Forms;

namespace LayoutConfigurer
{
    public class DisplayBlock
    {
        private const int SizeNodeRect = 5;

        private PictureBox pictureBox;
        private Rectangle block;
        public bool AllowDeformingDuringMovement { get; set; }
        private bool isClick;
        private bool move;
        private int oldX;
        private int oldY;        
        private PosSizableRect nodeSelected = PosSizableRect.None;

        public event SizeChangedHandler SizeChanged;

        public void OnSizeChanged(DisplayBlockSizeChangedHandlerArgs args)
        {
            var handler = SizeChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        private enum PosSizableRect
        {
            UpMiddle,
            LeftMiddle,
            LeftBottom,
            LeftUp,
            RightUp,
            RightMiddle,
            RightBottom,
            BottomMiddle,
            None
        }

        public DisplayBlock(Rectangle r)
        {
            block = r;
            isClick = false;
        }

        public void Draw(Graphics g)
        {
            g.DrawRectangle(new Pen(Color.Red), block);

            foreach (PosSizableRect pos in Enum.GetValues(typeof(PosSizableRect)))
            {
                g.DrawRectangle(new Pen(Color.Red), GetSizingRect(pos));
            }
        }       

        public void SetPictureBox(PictureBox p)
        {
            pictureBox = p;
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseUp += PictureBox_MouseUp;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.Paint += PictureBox_Paint;
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (block != Rectangle.Empty)
                {
                    Draw(e.Graphics);
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            isClick = true;

            nodeSelected = PosSizableRect.None;
            nodeSelected = GetNodeSelectable(e.Location);

            if (block.Contains(new Point(e.X, e.Y)))
            {
                move = true;
            }

            oldX = e.X;
            oldY = e.Y;
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isClick = false;
            move = false;
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeCursor(e.Location);
            if (isClick == false)
            {
                return;
            }

            var backupRect = block;

            switch (nodeSelected)
            {
                case PosSizableRect.LeftUp:
                    block.X = block.X + e.X - oldX;
                    block.Width -= e.X - oldX;
                    block.Y += e.Y - oldY;
                    block.Height -= e.Y - oldY;
                    break;
                case PosSizableRect.LeftMiddle:
                    block.X += e.X - oldX;
                    block.Width -= e.X - oldX;
                    break;
                case PosSizableRect.LeftBottom:
                    block.Width -= e.X - oldX;
                    block.X += e.X - oldX;
                    block.Height += e.Y - oldY;
                    break;
                case PosSizableRect.BottomMiddle:
                    block.Height += e.Y - oldY;
                    break;
                case PosSizableRect.RightUp:
                    block.Width += e.X - oldX;
                    block.Y += e.Y - oldY;
                    block.Height -= e.Y - oldY;
                    break;
                case PosSizableRect.RightBottom:
                    block.Width += e.X - oldX;
                    block.Height += e.Y - oldY;
                    break;
                case PosSizableRect.RightMiddle:
                    block.Width += e.X - oldX;
                    break;

                case PosSizableRect.UpMiddle:
                    block.Y += e.Y - oldY;
                    block.Height -= e.Y - oldY;
                    break;

                default:
                    if (move)
                    {
                        block.X = block.X + e.X - oldX;
                        block.Y = block.Y + e.Y - oldY;
                    }

                    break;
            }

            oldX = e.X;
            oldY = e.Y;

            if (block.Width < 5 || block.Height < 5)
            {
                block = backupRect;
            }

            TestIfRectInsideArea();

            pictureBox.Invalidate();            

            OnSizeChanged(new DisplayBlockSizeChangedHandlerArgs());
        }

        private void TestIfRectInsideArea()
        {
            // Test if rectangle still inside the area.
            if (block.X < 0) block.X = 0;
            if (block.Y < 0) block.Y = 0;
            if (block.Width <= 0) block.Width = 1;
            if (block.Height <= 0) block.Height = 1;

            if (block.X + block.Width > pictureBox.Width)
            {
                block.Width = pictureBox.Width - block.X - 1; // -1 to be still show 
                if (AllowDeformingDuringMovement == false)
                {
                    isClick = false;
                }
            }

            if (block.Y + block.Height > pictureBox.Height)
            {
                block.Height = pictureBox.Height - block.Y - 1; // -1 to be still show 
                if (AllowDeformingDuringMovement == false)
                {
                    isClick = false;
                }
            }
        }

        private Rectangle CreateRectSizableNode(int x, int y)
        {
            return new Rectangle(x - (SizeNodeRect / 2), y - (SizeNodeRect / 2), SizeNodeRect, SizeNodeRect);
        }

        private Rectangle GetSizingRect(PosSizableRect p)
        {
            switch (p)
            {
                case PosSizableRect.LeftUp:
                    return CreateRectSizableNode(block.X, block.Y);

                case PosSizableRect.LeftMiddle:
                    return CreateRectSizableNode(block.X, block.Y + (block.Height / 2));

                case PosSizableRect.LeftBottom:
                    return CreateRectSizableNode(block.X, block.Y + block.Height);

                case PosSizableRect.BottomMiddle:
                    return CreateRectSizableNode(block.X + (block.Width / 2), block.Y + block.Height);

                case PosSizableRect.RightUp:
                    return CreateRectSizableNode(block.X + block.Width, block.Y);

                case PosSizableRect.RightBottom:
                    return CreateRectSizableNode(block.X + block.Width, block.Y + block.Height);

                case PosSizableRect.RightMiddle:
                    return CreateRectSizableNode(block.X + block.Width, block.Y + (block.Height / 2));

                case PosSizableRect.UpMiddle:
                    return CreateRectSizableNode(block.X + (block.Width / 2), block.Y);

                default:
                    return new Rectangle();
            }
        }

        private PosSizableRect GetNodeSelectable(Point p)
        {
            foreach (PosSizableRect r in Enum.GetValues(typeof(PosSizableRect)))
            {
                if (GetSizingRect(r).Contains(p))
                {
                    return r;
                }
            }

            return PosSizableRect.None;
        }

        private void ChangeCursor(Point p)
        {
            pictureBox.Cursor = GetCursor(GetNodeSelectable(p));
        }
      
        private Cursor GetCursor(PosSizableRect p)
        {
            switch (p)
            {
                case PosSizableRect.LeftUp:
                    return Cursors.SizeNWSE;

                case PosSizableRect.LeftMiddle:
                    return Cursors.SizeWE;

                case PosSizableRect.LeftBottom:
                    return Cursors.SizeNESW;

                case PosSizableRect.BottomMiddle:
                    return Cursors.SizeNS;

                case PosSizableRect.RightUp:
                    return Cursors.SizeNESW;

                case PosSizableRect.RightBottom:
                    return Cursors.SizeNWSE;

                case PosSizableRect.RightMiddle:
                    return Cursors.SizeWE;

                case PosSizableRect.UpMiddle:
                    return Cursors.SizeNS;
                default:
                    return Cursors.Default;
            }
        }

        public void SetRect(Rectangle rectangle)
        {
            block = rectangle;
            pictureBox.Refresh();
        }

        public Rectangle GetRect()
        {
            return block;
        }
    }

    public delegate void SizeChangedHandler(object sender, DisplayBlockSizeChangedHandlerArgs args);

    public class DisplayBlockSizeChangedHandlerArgs
    {

    }
}