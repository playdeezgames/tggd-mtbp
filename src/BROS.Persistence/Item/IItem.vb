Public Interface IItem
    Inherits IBROSEntity
    ReadOnly Property ItemId As Guid
    Property Inventory As IInventory
    Property EquipSlot As IEquipSlot
    Sub Destroy()
End Interface
