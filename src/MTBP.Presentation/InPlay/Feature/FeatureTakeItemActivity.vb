Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class FeatureTakeItemActivity
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Private ReadOnly FeatureModel As IFeatureModel
    Private ReadOnly ItemModel As IItemModel

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, featureModel As IFeatureModel, itemModel As IItemModel)
        MyBase.New(context, model, exitDialog)
        Me.FeatureModel = featureModel
        Me.ItemModel = itemModel
    End Sub

    Friend Shared Function Launch(c As IDisplayContext, m As IWorldModel, e As DialogSource, featureModel As IFeatureModel, itemModel As IItemModel) As DialogSource
        Return Function() New FeatureTakeItemActivity(c, m, e, featureModel, itemModel)
    End Function

    Public Overrides Function Run() As IDialogPrompt
        ItemModel.Take()
        Return FeatureMenu.Launch(Context, Model, ExitDialog, FeatureModel).Invoke().Run()
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New FeatureTakeItemActivity(Context, Model, ExitDialog, FeatureModel, ItemModel)
    End Function
End Class
