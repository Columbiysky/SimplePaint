
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint
{
    interface IFigureCreator
    {
        Figure Create(float x, float y, float w, float h);
        Figure Create(float x, float y);
    }
}
