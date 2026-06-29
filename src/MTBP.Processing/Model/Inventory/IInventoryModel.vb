Imports TGGD.Processing

Public Interface IInventoryModel
    Inherits IModel
    ReadOnly Property LegacyHasItems As Boolean
    ReadOnly Property LegacyInventoryItems As IEnumerable(Of IItemModel)
End Interface
