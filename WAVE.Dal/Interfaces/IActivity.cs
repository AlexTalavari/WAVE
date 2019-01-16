using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WAVE.Dal.Interfaces
{
    interface IActivity
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        string Text { get; set; }
    }
}
