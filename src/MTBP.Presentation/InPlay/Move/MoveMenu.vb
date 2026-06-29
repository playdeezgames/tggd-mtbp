Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class MoveMenu
    Inherits PickerMenu

    Public Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource)
        MyBase.New(context, model, exitDialog, "Move Which Way?")
    End Sub

    Protected Overrides ReadOnly Property Launchers As IEnumerable(Of LaunchDelegate)
        Get
            Dim choices As New List(Of LaunchDelegate) From
                {
                    AddressOf ChooseCancel
                }
            choices.AddRange(Model.Exits.All.Select(AddressOf MakeExitChoice))
            Return choices
        End Get
    End Property

    Private Function MakeExitChoice(exitModel As IExitModel) As LaunchDelegate
        Return Function(c, m, e)
                   Return DialogChoice.Create(True, exitModel.Text, MoveActivity.Launch(c, m, e, exitModel.Direction))
               End Function
    End Function

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As DialogSource
        Return Function() New MoveMenu(context, model, exitDialog)
    End Function

    Private Shared Function ChooseCancel(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(True, "Cancel", NeutralActivity.Launch(context, model, exitDialog))
    End Function
End Class
