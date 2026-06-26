Imports MTBP.Provision

Friend MustInherit Class InventoriedEntity(Of TData As InventoriedEntityData)
    Inherits MTBPEntity(Of TData)
    Implements IInventoriedEntity

    Protected Sub New(world As IWorld, data As WorldData)
        MyBase.New(world, data)
    End Sub

    Public ReadOnly Property Inventory As IInventory Implements IInventoriedEntity.Inventory
        Get
            Dim inventoryId = Data.InventoryId
            If Not inventoryId.HasValue Then
                inventoryId = Guid.NewGuid
                _data.Inventories(inventoryId.Value) = New InventoryData
                Data.InventoryId = inventoryId
            End If
            Return Persistence.Inventory.Create(World, _data, inventoryId)
        End Get
    End Property
End Class
