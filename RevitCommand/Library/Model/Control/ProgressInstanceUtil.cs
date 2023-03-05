using Model.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class ProgressInstanceUtil
    {
        public static void Step(this ProgressInstance progressInstance)
        {
            var total = (double)progressInstance.Total!.Value;
            progressInstance.Value += 1;
            var value = progressInstance.Value!.Value;

            // Nếu value == total => cập nhập thanh trạng thái
            if (value != total)
            {
                var progressStepType = progressInstance.ProgressStepType;
                if (progressInstance.IsStepRatio && progressStepType != ProgressStepType.By1
                    && value % (int)progressStepType != 0) return;
            }

            var progressBar = progressInstance.ProgressUC.ProgressBar;
            progressBar.Dispatcher.Invoke(() =>
            {
                progressBar.Value = value / (total / 100);
                progressInstance.Ratio = $"{value}/{total}";
            }, System.Windows.Threading.DispatcherPriority.Background);
        }

        public static ProgressStepType GetProgressStepType(this ProgressInstance progressInstance)
        {
            var total = progressInstance.Total;
            if (total < 100)
            {
                return ProgressStepType.By1;
            }
            if (total < 250)
            {
                return ProgressStepType.By2;
            }
            if (total < 500)
            {
                return ProgressStepType.By5;
            }
            if (total < 1000)
            {
                return ProgressStepType.By1;
            }
            if (total < 10000)
            {
                return ProgressStepType.By10;
            }
            if (total < 100000)
            {
                return ProgressStepType.By100;
            }
            if (total < 1000000)
            {
                return ProgressStepType.By1000;
            }
            if (total < 10000000)
            {
                return ProgressStepType.By10000;
            }
            return ProgressStepType.By100000;
        }
    }
}
