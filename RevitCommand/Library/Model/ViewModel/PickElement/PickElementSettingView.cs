using Model.Command;
using Model.Entity;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.ViewModel
{
    public class PickElementSettingView : NotifyClass
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public PickElementSetting PickElementSetting { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private DisciplineType? disciplineType;
        public DisciplineType DisciplineType
        {
            get
            {
                if (disciplineType == null)
                {
                    disciplineType = PickElementSetting.DisciplineType;
                }
                return disciplineType.Value;
            }
            set
            {
                disciplineType = value;
                if (!M2VM)
                {
                    PickElementSetting.DisciplineType = value;
                }
            }
        }

        private PickElementType? pickElementType;
        public PickElementType PickElementType
        {
            get
            {
                if (pickElementType == null)
                {
                    pickElementType = PickElementSetting.PickElementType;
                }
                return pickElementType.Value;
            }
            set
            {
                pickElementType = value;
                this.PickElementsVisibility = this.GetPickElementsVisibility();

                PickElementSetting.PickElementType = value;
            }
        }

        private bool? pickByEntireProject;
        public bool PickByEntireProject
        {
            get
            {
                if (pickByEntireProject == null)
                {
                    pickByEntireProject = this.GetPickByEntireProject();
                }
                return pickByEntireProject.Value;
            }
            set
            {
                pickByEntireProject = value; OnPropertyChanged();
                this.SetPickByEntireProject(value);
            }
        }

        private bool? pickByView;
        public bool PickByView
        {
            get
            {
                if (pickByView == null)
                {
                    pickByView = this.GetPickByView();
                }
                return pickByView.Value;
            }
            set
            {
                pickByView = value; OnPropertyChanged();
                this.SetPickByView(value);
            }
        }

        private bool? pickElementsInProject;
        public bool PickElementsInProject
        {
            get
            {
                if (pickElementsInProject == null)
                {
                    pickElementsInProject = this.GetPickElementsInProject();
                }
                return pickElementsInProject.Value;
            }
            set
            {
                pickElementsInProject = value; OnPropertyChanged();
                this.SetPickElementsInProject(value);
            }
        }

        private Func<System.Windows.Visibility>? visibility_Render;
        public Func<System.Windows.Visibility> Visibility_Render
        {
            get => visibility_Render ?? (visibility_Render = this.GetVisibility_Render());
            set => visibility_Render = value;
        }

        private System.Windows.Visibility? visibility;
        public System.Windows.Visibility Visibility
        {
            get
            {
                if (visibility == null)
                {
                    visibility = this.Visibility_Render();
                }
                return visibility.Value;
            }
            set
            {
                visibility = value; OnPropertyChanged();
            }
        }

        private Func<System.Windows.Visibility>? pickElementsInProject_Visibility_Render;
        public Func<System.Windows.Visibility> PickElementsInProject_Visibility_Render
        {
            get => pickElementsInProject_Visibility_Render ??= this.GetPickElementsInProject_Visibility_Render();
            set => pickElementsInProject_Visibility_Render = value;
        }

        private System.Windows.Visibility? pickElementsInProject_Visibility;
        public System.Windows.Visibility PickElementsInProject_Visibility
        {
            get
            {
                if (pickElementsInProject_Visibility == null)
                {
                    pickElementsInProject_Visibility = this.PickElementsInProject_Visibility_Render();
                }
                return pickElementsInProject_Visibility.Value;
            }
            set
            {
                pickElementsInProject_Visibility = value; OnPropertyChanged();
            }
        }

        private System.Windows.Visibility? pickElementsVisibility;
        public System.Windows.Visibility PickElementsVisibility
        {
            get
            {
                if (pickElementsVisibility == null)
                {
                    pickElementsVisibility = this.GetPickElementsVisibility();
                }
                return pickElementsVisibility.Value;
            }
            set
            {
                pickElementsVisibility = value; OnPropertyChanged();
            }
        }

        private int count = -1;
        public int Count
        {
            get
            {
                if (count == -1)
                {
                    count = PickElementSetting.Count;
                }
                return count;
            }
            set
            {
                count = value; OnPropertyChanged();
            }
        }

        private bool pickElementEnable = true;
        public bool PickElementEnable
        {
            get
            {
                return pickElementEnable;
            }
            set
            {
                pickElementEnable = value; OnPropertyChanged();
            }
        }

        public Action? Action { get; set; }

        #region Method

        private System.Windows.Input.ICommand? pickElements;
        public System.Windows.Input.ICommand PickElements
        {
            get
            {
                return pickElements ??= new RelayCommand(x => this.PickElements());
            }
        }

        #endregion
    }
}
