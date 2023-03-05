using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.ViewModel
{
    public class DisciplineType_DictView : NotifyClass
    {
        public DisciplineType_Dict? DisciplineType_Dict { get; set; }

        private List<DisciplineTypeView>? disciplineTypeViews;
        public List<DisciplineTypeView> DisciplineTypeViews => disciplineTypeViews ??= this.GetDisciplineTypeViews();

        public bool IsGetSelectedDisciplineTypeView { get; set; } = false;

        private DisciplineTypeView? selectedDisciplineTypeView;
        public DisciplineTypeView? SelectedDisciplineTypeView
        {
            get
            {
                if (!IsGetSelectedDisciplineTypeView && selectedDisciplineTypeView == null)
                {
                    IsGetSelectedDisciplineTypeView = true;
                    selectedDisciplineTypeView = DisciplineTypeViews.FirstOrDefault(x => x.DisciplineType == DisciplineType_Dict?.SelectedDisciplineType);
                }
                return selectedDisciplineTypeView;
            }
            set
            {
                selectedDisciplineTypeView = value; OnPropertyChanged();
                if (value != null)
                {
                    DisciplineType_Dict!.SelectedDisciplineType = value.DisciplineType;
                }
            }
        }
    }
}
