Imports BROS.Processing
Imports TGGD.Presentation

Friend Class NeutralDialog
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog))
        MyBase.New(context, model, exitDialog)
    End Sub

    Public Overrides Function Run() As IDialogPrompt
        If Model.IsMenuRequested Then
            Return ExitDialog.Invoke().Run()
        End If
        Return InPlayDialog.Launch(Context, Model, ExitDialog).Invoke().Run()
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New NeutralDialog(Context, Model, ExitDialog)
    End Function

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog)) As Func(Of IDialog)
        Return Function() New NeutralDialog(context, model, exitDialog)
    End Function
End Class
