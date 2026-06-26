Public Delegate Sub ItemInitializer(item As IItem)
Public Interface IItem
    Inherits IMTBPEntity
    ReadOnly Property ItemId As Guid
    Property Inventory As IInventory
End Interface
