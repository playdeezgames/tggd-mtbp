Public Interface IFeatureModel
    ReadOnly Property Text As String
    Sub Describe()
    ReadOnly Property HasItems As Boolean
    ReadOnly Property InventoryItems As IEnumerable(Of IItemModel)
    ReadOnly Property CanInteract As Boolean
    ReadOnly Property Verbs As IEnumerable(Of IVerbModel)
End Interface
