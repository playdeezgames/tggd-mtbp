Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class StatusActivity
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource)
        MyBase.New(context, model, exitDialog)
    End Sub

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As DialogSource
        Return Function() New StatusActivity(context, model, exitDialog)
    End Function

    Public Overrides Function Run() As IDialogPrompt
        Model.Views.ShowStatus()
        Return NeutralActivity.Launch(Context, Model, ExitDialog).Invoke().Run
    End Function
End Class
