Public Interface IFeatureModel
    ReadOnly Property Text As String
    Sub Describe()
    ReadOnly Property HasItems As Boolean
    ReadOnly Property InventoryItems As IEnumerable(Of IInventoryItemModel)
End Interface
