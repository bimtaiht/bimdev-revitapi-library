using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ButtonGroup
    {
        public Action<bool>? HandleButtons { get; set; }
    }
}
