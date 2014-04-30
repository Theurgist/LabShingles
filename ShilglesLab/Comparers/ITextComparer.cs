using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShilglesLab
{
    interface ITextComparer
    {
        double Result { get; }
        double ProcessTexts(string textA, string textB);
    }
}
