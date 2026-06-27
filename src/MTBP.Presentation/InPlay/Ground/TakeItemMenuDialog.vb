Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class TakeItemMenuDialog
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Private Sub New(
                   context As IDisplayContext,
                   model As IWorldModel,
                   exitDialog As DialogSource,
                   itemModel As IGroundItemModel)
        MyBase.New(context, model, exitDialog)
        Me.ItemModel = itemModel
    End Sub

    Private ReadOnly ItemModel As IGroundItemModel

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, itemModel As IGroundItemModel) As DialogSource
        Return Function() New TakeItemMenuDialog(context, model, exitDialog, itemModel)
    End Function

    Public Overrides Function Run() As IDialogPrompt
        ItemModel.Take()
        Return GroundMenuDialog.Launch(Context, Model, ExitDialog).Invoke().Run()
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New TakeItemMenuDialog(Context, Model, ExitDialog, ItemModel)
    End Function
End Class
