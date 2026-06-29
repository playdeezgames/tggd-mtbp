Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class DropItemActivity
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Private ReadOnly ItemModel As IItemModel

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, itemModel As IItemModel)
        MyBase.New(context, model, exitDialog)
        Me.ItemModel = itemModel
    End Sub

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, itemModel As IItemModel) As DialogSource
        Return Function() New DropItemActivity(context, model, exitDialog, itemModel)
    End Function

    Public Overrides Function Run() As IDialogPrompt
        ItemModel.Drop()
        Return InventoryMenu.Launch(Context, Model, ExitDialog).Invoke().Run()
    End Function
End Class
