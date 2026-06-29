Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class TakeItemActivity
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Private Sub New(
                   context As IDisplayContext,
                   model As IWorldModel,
                   exitDialog As DialogSource,
                   itemModel As IItemModel)
        MyBase.New(context, model, exitDialog)
        Me.ItemModel = itemModel
    End Sub

    Private ReadOnly ItemModel As IItemModel

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, itemModel As IItemModel) As DialogSource
        Return Function() New TakeItemActivity(context, model, exitDialog, itemModel)
    End Function

    Public Overrides Function Run() As IDialogPrompt
        ItemModel.Take()
        Return GroundMenuDialog.Launch(Context, Model, ExitDialog).Invoke().Run()
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New TakeItemActivity(Context, Model, ExitDialog, ItemModel)
    End Function
End Class
