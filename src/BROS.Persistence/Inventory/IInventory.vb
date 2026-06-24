Public Interface IInventory
    Inherits IBaseInventory
    ReadOnly Property InventoryId As Guid
    Function CreateItem(Optional initializer As Action(Of IItem) = Nothing) As IItem
    ReadOnly Property Items As IEnumerable(Of IItem)
    Function FindItemByNoun(noun As String) As IItem
End Interface
