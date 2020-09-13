
using MyPaint.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint
{
    interface IFigureCreator
    {
        Figure Create(int x, int y, int w, int h);
    }
}
