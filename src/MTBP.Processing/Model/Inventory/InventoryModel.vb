Imports MTBP.Persistence
Imports TGGD.Processing

Friend Class InventoryModel
    Inherits BaseModel(Of IWorld)
    Implements IInventoryModel

    Protected Sub New(entity As IWorld)
        MyBase.New(entity)
    End Sub

    Public ReadOnly Property HasItems As Boolean Implements IInventoryModel.HasItems
        Get
            Return Not Entity.Avatar.IsDead AndAlso Entity.Avatar.Inventory.HasItems
        End Get
    End Property

    Public ReadOnly Property Items As IEnumerable(Of IItemModel) Implements IInventoryModel.Items
        Get
            Return Entity.Avatar.Inventory.Items.Select(AddressOf ItemModel.Create)
        End Get
    End Property

    Friend Shared Function Create(entity As IWorld) As IInventoryModel
        Return New InventoryModel(entity)
    End Function
End Class
