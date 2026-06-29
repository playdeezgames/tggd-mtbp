Imports MTBP.Persistence
Imports TGGD.Processing

Friend Class FeaturesModel
    Inherits BaseModel(Of IWorld)
    Implements IFeaturesModel

    Protected Sub New(entity As IWorld)
        MyBase.New(entity)
    End Sub

    Public ReadOnly Property HasAny As Boolean Implements IFeaturesModel.HasAny
        Get
            Return Not Entity.Avatar.IsDead AndAlso Entity.Avatar.Location.HasFeatures
        End Get
    End Property

    Public ReadOnly Property All As IEnumerable(Of IFeatureModel) Implements IFeaturesModel.All
        Get
            Return Entity.Avatar.Location.Features.Select(Function(x) FeatureModel.Create(x))
        End Get
    End Property

    Friend Shared Function Create(entity As IWorld) As IFeaturesModel
        Return New FeaturesModel(entity)
    End Function
End Class
