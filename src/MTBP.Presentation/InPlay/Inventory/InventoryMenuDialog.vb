Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class InventoryMenuDialog
    Inherits PickerMenu

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource)
        MyBase.New(context, model, exitDialog, "Items in Inventory:")
    End Sub

    Protected Overrides ReadOnly Property Launchers As IEnumerable(Of LaunchDelegate)
        Get
            Return Enumerable.Empty(Of LaunchDelegate).
                Append(AddressOf ChooseNeverMind).
                Concat(Model.InventoryItems.Select(AddressOf ChooseItem))
        End Get
    End Property

    Private Function ChooseNeverMind(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(True, "Never Mind", NeutralDialog.Launch(context, model, exitDialog))
    End Function

    Private Function ChooseItem(itemModel As IItemModel) As LaunchDelegate
        Return Function(c, m, e) DialogChoice.Create(True, itemModel.Text, InventoryItemMenuDialog.Launch(c, m, e, itemModel))
    End Function

    Friend Shared Function Launch(
                                 context As IDisplayContext,
                                 model As IWorldModel,
                                 exitDialog As DialogSource) As DialogSource
        Return Function()
                   If model.HasItems Then
                       Return New InventoryMenuDialog(context, model, exitDialog)
                   End If
                   Return NeutralDialog.Launch(context, model, exitDialog).Invoke()
               End Function
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New InventoryMenuDialog(Context, Model, ExitDialog)
    End Function
End Class
