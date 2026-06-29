Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class GameMenu
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource)
        MyBase.New(context, model, exitDialog)
    End Sub

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As DialogSource
        Return Function() New GameMenu(context, model, exitDialog)
    End Function

    Public Overrides Function Run() As IDialogPrompt
        Return DialogPrompt.CreateChoicePrompt(
            "Game Menu:",
            DialogChoice.Create(True, "Continue Game", NeutralActivity.Launch(Context, Model, ExitDialog)),
            DialogChoice.Create(True, "Abandon Game", ConfirmDialog(Of IDisplayContext).Launch(Context, "Are you sure you want to abandon?", AbandonGameActivity.Launch(Context, Model, ExitDialog), Launch(Context, Model, ExitDialog))))
    End Function
End Class
