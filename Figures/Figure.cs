using System.Drawing;


namespace MyPaint.Figures
{
    abstract class Figure
    {
        protected int X { get; set; }
        protected int Y { get; set; }
        protected int W { get; set; }
        protected int H { get; set; }

        public Figure()
        {

        }

        public Figure(int x, int y, int w, int h)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
        }

        public abstract void Draw(Graphics gr);
        public abstract bool Touch(Graphics gr, int x, int y);
        public abstract void Resize(Graphics gr, int w, int h);
        public abstract void Move(int dx, int dy);   
    }
}
