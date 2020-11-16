using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint
{
    class GroupCreator : IFigureCreator
    {
        Figure created;
        public Figure Create(float x, float y, float w, float h)
        {
            Figure fig;
            fig = created.Clone();
            fig.Resize(4,fig.W-w, fig.H-h);
            fig.Move(x - fig.X - (fig.W / 2), y - fig.Y - (fig.H / 2));
            return fig;
        }
        public Figure Create(float x, float y)
        {
            Figure fig;
            fig = created.Clone();
            fig.Resize(4, 0, 0);
            fig.Move(x - fig.X - (fig.W / 2), y - fig.Y - (fig.H / 2));
            return fig;
        }

        public void CopyCreated(Group g)
        {
            created = g.Clone();
        }
    }
}
