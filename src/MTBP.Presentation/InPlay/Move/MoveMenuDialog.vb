Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class MoveMenuDialog
    Inherits PickerDialog

    Public Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource)
        MyBase.New(context, model, exitDialog, "Move Which Way?")
    End Sub

    Protected Overrides ReadOnly Property Launchers As IEnumerable(Of LaunchDelegate)
        Get
            Dim choices As New List(Of LaunchDelegate) From
                {
                    AddressOf ChooseCancel
                }
            choices.AddRange(Model.Exits.Select(AddressOf MakeExitChoice))
            Return choices
        End Get
    End Property

    Private Function MakeExitChoice(exitModel As IExitModel) As LaunchDelegate
        Return Function(c, m, e)
                   Return DialogChoice.Create(True, exitModel.Text, MoveActivity.Launch(c, m, e, exitModel.Direction))
               End Function
    End Function

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As DialogSource
        Return Function() New MoveMenuDialog(context, model, exitDialog)
    End Function

    Private Shared Function ChooseCancel(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(True, "Cancel", NeutralDialog.Launch(context, model, exitDialog))
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Throw New NotImplementedException()
    End Function
End Class
