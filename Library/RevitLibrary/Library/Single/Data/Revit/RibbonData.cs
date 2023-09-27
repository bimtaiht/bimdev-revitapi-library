using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Model.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Utility;

namespace SingleData
{
    public class RibbonData
    {
        private static RibbonData? instance;
        public static RibbonData Instance
        {
            get => instance ??= new RibbonData();
            set => instance = value;
        }

        public UIControlledApplication? Application { get; set; }

        private List<EntTab>? tabs;
        public List<EntTab> Tabs => tabs ??= new List<EntTab>();

        public IEnumerable<EntPanel> Panels => this.Tabs.Select(x => x.EntPanels).Aggregate((a, b) =>
        {
            a.AddRange(b);
            return a;
        });

        public IEnumerable<EntPushButton> PushButtons => this.Panels.Select(x => x.EntPushButtons).Aggregate((a, b) =>
        {
            a.AddRange(b);
            return a;
        });

        private string? addinFilePath;
        public string AddinFilePath => addinFilePath ??= Assembly.GetExecutingAssembly().Location;
    }
}
