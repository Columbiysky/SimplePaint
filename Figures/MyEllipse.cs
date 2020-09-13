using System.Drawing;

namespace MyPaint.Figures
{
    class MyEllipse : Figure
    {
        public class EllipseCreator : IFigureCreator
        {
            public Figure Create(int x, int y, int w, int h)
            {
                return new MyEllipse(x, y, w, h);
            }
        }
        public MyEllipse()
        {
        }

        public MyEllipse(int x, int y, int w, int h) : base(x, y, w, h)
        {
        }

        public override void Draw(Graphics gr)
        {
            Rectangle rect = new Rectangle(X, Y, W, H);
            gr.DrawEllipse(new Pen(Color.Green), rect);
        }

        private void Select(Graphics gr)
        {
            Rectangle rect = new Rectangle(X - 1, Y - 1, W + 2, H + 2);
            gr.DrawEllipse(new Pen(Color.Black), rect);
        }

        private void Unselect(Graphics gr)
        {
            Rectangle rect = new Rectangle(X - 1, Y - 1, W + 2, H + 2);
            gr.DrawEllipse(new Pen(Color.White), rect);
        }

        public override bool Touch(Graphics gr, int x, int y)
        {
            if (x > X && x < X + W &&
                y > Y && y < Y + H)
            {
                Select(gr);
                return true;
            }
            else
            {
                Unselect(gr);
                return false;
            }
        }
        public override void Move(int x, int y)
        {

            X += x;
            Y += y;
        }

        public override void Resize(Graphics gr, int w, int h)
        {
            
        }
    }
}
