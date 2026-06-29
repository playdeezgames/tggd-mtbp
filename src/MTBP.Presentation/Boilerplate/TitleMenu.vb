Imports MTBP.Processing
Imports TGGD.Presentation

Public Class TitleMenu
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)
    Implements IDialog

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource)
        MyBase.New(context, model, exitDialog)
    End Sub

    Public Overrides Function Run() As IDialogPrompt
        Context.Render("Toxic City of SPLORR!!")
        Return DialogPrompt.CreateChoicePrompt(
            "",
            DialogChoice.Create(True, "OK", MainMenu.Launch(Context, Model, ExitDialog)))
    End Function

    Public Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As DialogSource
        Return Function() New TitleMenu(context, model, exitDialog)
    End Function
End Class
