Public Interface IInventoryItemModel
    ReadOnly Property Text As String
    Sub Drop()
    Sub Place(featureModel As IFeatureModel)
    Sub Take()
End Interface
