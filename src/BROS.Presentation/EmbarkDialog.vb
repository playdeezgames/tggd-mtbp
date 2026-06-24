Imports BROS.Processing
Imports TGGD.Presentation

Friend Class EmbarkDialog
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog))
        MyBase.New(context, model, exitDialog)
    End Sub

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog)) As Func(Of IDialog)
        Return Function() New EmbarkDialog(context, model, exitDialog)
    End Function

    Public Overrides Function Run() As IDialogPrompt
        Model.Embark()
        Return NeutralDialog.Launch(Context, Model, ExitDialog).Invoke.Run()
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New EmbarkDialog(Context, Model, ExitDialog)
    End Function
End Class
