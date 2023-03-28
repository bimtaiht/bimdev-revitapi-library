using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Control
{
    public class ProgressInstance : NotifyClass
    {
        public ProgressInstance(Model.UserControl.ProgressUC progressUC)
        {
            this.ProgressUC = progressUC;
        }

        public Model.UserControl.ProgressUC ProgressUC { get; set; }

        private string? log;
        public string? Log
        {
            get=> log;
            set
            {
                log = value; OnPropertyChanged();
            }
        }

        private int? total;
        public int? Total
        {
            get=>total;
            set
            {
                total = value;
                Value = 0;
                Ratio = $"{Value}/{total}";
            }
        }

        private ProgressStepType? progressStepType;
        public ProgressStepType ProgressStepType => progressStepType ??= this.GetProgressStepType();

        private int? value;
        public int? Value
        {
            get=> value;
            set
            {
                if (this.value == value) return;

                this.value = value;
            }
        }

        private string? ratio;
        public string? Ratio
        {
            get => ratio;
            set
            {
                ratio = value; OnPropertyChanged();
            }
        }

        public bool IsStepRatio { get; set; } = true;
    }
}
