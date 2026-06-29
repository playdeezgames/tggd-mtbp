Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class FeatureMenuDialog
    Inherits PickerDialog

    Private ReadOnly FeatureModel As IFeatureModel

    Public Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, featureModel As IFeatureModel)
        MyBase.New(context, model, exitDialog, $"Do what with {featureModel.Text}?")
        Me.FeatureModel = featureModel
    End Sub

    Protected Overrides ReadOnly Property Launchers As IEnumerable(Of LaunchDelgate)
        Get
            Return Enumerable.Empty(Of LaunchDelgate).
                Append(AddressOf ChooseNeverMind)
        End Get
    End Property

    Public Overrides Function Run() As IDialogPrompt
        FeatureModel.Describe()
        Return MyBase.Run()
    End Function

    Private Function ChooseNeverMind(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(True, "Never Mind", FeaturesMenuDialog.Launch(context, model, exitDialog))
    End Function

    Friend Shared Function Launch(c As IDisplayContext, m As IWorldModel, e As DialogSource, featureModel As IFeatureModel) As DialogSource
        Return Function() New FeatureMenuDialog(c, m, e, featureModel)
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New FeatureMenuDialog(Context, Model, ExitDialog, FeatureModel)
    End Function
End Class
