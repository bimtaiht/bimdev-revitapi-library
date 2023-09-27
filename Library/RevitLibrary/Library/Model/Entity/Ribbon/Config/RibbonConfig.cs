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
    public class RibbonConfig
    {
        private string? _id;
        public string id
        {
            get => this._id ??= this.Name!;
            set => this._id = value;
        }

        public string? Tab { get; set; }

        public string? Panel { get; set; }

        public string? Name { get; set; }

        public string? ToolTip { get; set; }

        public string? IconPath { get; set; }

        public bool Enabled { get; set; } = true;

        public PushButton_UseType? UseType { get; set; }
    }
}
