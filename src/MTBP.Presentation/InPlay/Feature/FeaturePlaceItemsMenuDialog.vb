Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class FeaturePlaceItemsMenuDialog
    Inherits PickerDialog

    Private ReadOnly FeatureModel As IFeatureModel

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, featureModel As IFeatureModel)
        MyBase.New(context, model, exitDialog, $"Place what item on {featureModel.Text}?")
        Me.FeatureModel = featureModel
    End Sub

    Protected Overrides ReadOnly Property Launchers As IEnumerable(Of LaunchDelgate)
        Get
            Return Enumerable.Empty(Of LaunchDelgate).
                Append(AddressOf ChooseNeverMind).
                Concat(Model.InventoryItems.Select(AddressOf ChooseItem))
        End Get
    End Property

    Private Function ChooseItem(itemModel As IInventoryItemModel) As LaunchDelgate
        Return Function(c, m, e) DialogChoice.Create(True, itemModel.Text, FeaturePlaceItemActivity.Launch(c, m, e, FeatureModel, itemModel))
    End Function

    Private Function ChooseNeverMind(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(True, "Never Mind", FeatureMenuDialog.Launch(context, model, exitDialog, FeatureModel))
    End Function

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, featureModel As IFeatureModel) As DialogSource
        Return Function() New FeaturePlaceItemsMenuDialog(context, model, exitDialog, featureModel)
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New FeaturePlaceItemsMenuDialog(Context, Model, ExitDialog, FeatureModel)
    End Function
End Class
