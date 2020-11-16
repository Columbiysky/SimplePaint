using System.Collections.Generic;
using System.Drawing;

namespace MyPaint
{
    class MyEllipse : Figure
    {
        public class EllipseCreator : IFigureCreator
        {
            public Figure Create(float x, float y, float w, float h)
            {
                return new MyEllipse(x, y, w, h);
            }

            public Figure Create(float x, float y)
            {
                return new MyEllipse(x, y, 50, 50);
            }
        }
        public MyEllipse()
        {
        }

        public MyEllipse(float x, float y, float w, float h) : base(x, y, w, h)
        {
        }

        public override void Draw(Graphics gr)
        {
            Rectangle rect = new Rectangle((int)X, (int)Y, (int)W, (int)H);
            gr.DrawEllipse(new Pen(Color.Green), rect);
        }

        private void Select(Graphics gr)
        {
            Rectangle rect = new Rectangle((int)X - 1, (int)Y - 1, (int)W + 2, (int)H + 2);
            gr.DrawRectangle(new Pen(Color.Black), rect);
        }

        private void Unselect(Graphics gr)
        {
            Rectangle rect = new Rectangle((int)X - 1, (int)Y - 1, (int)W + 2, (int)H + 2);
            gr.DrawRectangle(new Pen(Color.White), rect);
        }

        public override bool Touch(Graphics gr, float x, float y)
        {
            if (x >= X && x <= X + W &&
                y >= Y && y <= Y + H)
            {
                List<Rectangle> l = new List<Rectangle>();
                l.Add(new Rectangle((int)X - 1, (int)Y - 1, (int)W + 2, (int)H + 2));
                l.Add(new Rectangle((int)X - 3, (int)Y - 3, 5, 5)); //left-Top
                l.Add(new Rectangle((int)(X + W - 2), (int)Y - 3, 5, 5)); //right-Top
                l.Add(new Rectangle((int)X - 3, (int)(Y + H - 2), 5, 5)); //left-bot
                l.Add(new Rectangle((int)(X + W - 2), (int)(Y + H - 2), 5, 5)); //right-bot

                Select(gr);
                return true;
            }
            else
            {
                Unselect(gr);
                return false;
            }
        }
        public override void Move(float x, float y)
        {
            X += x;
            Y += y;
        }

        public override void Resize(int c, float dx, float dy)
        {
            W += dx;
            H += dy;
        }

        public override Figure Clone()
        {
            return new MyEllipse(X, Y, W, H);
        }
    }
}
