using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint
{
    class Manipulator : Figure
    {
        public Figure fig { get; private set; }
        int corner = -1;
        public override void Draw(Graphics gr) //draw frame
        {
            if (fig == null) return;

            gr.DrawRectangle(new Pen(Color.Black), new Rectangle((int)X - 2, (int)Y - 2, (int)W + 4, (int)H + 4)); //Main

            gr.DrawRectangle(new Pen(Color.Black), new Rectangle((int)X - 4, (int)Y - 4, 4, 4)); //left-Top - 1
            gr.DrawRectangle(new Pen(Color.Black), new Rectangle((int)(X + W), (int)Y - 4, 4, 4)); //right-Top - 2 
            gr.DrawRectangle(new Pen(Color.Black), new Rectangle((int)X - 4, (int)(Y + H), 4, 4)); //left-bot - 3
            gr.DrawRectangle(new Pen(Color.Black), new Rectangle((int)(X + W), (int)(Y + H), 4, 4)); //right-bot - 4
        }

        public override void Move(float dx, float dy)
        {
            throw new NotImplementedException();
        }

        public override void Resize(int c, float dw, float dh)
        {
            throw new NotImplementedException();
        }

        public override bool Touch(Graphics gr, float x, float y)
        {
            // Left top corner
            if (x >= X - 2 && x <= X + 2 &&
                y >= Y - 2 && y <= Y + 2)
            {
                corner = 1;
                return true;
            }
            //Right top corner
            else if (x >= X + W - 2 && x <= X + W + 2 &&
                 y >= Y - 2 && y <= Y + 2)
                 {
                     corner = 2;
                     return true;
                 }
            //Left bot corner
            else if (x >= X - 2 && x <= X + 2 &&
                 y >= Y + H - 2 && y <= Y + H + 2)
                 {
                     corner = 3;
                     return true;
                 }
            //Right bot corner
            else if (x >= X + W - 2 && x <= X + W + 2 &&
                 y >= Y + H - 2 && y <= Y + H + 2)
                 {
                     corner = 4;
                     return true;
                 }
            //No corner moving only
            else if (x >= X && x <= X + W &&
                 y >= Y && y <= Y + H)
                 {
                     corner = -1;
                     Draw(gr);
                     return true;
                 }
            //the figure hasn't been touched
            else
            {
                X = Y = W = H = 0;
                corner = -1;
                gr.DrawRectangle(new Pen(Color.White), new Rectangle((int)X - 1, (int)Y - 1, (int)W + 2, (int)H + 2));
                return false;
            }
        }
        public virtual void Attach( Figure f ) //attach the figure to manipulator
        {
            fig = f;
            Update();
        }

        public virtual void Drag(float dx, float dy) //move-resize one method
        {
            //No corner
            if (corner == -1)
            {
                fig.Move(dx, dy);
                Update();
            }
            //left top
            else if (corner == 1)
            {
                fig.Move(dx, dy);
                fig.Resize(corner, -dx, -dy);
                Update();
            }
            //right top
            else if(corner == 2)
            {
                fig.Move(0, dy);
                fig.Resize(corner, dx, -dy);
                Update();
            }
            //left bot
            else if (corner == 3)
            {
                fig.Move(dx, 0);
                fig.Resize(corner, -dx, dy);
                Update();
            }
            //right bot
            else if (corner == 4)
            {
                fig.Resize(corner, dx, dy);
                Update();
            }
        }

        public virtual void Clear(Graphics gr) //убрать ссылку на фигуру
        {
            fig = null;
            gr.DrawRectangle(new Pen(Color.White), new Rectangle((int)X - 1, (int)Y - 1, (int)W + 2, (int)H + 2));
        }

        public virtual void Update() //update manipulator variables to new figure variables
        {
            X = fig.X;
            Y = fig.Y;
            W = fig.W;
            H = fig.H;
        }

        public override Figure Clone() => fig;
    }
}