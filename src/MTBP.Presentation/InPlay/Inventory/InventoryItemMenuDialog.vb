Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class InventoryItemMenuDialog
    Inherits PickerDialog

    Private ReadOnly ItemModel As IInventoryItemModel

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, itemModel As IInventoryItemModel)
        MyBase.New(context, model, exitDialog, $"What to do with {itemModel.Text}?")
        Me.ItemModel = itemModel
    End Sub

    Protected Overrides ReadOnly Property Launchers As IEnumerable(Of LaunchDelegate)
        Get
            Return Enumerable.Empty(Of LaunchDelegate).
                Append(AddressOf ChooseNeverMind).
                Append(AddressOf ChooseDrop)
        End Get
    End Property

    Private Function ChooseDrop(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(True, "Drop", DropItemActivity.Launch(context, model, exitDialog, ItemModel))
    End Function

    Private Function ChooseNeverMind(
                                    context As IDisplayContext,
                                    model As IWorldModel,
                                    exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(True, "Never Mind", InventoryMenuDialog.Launch(context, model, exitDialog))
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New InventoryItemMenuDialog(Context, Model, ExitDialog, ItemModel)
    End Function

    Friend Shared Function Launch(c As IDisplayContext, m As IWorldModel, e As DialogSource, itemModel As IInventoryItemModel) As DialogSource
        Return Function() New InventoryItemMenuDialog(c, m, e, itemModel)
    End Function
End Class
