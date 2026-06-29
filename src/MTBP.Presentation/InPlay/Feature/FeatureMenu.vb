Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class FeatureMenu
    Inherits PickerMenu

    Private ReadOnly FeatureModel As IFeatureModel

    Public Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, featureModel As IFeatureModel)
        MyBase.New(context, model, exitDialog, $"Do what with {featureModel.Text}?")
        Me.FeatureModel = featureModel
    End Sub

    Protected Overrides ReadOnly Property Launchers As IEnumerable(Of LaunchDelegate)
        Get
            Return Enumerable.Empty(Of LaunchDelegate).
                Append(AddressOf ChooseNeverMind).
                Append(AddressOf ChoosePlaceItem).
                Append(AddressOf ChooseTakeItem)
        End Get
    End Property

    Private Function ChooseTakeItem(
                                   context As IDisplayContext,
                                   model As IWorldModel,
                                   exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(
            FeatureModel.HasItems,
            "Take Item...",
            FeatureTakeItemMenu.Launch(context, model, exitDialog, FeatureModel))
    End Function

    Private Function ChoosePlaceItem(
                                    context As IDisplayContext,
                                    model As IWorldModel,
                                    exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(
            model.Inventory.HasItems,
            "Place Item...",
            FeaturePlaceItemMenu.Launch(context, model, exitDialog, FeatureModel))
    End Function

    Public Overrides Function Run() As IDialogPrompt
        FeatureModel.Describe()
        Return MyBase.Run()
    End Function

    Private Function ChooseNeverMind(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(True, "Never Mind", FeaturesMenu.Launch(context, model, exitDialog))
    End Function

    Friend Shared Function Launch(c As IDisplayContext, m As IWorldModel, e As DialogSource, featureModel As IFeatureModel) As DialogSource
        Return Function() New FeatureMenu(c, m, e, featureModel)
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New FeatureMenu(Context, Model, ExitDialog, FeatureModel)
    End Function
End Class
