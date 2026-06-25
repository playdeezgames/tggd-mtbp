Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class MoveMenuDialog
    Inherits LauncherModelDialog

    Public Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog))
        MyBase.New(context, model, exitDialog, "Move Which Way?")
    End Sub

    Protected Overrides ReadOnly Property Launchers As IEnumerable(Of Func(Of IDisplayContext, IWorldModel, Func(Of IDialog), IDialogChoice))
        Get
            Return {
                AddressOf ChooseCancel
                }
        End Get
    End Property

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog)) As Func(Of IDialog)
        Return Function() New MoveMenuDialog(context, model, exitDialog)
    End Function

    Private Shared Function ChooseCancel(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog)) As IDialogChoice
        Return DialogChoice.Create(True, "Cancel", NeutralDialog.Launch(context, model, exitDialog))
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Throw New NotImplementedException()
    End Function
End Class
