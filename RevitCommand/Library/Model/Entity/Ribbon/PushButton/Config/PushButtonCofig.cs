using Autodesk.Revit.UI;
using SingleData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Utility;

namespace Model.Entity
{
    public class PushButtonCofig
    {
        public string? Name { get; set; }

        public string? CommandName { get; set; }

        public string? IconName { get; set; }

        public string? ToolTip { get; set; }
    }
}
