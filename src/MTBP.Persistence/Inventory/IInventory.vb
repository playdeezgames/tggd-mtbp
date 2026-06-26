Public Interface IInventory
    ReadOnly Property HasItems As Boolean
    ReadOnly Property World As IWorld
    ReadOnly Property InventoryId As Guid
    Function CreateItem(Optional initializer As ItemInitializer = Nothing) As IItem
End Interface
