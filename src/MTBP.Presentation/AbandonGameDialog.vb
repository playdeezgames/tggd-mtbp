Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class AbandonGameDialog
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog))
        MyBase.New(context, model, exitDialog)
    End Sub

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog)) As Func(Of IDialog)
        Return Function() New AbandonGameDialog(context, model, exitDialog)
    End Function

    Public Overrides Function Run() As IDialogPrompt
        Model.Abandon()
        Return MainMenuDialog.Launch(Context, Model, ExitDialog).Invoke().Run()
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New AbandonGameDialog(Context, Model, ExitDialog)
    End Function
End Class
