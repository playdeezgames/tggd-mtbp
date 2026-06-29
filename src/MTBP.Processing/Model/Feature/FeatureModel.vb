Imports MTBP.Persistence

Friend Class FeatureModel
    Implements IFeatureModel
    Private Sub New(feature As IFeature)
        Me.feature = feature
    End Sub

    Friend Shared Function Create(feature As IFeature) As IFeatureModel
        Return New FeatureModel(feature)
    End Function

    Public ReadOnly Property Text As String Implements IFeatureModel.Text
        Get
            Return feature.GetName
        End Get
    End Property

    Private ReadOnly feature As IFeature
End Class
