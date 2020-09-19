using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace MyPaint.Figures
{
    class MyRectangle : Figure
    {
        public class RectangleCreator : IFigureCreator
        {
            public Figure Create(int x, int y, int w, int h)
            {
                return new MyRectangle(x, y, w, h);
            }
        }



        private  MyRectangle(int x, int y, int w, int h) : base(x, y, w, h)
        {

        }

        [DebuggerStepThrough]
        public override void Draw(Graphics gr)
        {
            Rectangle rect = new Rectangle(X, Y, W, H);
            gr.DrawRectangle(new Pen(Color.Red), rect);
        }

        private void Select(Graphics gr)
        {
            Rectangle rect = new Rectangle(X - 1, Y - 1, W + 2, H + 2);
            gr.DrawRectangle(new Pen(Color.Black), rect);
        }

        private void Unselect(Graphics gr)
        {
            Rectangle rect = new Rectangle(X - 1, Y - 1, W + 2, H + 2);
            gr.DrawRectangle(new Pen(Color.White), rect);
        }

        public override bool Touch(Graphics gr, int x, int y, out List<Rectangle> Resizers)
        {
            
            if (x >= X && x <= X + W &&
                y >= Y && y <= Y + H)
            {
                List<Rectangle> l = new List<Rectangle>();
                l.Add(new Rectangle(X - 1, Y - 1, W + 2, H + 2));
                l.Add(new Rectangle(X - 3, Y - 3, 5, 5)); //left-Top
                l.Add(new Rectangle(X + W - 2, Y - 3, 5, 5)); //right-Top
                l.Add(new Rectangle(X - 3, Y + H - 2, 5, 5)); //left-bot
                l.Add(new Rectangle(X + W - 2, Y + H - 2, 5, 5)); //right-bot

                Resizers = l;
                Select(gr);
                return true;
            }
            else
            {
                Resizers = null;
                Unselect(gr);
                return false;
            }
        }

        public override void Resize(Graphics gr, int w, int h)
        {
            
        }

        public override void Move(int x, int y)
        {
            
            X += x;
            Y += y;
        }
    }
}
