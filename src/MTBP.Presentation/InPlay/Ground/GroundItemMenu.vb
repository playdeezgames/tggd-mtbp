Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class GroundItemMenu
    Inherits PickerMenu

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, itemModel As IItemModel)
        MyBase.New(context, model, exitDialog, $"What to do with {itemModel.Text}?")
        Me.ItemModel = itemModel
    End Sub

    Public ReadOnly Property ItemModel As IItemModel

    Protected Overrides ReadOnly Property Launchers As IEnumerable(Of LaunchDelegate)
        Get
            Return Enumerable.Empty(Of LaunchDelegate).
                Append(AddressOf ChooseNeverMind).
                Append(AddressOf ChooseTake)
        End Get
    End Property

    Private Function ChooseTake(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(True, "Take", TakeItemActivity.Launch(context, model, exitDialog, ItemModel))
    End Function

    Private Function ChooseNeverMind(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(True, "Never Mind", GroundMenu.Launch(context, model, exitDialog))
    End Function

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, itemModel As IItemModel) As DialogSource
        Return Function() New GroundItemMenu(context, model, exitDialog, itemModel)
    End Function
End Class
