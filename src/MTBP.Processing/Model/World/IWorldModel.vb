Imports TGGD.Processing

Public Interface IWorldModel
    Inherits IModel
    ReadOnly Property IsQuittable As Boolean
    Sub Embark()
    Sub Abandon()
    ReadOnly Property Messages As IEnumerable(Of String)
    ReadOnly Property Views As IViewsModel
    ReadOnly Property Exits As IExitsModel
    'TODO: ground item things!
    ReadOnly Property HasGroundItems As Boolean
    ReadOnly Property GroundItems As IEnumerable(Of IItemModel)
    'TODO: inventory things!
    ReadOnly Property HasItems As Boolean
    ReadOnly Property InventoryItems As IEnumerable(Of IItemModel)
    'TODO: feature things!
    ReadOnly Property HasFeatures As Boolean
    ReadOnly Property Features As IEnumerable(Of IFeatureModel)
End Interface
