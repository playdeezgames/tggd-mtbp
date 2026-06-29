Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class FeatureTakeItemsMenuDialog
    Inherits PickerDialog

    Private ReadOnly FeatureModel As IFeatureModel

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, featureModel As IFeatureModel)
        MyBase.New(context, model, exitDialog, $"Take which item from {featureModel.Text}?")
        Me.FeatureModel = featureModel
    End Sub

    Protected Overrides ReadOnly Property Launchers As IEnumerable(Of LaunchDelegate)
        Get
            Return Enumerable.Empty(Of LaunchDelegate).
                Append(AddressOf ChooseNeverMind).
                Concat(FeatureModel.InventoryItems.Select(AddressOf ChooseItem))
        End Get
    End Property

    Private Function ChooseItem(itemModel As IInventoryItemModel) As LaunchDelegate
        Return Function(c, m, e) DialogChoice.Create(True, itemModel.Text, FeatureTakeItemActivity.Launch(c, m, e, FeatureModel, itemModel))
    End Function

    Private Function ChooseNeverMind(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(True, "Never Mind", FeatureMenuDialog.Launch(context, model, exitDialog, FeatureModel))
    End Function

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, featureModel As IFeatureModel) As DialogSource
        Return Function() New FeatureTakeItemsMenuDialog(context, model, exitDialog, featureModel)
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New FeatureTakeItemsMenuDialog(Context, Model, ExitDialog, FeatureModel)
    End Function
End Class
