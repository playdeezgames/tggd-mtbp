Imports BROS.Processing
Imports TGGD.Presentation

Friend Class InPlayDialog
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog))
        MyBase.New(context, model, exitDialog)
    End Sub

    Public Overrides Function Run() As IDialogPrompt
        For Each message In Model.Messages
            Context.Render(message.Text, message.Mood, message.NewLine)
        Next
        Return DialogPrompt.CreateStringPrompt("Now What?", AddressOf ProcessCommand)
    End Function

    Private Function ProcessCommand(command As String) As IDialog
        Model.ProcessCommand(command)
        Return NeutralDialog.Launch(Context, Model, ExitDialog).Invoke()
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New InPlayDialog(Context, Model, ExitDialog)
    End Function

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog)) As Func(Of IDialog)
        Return Function() New InPlayDialog(context, model, exitDialog)
    End Function
End Class
