Imports MTBP.Provision

Friend Class Item
    Inherits MTBPEntity(Of ItemData)
    Implements IItem

    Private Sub New(world As IWorld, data As WorldData, itemId As Guid)
        MyBase.New(world, data)
        Me.ItemId = itemId
    End Sub

    Public ReadOnly Property ItemId As Guid Implements IItem.ItemId

    Public Property Inventory As IInventory Implements IItem.Inventory
        Get
            Return Persistence.Inventory.Create(World, _data, Data.InventoryId)
        End Get
        Set(value As IInventory)
            _data.Inventories(Data.InventoryId).ItemIds.Remove(ItemId)
            Data.InventoryId = value.InventoryId
            _data.Inventories(Data.InventoryId).ItemIds.Add(ItemId)
        End Set
    End Property

    Protected Overrides ReadOnly Property Data As ItemData
        Get
            Return _data.Items(ItemId)
        End Get
    End Property

    Friend Shared Function Create(world As IWorld, data As WorldData, itemId As Guid) As IItem
        Return New Item(world, data, itemId)
    End Function
End Class
