Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class FeaturesMenuDialog
    Inherits PickerDialog

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource)
        MyBase.New(context, model, exitDialog, "Which Feature?")
    End Sub

    Protected Overrides ReadOnly Property Launchers As IEnumerable(Of LaunchDelgate)
        Get
            Return Enumerable.Empty(Of LaunchDelgate).
                Append(AddressOf ChooseNeverMind).
                Concat(Model.Features.Select(AddressOf ChooseFeature))
        End Get
    End Property
    Private Function ChooseFeature(featureModel As IFeatureModel) As LaunchDelgate
        Return Function(c, m, e) DialogChoice.Create(True, featureModel.Text, FeatureMenuDialog.Launch(c, m, e, featureModel))
    End Function

    Private Function ChooseNeverMind(
                                    context As IDisplayContext,
                                    model As IWorldModel,
                                    exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(True, "Never Mind", LookActivity.Launch(context, model, exitDialog))
    End Function

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As DialogSource
        Return Function()
                   If model.HasFeatures Then
                       Return New FeaturesMenuDialog(context, model, exitDialog)
                   End If
                   Return LookActivity.Launch(context, model, exitDialog).Invoke()
               End Function
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New FeaturesMenuDialog(Context, Model, ExitDialog)
    End Function
End Class
