Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class GameMenuDialog
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource)
        MyBase.New(context, model, exitDialog)
    End Sub

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As DialogSource
        Return Function() New GameMenuDialog(context, model, exitDialog)
    End Function

    Public Overrides Function Run() As IDialogPrompt
        Return DialogPrompt.CreateChoicePrompt(
            "Game Menu:",
            DialogChoice.Create(True, "Continue Game", NeutralDialog.Launch(Context, Model, ExitDialog)),
            DialogChoice.Create(True, "Abandon Game", ConfirmDialog(Of IDisplayContext).Launch(Context, "Are you sure you want to abandon?", AbandonGameDialog.Launch(Context, Model, ExitDialog), AddressOf Relaunch)))
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New GameMenuDialog(Context, Model, ExitDialog)
    End Function
End Class
