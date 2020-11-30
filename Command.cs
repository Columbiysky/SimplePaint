using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPaint
{
    class Command : ICommand
    {
        Stack<Figure> commands = new Stack<Figure>();
        List<Figure> MainFigures = new List<Figure>();
        Figure fig;
        public Figure Figure { get => fig; }

        public Command()
        {
        }

        public void Execute()
        {
            //fig.Draw(gr);
        }

        public void Undo()
        {
            if (commands.Count != 0)
            {
                fig = commands.Pop();
                //foreach (var f in MainFigures)
                //    if (f.X == fig.X &&
                //        f.Y == fig.Y &&
                //        f.H == fig.H &&
                //        f.W == fig.W)
                //    {
                //        fig = null;
                //        break;
                //    }
            }
            else
                fig = null;
        }

        public void UpdateList(Figure f)
        {
            MainFigures.Add(f.Clone());
            commands.Push(f.Clone());
        }

        public void UpdateCurrentFigure(Figure f)
        {
            fig = f.Clone();
            commands.Push(f.Clone());
        }

        public void Clear()
        {
            MainFigures.Clear();
            commands.Clear();
        }
    }
}
