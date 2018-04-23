using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.XtraScheduler;
using DevExpress.Xpf.Scheduler.UI;
using DevExpress.Xpf.Scheduler;

namespace SilverlightApplication1 {
    public partial class MainPage : UserControl {
        public MainPage() {
            InitializeComponent();

            schedulerControl1.Start = new System.DateTime(2010, 7, 19, 0, 0, 0, 0);

        }

        #region #AppointmentCollectionModifiedEvent
        // Commit changes to the database.
        private void SchedulerStorage_AppointmentCollectionModified(object sender, EventArgs e) {
            if (domainDataSource1.HasChanges) {
                domainDataSource1.SubmitChanges();
            }
        }
        #endregion #AppointmentCollectionModifiedEvent

        private void DomainDataSource_LoadedData(object sender, LoadedDataEventArgs e) {
            if (e.HasError) {
                MessageBox.Show("Connection could not be established." + Environment.NewLine +
                                e.Error.Message, "Connection Error", MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }

        private void DomainDataSource_SubmittedChanges(object sender, SubmittedChangesEventArgs e) {
            if (e.HasError) {
                MessageBox.Show(e.Error.ToString(), "Submit Changes Error", MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }

        #region #RecurrenceFormShowingEvent
        private void schedulerControl1_RecurrenceFormShowing(object sender, RecurrenceFormEventArgs e) {
            AppointmentForm appointmentForm = e.ParentForm as AppointmentForm;
            e.Form = new CustomRecurrenceForm(appointmentForm.Controller);
        }
        #endregion #RecurrenceFormShowingEvent
    }
}
