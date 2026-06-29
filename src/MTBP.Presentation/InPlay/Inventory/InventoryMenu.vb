Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class InventoryMenu
    Inherits PickerMenu

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource)
        MyBase.New(context, model, exitDialog, "Items in Inventory:")
    End Sub

    Protected Overrides ReadOnly Property Launchers As IEnumerable(Of LaunchDelegate)
        Get
            Return Enumerable.Empty(Of LaunchDelegate).
                Append(AddressOf ChooseNeverMind).
                Concat(Model.Inventory.Items.Select(AddressOf ChooseItem))
        End Get
    End Property

    Private Function ChooseNeverMind(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(True, "Never Mind", NeutralActivity.Launch(context, model, exitDialog))
    End Function

    Private Function ChooseItem(itemModel As IItemModel) As LaunchDelegate
        Return Function(c, m, e) DialogChoice.Create(True, itemModel.Text, InventoryItemMenu.Launch(c, m, e, itemModel))
    End Function

    Friend Shared Function Launch(
                                 context As IDisplayContext,
                                 model As IWorldModel,
                                 exitDialog As DialogSource) As DialogSource
        Return Function()
                   If model.Inventory.HasItems Then
                       Return New InventoryMenu(context, model, exitDialog)
                   End If
                   Return LookActivity.Launch(context, model, exitDialog, False).Invoke()
               End Function
    End Function
End Class
