using System.Collections.Generic;
using System.Drawing;


namespace MyPaint
{
    abstract class Figure
    {
        public  int X { get; protected set; }
        public int Y { get; protected set; }
        public int W { get; protected set; }
        public int H { get; protected set; }

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
        public abstract void Resize(int c, int dx, int dy);
        public abstract void Move(int dx, int dy);   
    }
}
