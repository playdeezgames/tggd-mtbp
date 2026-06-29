Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class LookActivity
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource)
        MyBase.New(context, model, exitDialog)
    End Sub

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As DialogSource
        Return Function() New LookActivity(context, model, exitDialog)
    End Function

    Public Overrides Function Run() As IDialogPrompt
        Model.Look()
        Return NeutralActivity.Launch(Context, Model, ExitDialog).Invoke().Run
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New LookActivity(Context, Model, ExitDialog)
    End Function
End Class
