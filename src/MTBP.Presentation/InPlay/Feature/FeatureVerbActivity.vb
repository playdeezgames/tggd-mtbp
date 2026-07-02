Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class FeatureVerbActivity
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)

    Private ReadOnly FeatureModel As IFeatureModel
    Private ReadOnly VerbModel As IVerbModel

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, featureModel As IFeatureModel, verbModel As IVerbModel)
        MyBase.New(context, model, exitDialog)
        Me.FeatureModel = featureModel
        Me.VerbModel = verbModel
    End Sub

    Friend Shared Function Launch(c As IDisplayContext, m As IWorldModel, e As DialogSource, featureModel As IFeatureModel, verbModel As IVerbModel) As DialogSource
        Return Function() New FeatureVerbActivity(c, m, e, featureModel, verbModel)
    End Function

    Public Overrides Function Run() As IDialogPrompt
        VerbModel.Perform(FeatureModel)
        Return FeatureMenu.Launch(Context, Model, ExitDialog, FeatureModel).Invoke().Run()
    End Function
End Class
