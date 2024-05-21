using Autodesk.Revit.DB;
using Model.Entity;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Utility
{
    public static class TransactionUtil
    {
        private static RevitData revitData => RevitData.Instance;
        private static TransactionData transactionData => TransactionData.Instance;
        private static FormData formData => FormData.Instance;

        public static void DoTransaction(this Document q, string transactionName, Action<Transaction> action, Action? onFinish = null)
        {
            q.DoTransaction(new TransactionConfig
            {
                Name = transactionName,
                Action = action,
                OnFinish = onFinish,
            });
        }

        public static void DoTransaction(this Document q, TransactionConfig config)
        {
            var action = config.Action;

            Action wrapperAction = () =>
            {
                var transactionName = config.Name;
                var onCreating = config.OnCreatingTransaction;

                using (var transactionGroup = new TransactionGroup(q))
                {
                    transactionGroup.Start();
                    using (var transaction = new Transaction(q, transactionName))
                    {
                        onCreating?.Invoke(transaction);
                        transaction.Start();

                        transactionData.CurrentTransaction = transaction;
                        action?.Invoke(transaction);

                        if (!transaction.HasEnded())
                        {
                            transaction.Commit();
                        }
                    }

                    transactionData.CurrentTransaction = null;
                    transactionGroup.Assimilate();

                    config.OnFinish?.Invoke();
                }
            };

            if (transactionData.State == TransactionState.Pending)
            {
                action?.Invoke(transactionData.CurrentTransaction!);
            }
            else
            {
                var isFormShow = formData.IsFormVisible && !formData.IsDialog;
                if (!isFormShow)
                {
                    wrapperAction();
                }
                else
                {
                    try
                    {
                        revitData.ExternalEventHandler.Action = wrapperAction;
                        revitData.ExternalEvent!.Raise();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message}\n{ex.StackTrace}");
                    }
                }
            }
        }
    }
}
