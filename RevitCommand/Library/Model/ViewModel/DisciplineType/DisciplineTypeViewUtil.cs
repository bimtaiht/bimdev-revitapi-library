using Model.Entity;
using Model.ViewModel;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class DisciplineTypeViewUtil
    {
        public static DisciplineTypeView GetDisciplineTypeView(this DisciplineType disciplineType)
        {
            return new DisciplineTypeView
            {
                DisciplineType = disciplineType
            };
        }

        #region Property

        public static string GetName(this DisciplineTypeView disciplineTypeView)
        {
            return disciplineTypeView.DisciplineType.GetName();
        }

        #endregion
    }
}
