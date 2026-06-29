Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class FeaturePlaceItemMenu
    Inherits PickerMenu

    Private ReadOnly FeatureModel As IFeatureModel

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, featureModel As IFeatureModel)
        MyBase.New(context, model, exitDialog, $"Place what item on {featureModel.Text}?")
        Me.FeatureModel = featureModel
    End Sub

    Protected Overrides ReadOnly Property Launchers As IEnumerable(Of LaunchDelegate)
        Get
            Return Enumerable.Empty(Of LaunchDelegate).
                Append(AddressOf ChooseNeverMind).
                Concat(Model.Inventory.Items.Select(AddressOf ChooseItem))
        End Get
    End Property

    Private Function ChooseItem(itemModel As IItemModel) As LaunchDelegate
        Return Function(c, m, e) DialogChoice.Create(True, itemModel.Text, FeaturePlaceItemActivity.Launch(c, m, e, FeatureModel, itemModel))
    End Function

    Private Function ChooseNeverMind(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(True, "Never Mind", FeatureMenu.Launch(context, model, exitDialog, FeatureModel))
    End Function

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, featureModel As IFeatureModel) As DialogSource
        Return Function() New FeaturePlaceItemMenu(context, model, exitDialog, featureModel)
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New FeaturePlaceItemMenu(Context, Model, ExitDialog, FeatureModel)
    End Function
End Class
