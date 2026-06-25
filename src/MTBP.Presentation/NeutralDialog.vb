Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class NeutralDialog
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog))
        MyBase.New(context, model, exitDialog)
    End Sub

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog)) As Func(Of IDialog)
        Return Function() New NeutralDialog(context, model, exitDialog)
    End Function

    Public Overrides Function Run() As IDialogPrompt
        Return GameMenuDialog.Launch(Context, Model, ExitDialog).Invoke().Run()
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New NeutralDialog(Context, Model, ExitDialog)
    End Function
End Class
