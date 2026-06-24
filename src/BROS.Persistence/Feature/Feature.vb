Imports BROS.Provision

Friend Class Feature
    Inherits InventoryEntity(Of FeatureData)
    Implements IFeature

    Private Sub New(world As IWorld, data As WorldData, featureId As Guid)
        MyBase.New(world, data)
        Me.FeatureId = featureId
    End Sub

    Public ReadOnly Property FeatureId As Guid Implements IFeature.FeatureId

    Protected Overrides ReadOnly Property Data As FeatureData
        Get
            Return _data.Features(FeatureId)
        End Get
    End Property

    Friend Shared Function Create(world As IWorld, data As WorldData, featureId As Guid) As IFeature
        Return New Feature(world, data, featureId)
    End Function
End Class
