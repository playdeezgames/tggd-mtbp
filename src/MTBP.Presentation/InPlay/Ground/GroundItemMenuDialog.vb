Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class GroundItemMenuDialog
    Inherits LauncherModelDialog

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, itemModel As IGroundItemModel)
        MyBase.New(context, model, exitDialog, $"What to do with {itemModel.Text}?")
        Me.ItemModel = itemModel
    End Sub

    Public ReadOnly Property ItemModel As IGroundItemModel

    Protected Overrides ReadOnly Property Launchers As IEnumerable(Of LaunchDelgate)
        Get
            Return Enumerable.Empty(Of LaunchDelgate).
                Append(AddressOf ChooseNeverMind)
        End Get
    End Property

    Private Function ChooseNeverMind(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(True, "Never Mind", GroundMenuDialog.Launch(context, model, exitDialog))
    End Function

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, itemModel As IGroundItemModel) As DialogSource
        Return Function() New GroundItemMenuDialog(context, model, exitDialog, itemModel)
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New GroundItemMenuDialog(Context, Model, ExitDialog, itemModel)
    End Function
End Class
