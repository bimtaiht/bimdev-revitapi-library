using Autodesk.Revit.UI;
using SingleData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Utility;

namespace Model.Entity
{
    public class PushButtonCofig
    {
        public string? Name { get; set; }

        public Type? CommandType { get; set; }

        private string? commandName;
        public string CommandName
        {
            get => this.commandName ??= this.CommandType!.FullName;
            set => this.commandName = value;
        }

        public string AssemblyName => Assembly.GetAssembly(this.CommandType!).Location;

        public string? IconName { get; set; }

        public string? ToolTip { get; set; }
    }
}
