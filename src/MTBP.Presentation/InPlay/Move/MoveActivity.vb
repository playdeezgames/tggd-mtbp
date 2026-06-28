Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class MoveActivity
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Private ReadOnly Direction As String

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, direction As String)
        MyBase.New(context, model, exitDialog)
        Me.Direction = direction
    End Sub

    Friend Shared Function Launch(c As IDisplayContext, m As IWorldModel, e As DialogSource, direction As String) As DialogSource
        Return Function() New MoveActivity(c, m, e, direction)
    End Function

    Public Overrides Function Run() As IDialogPrompt
        Model.Move(Direction)
        Return NeutralDialog.Launch(Context, Model, ExitDialog).Invoke().Run()
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New MoveActivity(Context, Model, ExitDialog, Direction)
    End Function
End Class
