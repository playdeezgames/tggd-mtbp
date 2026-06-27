Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class DropItemMenuDialog
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Private ReadOnly ItemModel As IInventoryItemModel

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, itemModel As IInventoryItemModel)
        MyBase.New(context, model, exitDialog)
        Me.ItemModel = itemModel
    End Sub

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, itemModel As IInventoryItemModel) As DialogSource
        Return Function() New DropItemMenuDialog(context, model, exitDialog, itemModel)
    End Function

    Public Overrides Function Run() As IDialogPrompt
        ItemModel.Drop()
        Return InventoryMenuDialog.Launch(Context, Model, ExitDialog).Invoke().Run()
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New DropItemMenuDialog(Context, Model, ExitDialog, ItemModel)
    End Function
End Class
