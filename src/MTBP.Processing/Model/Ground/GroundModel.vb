Imports MTBP.Persistence
Imports TGGD.Processing

Friend Class GroundModel
    Inherits BaseModel(Of IWorld)
    Implements IGroundModel

    Protected Sub New(entity As IWorld)
        MyBase.New(entity)
    End Sub

    Public ReadOnly Property HasItems As Boolean Implements IGroundModel.HasItems
        Get
            Return Not Entity.Avatar.IsDead AndAlso Entity.Avatar.Location.Inventory.HasItems
        End Get
    End Property

    Public ReadOnly Property Items As IEnumerable(Of IItemModel) Implements IGroundModel.Items
        Get
            Return Entity.Avatar.Location.Inventory.Items.Select(AddressOf ItemModel.Create)
        End Get
    End Property

    Friend Shared Function Create(entity As IWorld) As IGroundModel
        Return New GroundModel(entity)
    End Function
End Class
