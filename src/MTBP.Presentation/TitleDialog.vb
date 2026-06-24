Imports MTBP.Processing
Imports TGGD.Presentation

Public Class TitleDialog
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)
    Implements IDialog

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog))
        MyBase.New(context, model, exitDialog)
    End Sub

    Public Overrides Function Run() As IDialogPrompt
        Context.Render("TODO: Game Title")
        Return DialogPrompt.CreateChoicePrompt("", DialogChoice.Create(True, "OK", AddressOf Relaunch))
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New TitleDialog(Context, Model, ExitDialog)
    End Function

    Public Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog)) As Func(Of IDialog)
        Return Function() New TitleDialog(context, model, exitDialog)
    End Function
End Class
