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
    public static class DisciplineType_DictViewUtil
    {
        public static DisciplineType_DictView GetDisciplineType_DictView(this DisciplineType_Dict dict)
        {
            return new DisciplineType_DictView
            {
                DisciplineType_Dict = dict
            };
        }

        #region Property

        public static List<DisciplineTypeView> GetDisciplineTypeViews(this DisciplineType_DictView dict)
        {
            return dict.DisciplineType_Dict!.DisciplineTypes.Select(x => x.GetDisciplineTypeView()).ToList();
        }

        #endregion
    }
}
