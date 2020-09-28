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
        public override void Draw(Graphics gr) //нарисовать рамку
        {
            if (fig == null) return;

            gr.DrawRectangle(new Pen(Color.Black), new Rectangle(X - 1, Y - 1, W + 2, H + 2)); //Main

            gr.DrawRectangle(new Pen(Color.Black), new Rectangle(X - 5, Y - 5, 7, 7)); //left-Top - 1
            gr.DrawRectangle(new Pen(Color.Black), new Rectangle(X + W - 2, Y - 5, 7, 7)); //right-Top - 2 
            gr.DrawRectangle(new Pen(Color.Black), new Rectangle(X - 5, Y + H - 4, 7, 7)); //left-bot - 3
            gr.DrawRectangle(new Pen(Color.Black), new Rectangle(X + W - 4, Y + H - 4, 7, 7)); //right-bot - 4
        }

        public override void Move(int dx, int dy)
        {
            throw new NotImplementedException();
        }

        public override void Resize(int c, int dw, int dh)
        {
            throw new NotImplementedException();
        }

        public override bool Touch(Graphics gr, int x, int y)
        {
            if (x >= X - 5 && x <= X + 2 &&
                y >= Y - 5 && y <= Y + 2)
            {
                corner = 1;
                return true;
            }

            else if (x >= X + W - 2 && x <= X + W + 5 &&
                y >= Y - 5 && y <= Y + 2)
                 {
                    corner = 2;
                    return true;
                 }

            else if (x >= X - 5 && x <= X + 4 &&
                 y >= Y + H - 4 && y <= Y + H + 5)
                 {
                    corner = 3;
                    return true;
                 }

            else if (x >= X + W - 4 && x <= X + W + 5 &&
                 y >= Y + H - 4 && y <= Y + H + 5)
                 {
                    corner = 4;
                    return true;
                 }

            else if (x >= X && x <= X + W &&
                 y >= Y && y <= Y + H)
                 {
                    corner = -1;
                    return true;
                 }
            else
                return false;
        }
        public virtual void Attach( Figure G ) //присоеденить манипулятор к одной фигуре, хз как по-русски это сказать
        {
            fig = G;
            Update();
        }

        public virtual void Drag(int dx, int dy) //move-resize one method
        {
            if (corner == -1)
            {
                X += dx;
                Y += dy;
                fig.Move(dx, dy);
                Update();
            }
            else if (corner == 1)
            {
                X += dx;
                Y += dy;
                W -= dx;
                H -= dy;
                fig.Resize(corner, dx, dy);
                Update();
            }
            else if(corner == 2)
            {
                Y += dy;
                W += dx;
                H -= dy;
                fig.Resize(corner, dx, dy);
                Update();
            }

            else if (corner == 3)
            {
                X += dx;
                W -= dx;
                H -= dy;
                fig.Resize(corner, dx, dy);
                Update();
            }

            else if (corner == 4)
            {
                W += dx;
                H += dy;
                fig.Resize(corner, dx, dy);
                Update();
            }
        }

        public virtual void Clear(Graphics gr) //убрать ссылку на фигуру
        {
            fig = null;
            gr.DrawRectangle(new Pen(Color.White), new Rectangle(X - 1, Y - 1, W + 2, H + 2));
        }

        public virtual void Update() //обновить параметры манипулятора под параметры фигуры
        {
            X = fig.X;
            Y = fig.Y;
            W = fig.W;
            H = fig.H;
        }
    }
}