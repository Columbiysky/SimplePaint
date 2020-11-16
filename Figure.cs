using System.Collections.Generic;
using System.Drawing;


namespace MyPaint
{
    abstract class Figure
    {
        public float X { get; protected set; }
        public float Y { get; protected set; }
        public float W { get; protected set; }
        public float H { get; protected set; }

        public Figure()
        {

        }

        public Figure(float x, float y, float w, float h)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
        }

        public abstract void Draw(Graphics gr);
        public abstract bool Touch(Graphics gr, float x, float y);
        public abstract void Resize(int c, float dx, float dy);
        public abstract void Move(float dx, float dy);
        public abstract Figure Clone();
    }
}
