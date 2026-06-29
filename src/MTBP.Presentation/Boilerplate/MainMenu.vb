Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class MainMenu
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Public Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource)
        MyBase.New(context, model, exitDialog)
    End Sub

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As DialogSource
        Return Function() New MainMenu(context, model, exitDialog)
    End Function

    Public Overrides Function Run() As IDialogPrompt
        Return DialogPrompt.CreateChoicePrompt(
            "Main Menu:",
            DialogChoice.Create(True, "Embark!", EmbarkActivity.Launch(Context, Model, ExitDialog)),
            DialogChoice.Create(Model.IsQuittable, "Quit", ConfirmDialog(Of IDisplayContext).Launch(Context, "Are you sure you want to quit?", ExitDialog, AddressOf Relaunch)))
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New MainMenu(Context, Model, ExitDialog)
    End Function
End Class
