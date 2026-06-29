Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class NeutralActivity
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource)
        MyBase.New(context, model, exitDialog)
    End Sub

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As DialogSource
        Return Function() New NeutralActivity(context, model, exitDialog)
    End Function

    Public Overrides Function Run() As IDialogPrompt
        Return NavigationMenu.Launch(Context, Model, ExitDialog).Invoke().Run()
    End Function
End Class
