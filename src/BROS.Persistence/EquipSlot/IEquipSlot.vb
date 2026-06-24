Public Interface IEquipSlot
    Inherits IBaseInventory
    Inherits IBROSEntity
    Function CreateItem(initializer As Action(Of IItem)) As IItem
    ReadOnly Property EquipSlotId As Guid
    ReadOnly Property Item As IItem
End Interface
