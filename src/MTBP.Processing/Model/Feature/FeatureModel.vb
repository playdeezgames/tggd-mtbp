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

    Public ReadOnly Property HasItems As Boolean Implements IFeatureModel.HasItems
        Get
            Return feature.Inventory.HasItems
        End Get
    End Property

    Public ReadOnly Property InventoryItems As IEnumerable(Of IItemModel) Implements IFeatureModel.InventoryItems
        Get
            Return feature.Inventory.Items.Select(AddressOf ItemModel.Create)
        End Get
    End Property

    Public ReadOnly Property CanInteract As Boolean Implements IFeatureModel.CanInteract
        Get
            Return Not feature.World.Avatar.IsDead
        End Get
    End Property

    Public ReadOnly Property Verbs As IEnumerable(Of IVerbModel) Implements IFeatureModel.Verbs
        Get
            Return feature.Verbs.Select(AddressOf VerbModel.Create)
        End Get
    End Property

    Friend ReadOnly feature As IFeature
End Class
