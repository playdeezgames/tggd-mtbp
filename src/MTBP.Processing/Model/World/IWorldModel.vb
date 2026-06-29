Imports TGGD.Processing

Public Interface IWorldModel
    Inherits IModel
    ReadOnly Property IsQuittable As Boolean
    Sub Embark()
    Sub Abandon()
    ReadOnly Property Messages As IEnumerable(Of String)
    ReadOnly Property Views As IViewsModel
    ReadOnly Property Exits As IExitsModel
    ReadOnly Property Ground As IGroundModel
    ReadOnly Property Inventory As IInventoryModel
    ReadOnly Property Features As IFeaturesModel
    'TODO: ground item things!
    ReadOnly Property LegacyHasGroundItems As Boolean
    ReadOnly Property LegacyGroundItems As IEnumerable(Of IItemModel)
    'TODO: inventory things!
    ReadOnly Property LegacyHasItems As Boolean
    ReadOnly Property LegacyInventoryItems As IEnumerable(Of IItemModel)
End Interface
