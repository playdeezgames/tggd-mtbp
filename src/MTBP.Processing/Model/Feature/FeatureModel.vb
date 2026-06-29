Imports MTBP.Persistence

Friend Class FeatureModel
    Implements IFeatureModel
    Private Sub New(feature As IFeature)
        Me.feature = feature
    End Sub

    Friend Shared Function Create(feature As IFeature) As IFeatureModel
        Return New FeatureModel(feature)
    End Function

    Public Sub Describe() Implements IFeatureModel.Describe
        feature.World.Avatar.Describe(feature)
    End Sub

    Public ReadOnly Property Text As String Implements IFeatureModel.Text
        Get
            Return feature.GetName
        End Get
    End Property

    Friend ReadOnly feature As IFeature
End Class
