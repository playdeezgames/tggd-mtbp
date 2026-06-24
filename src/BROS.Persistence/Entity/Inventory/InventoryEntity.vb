Imports BROS.Provision

Friend MustInherit Class InventoryEntity(Of TData As InventoryEntityData)
    Inherits BROSEntity(Of TData)
    Implements IInventoryEntity

    Protected Sub New(world As IWorld, data As WorldData)
        MyBase.New(world, data)
    End Sub

    Public ReadOnly Property Inventory As IInventory Implements IInventoryEntity.Inventory
        Get
            If Not Data.InventoryId.HasValue Then
                Data.InventoryId = Guid.NewGuid
                _data.Inventories(Data.InventoryId.Value) = New InventoryData
            End If
            Return Persistence.Inventory.Create(World, _data, Data.InventoryId.Value)
        End Get
    End Property
End Class
