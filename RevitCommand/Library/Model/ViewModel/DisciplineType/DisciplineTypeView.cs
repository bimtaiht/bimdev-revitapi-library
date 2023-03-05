using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.ViewModel
{
    public class DisciplineTypeView
    {
        public DisciplineType DisciplineType { get; set; }

        private string? name;
        public string Name => name ??= this.GetName();

        #region Method

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
