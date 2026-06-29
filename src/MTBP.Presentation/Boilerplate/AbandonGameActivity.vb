Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class AbandonGameActivity
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource)
        MyBase.New(context, model, exitDialog)
    End Sub

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As DialogSource
        Return Function() New AbandonGameActivity(context, model, exitDialog)
    End Function

    Public Overrides Function Run() As IDialogPrompt
        Model.Abandon()
        Return MainMenu.Launch(Context, Model, ExitDialog).Invoke().Run()
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New AbandonGameActivity(Context, Model, ExitDialog)
    End Function
End Class
