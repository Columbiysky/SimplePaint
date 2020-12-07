
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint
{
    interface ITools
    {
        Figure Create(float x, float y, float w, float h);
        Figure Create(float x, float y);
    }
}
