Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class LookActivity
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Private ReadOnly ClearMessages As Boolean

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, clearMessages As Boolean)
        MyBase.New(context, model, exitDialog)
        Me.ClearMessages = clearMessages
    End Sub

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, clearMessages As Boolean) As DialogSource
        Return Function() New LookActivity(context, model, exitDialog, clearMessages)
    End Function

    Public Overrides Function Run() As IDialogPrompt
        Model.Views.ShowLocation(ClearMessages)
        Return NeutralActivity.Launch(Context, Model, ExitDialog).Invoke().Run
    End Function
End Class
