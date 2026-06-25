Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class NavigationDialog
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog))
        MyBase.New(context, model, exitDialog)
    End Sub

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog)) As Func(Of IDialog)
        Return Function() New NavigationDialog(context, model, exitDialog)
    End Function

    Public Overrides Function Run() As IDialogPrompt
        For Each message In Model.Messages
            Context.Render(message)
        Next
        'TODO: choices come from model!
        Return DialogPrompt.CreateChoicePrompt(
            "Now What?",
            DialogChoice.Create(True, "Game Menu", GameMenuDialog.Launch(Context, Model, ExitDialog)))
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New NavigationDialog(Context, Model, ExitDialog)
    End Function
End Class
