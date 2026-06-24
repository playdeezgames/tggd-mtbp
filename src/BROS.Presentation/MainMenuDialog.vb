Imports BROS.Processing
Imports TGGD.Presentation

Friend Class MainMenuDialog
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog))
        MyBase.New(context, model, exitDialog)
    End Sub

    Public Overrides Function Run() As IDialogPrompt
        Return DialogPrompt.CreateChoicePrompt(
            "Main Menu",
            DialogChoice.Create(Model.IsInPlay, "Continue Game", NeutralDialog.Launch(Context, Model, AddressOf Relaunch)),
            DialogChoice.Create(Not Model.IsInPlay, "Embark!", EmbarkDialog.Launch(Context, Model, AddressOf Relaunch)),
            DialogChoice.Create(Model.IsQuittable, "Quit", ConfirmDialog(Of IDisplayContext).Launch(Context, "Are you sure you want to quit?", ExitDialog, AddressOf Relaunch)))
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New MainMenuDialog(Context, Model, ExitDialog)
    End Function

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog)) As Func(Of IDialog)
        Return Function() New MainMenuDialog(context, model, exitDialog)
    End Function
End Class
