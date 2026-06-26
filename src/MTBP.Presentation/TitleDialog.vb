Imports MTBP.Processing
Imports TGGD.Presentation

Public Class TitleDialog
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)
    Implements IDialog

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource)
        MyBase.New(context, model, exitDialog)
    End Sub

    Public Overrides Function Run() As IDialogPrompt
        Context.Render("Toxic City of SPLORR!!")
        Return DialogPrompt.CreateChoicePrompt(
            "",
            DialogChoice.Create(True, "OK", MainMenuDialog.Launch(Context, Model, ExitDialog)))
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New TitleDialog(Context, Model, ExitDialog)
    End Function

    Public Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As DialogSource
        Return Function() New TitleDialog(context, model, exitDialog)
    End Function
End Class
