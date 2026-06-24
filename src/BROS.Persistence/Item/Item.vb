Imports BROS.Provision

Friend Class Item
    Inherits BROSEntity(Of ItemData)
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
            RemoveFromInventoryAndEquipSlot()
            If value IsNot Nothing Then
                Data.InventoryId = value.InventoryId
                _data.Inventories(Data.InventoryId.Value).ItemIds.Add(ItemId)
            End If
        End Set
    End Property

    Private Sub RemoveFromInventoryAndEquipSlot()
        If Data.InventoryId.HasValue Then
            _data.Inventories(Data.InventoryId.Value).ItemIds.Remove(ItemId)
            Data.InventoryId = Nothing
        End If
        If Data.EquipSlotId.HasValue Then
            _data.EquipSlots(Data.EquipSlotId.Value).ItemId = Nothing
            Data.EquipSlotId = Nothing
        End If
    End Sub

    Public Property EquipSlot As IEquipSlot Implements IItem.EquipSlot
        Get
            Return Persistence.EquipSlot.Create(World, _data, Data.EquipSlotId)
        End Get
        Set(value As IEquipSlot)
            RemoveFromInventoryAndEquipSlot()
            If value IsNot Nothing Then
                Data.EquipSlotId = value.EquipSlotId
                _data.EquipSlots(Data.EquipSlotId.Value).ItemId = ItemId
            End If
        End Set
    End Property

    Protected Overrides ReadOnly Property Data As ItemData
        Get
            Return _data.Items(ItemId)
        End Get
    End Property

    Friend Shared Function Create(
                                 world As IWorld,
                                 data As WorldData,
                                 itemId As Guid?) As IItem
        Return If(
            itemId.HasValue,
            New Item(world, data, itemId.Value),
            Nothing)
    End Function

    Public Sub Destroy() Implements IItem.Destroy
        RemoveFromInventoryAndEquipSlot()
        _data.Items.Remove(ItemId)
    End Sub
End Class
