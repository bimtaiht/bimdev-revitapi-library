using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleData
{
    public class FormData : NotifyClass
    {
        private static FormData? instance;
        public static FormData Instance
        {
            get => instance ??= new FormData();
            set => instance = value;
        }

        public System.Windows.Window? Form { get; set; }

        public bool IsDisposeOnClosed { get; set; }

        public bool IsFormVisible { get; set; }

        public bool IsDialog { get; set; }

        public bool IsPublish { get; set; }

        public static void Dispose()
        {
            Instance = null;
        }
    }
}
