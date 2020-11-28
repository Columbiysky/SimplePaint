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
        List<Figure> figures = new List<Figure>();
        List<Figure> MainFigures = new List<Figure>();
        Figure fig;
        Graphics gr;
        public Figure Figure { get => fig; }

        public Command(Graphics gr_)
        {
            gr = gr_;
        }

        public void Execute()
        {
            //fig.Draw(gr);
        }

        public void Undo()
        {
            fig = commands.Pop();
        }

        public void UpdateList(Figure f,  List<Figure> mainFigures)
        {
            figures.Add(f.Clone());
            MainFigures = mainFigures;
            commands.Push(f.Clone());
        }

        public void UpdateCurrentFigure(Figure f)
        {
            fig = f.Clone();
            commands.Push(fig.Clone());
        }
    }


}
