Imports Microsoft.VisualBasic
Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.XtraScheduler
Imports DevExpress.Xpf.Scheduler.UI
Imports DevExpress.Xpf.Scheduler

Partial Public Class MainPage
    Inherits UserControl

    Public Sub New()
        InitializeComponent()

        schedulerControl1.Start = New DateTime(2010, 7, 19, 0, 0, 0, 0)
    End Sub

#Region "#AppointmentCollectionModifiedEvent"
    ' Commit changes to the database.
    Private Sub SchedulerStorage_AppointmentCollectionModified(sender As Object, e As EventArgs)
        If domainDataSource1.HasChanges Then
            domainDataSource1.SubmitChanges()
        End If
    End Sub
#End Region

    Private Sub DomainDataSource_LoadedData(sender As Object, _
                                            e As LoadedDataEventArgs)
        If e.HasError Then
            MessageBox.Show("Connection could not be established." _
                            & Environment.NewLine & e.Error.Message, "Connection Error", _
                            MessageBoxButton.OK)
            e.MarkErrorAsHandled()
        End If
    End Sub

    Private Sub DomainDataSource_SubmittedChanges(sender As Object, e As SubmittedChangesEventArgs)
        If e.HasError Then
            MessageBox.Show(e.Error.ToString(), "Submit Changes Error", MessageBoxButton.OK)
            e.MarkErrorAsHandled()
        End If
    End Sub

#Region "#RecurrenceFormShowingEvent"
    Private Sub schedulerControl1_RecurrenceFormShowing(sender As Object, _
                                                        e As RecurrenceFormEventArgs)
        Dim appointmentForm As AppointmentForm = TryCast(e.ParentForm, AppointmentForm)
        e.Form = New CustomRecurrenceForm(appointmentForm.Controller)
    End Sub
#End Region
End Class

