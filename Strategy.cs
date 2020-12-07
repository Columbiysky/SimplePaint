using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPaint
{
    class FigureSelect : IStrategy
    {
        public void select(Graphics gr, List<Figure> figures, Group group, Manipulator manipulator, float x, float y)
        {
            foreach (var fig in figures)
            {
                if (fig.Touch(gr, x, y))
                {
                    group.Clear();
                    manipulator.Attach(fig);
                    break;
                }
                else
                    manipulator.Clear(gr);
            }
        }
    }

    class GroupSelect : IStrategy
    {
        public void select(Graphics gr, List<Figure> figures, Group group, Manipulator manipulator, float x, float y)
        {
            foreach (var fig in figures)
            {
                if (fig.Touch(gr, x, y))
                {
                    group.Add(fig);
                    manipulator.Attach(group);
                    break;
                    //manipulator.Update();
                }
                else
                    manipulator.Clear(gr);
            }
        }
    }
}
