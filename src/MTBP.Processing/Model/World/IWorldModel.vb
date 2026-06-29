Imports TGGD.Processing

Public Interface IWorldModel
    Inherits IModel
    ReadOnly Property IsQuittable As Boolean
    Sub Embark()
    Sub Abandon()
    ReadOnly Property Messages As IEnumerable(Of String)
    'TODO: view selection
    Sub ShowStatus()
    Sub Look()
    ReadOnly Property StatusChoiceVisible As Boolean
    ReadOnly Property LookChoiceVisible As Boolean
    'TODO: exit things!
    Sub Move(direction As String)
    ReadOnly Property CanMove As Boolean
    ReadOnly Property Exits As IEnumerable(Of IExitModel)
    'TODO: ground item things!
    ReadOnly Property HasGroundItems As Boolean
    ReadOnly Property GroundItems As IEnumerable(Of IInventoryItemModel)
    'TODO: inventory things!
    ReadOnly Property HasItems As Boolean
    ReadOnly Property InventoryItems As IEnumerable(Of IInventoryItemModel)
    'TODO: feature things!
    ReadOnly Property HasFeatures As Boolean
    ReadOnly Property Features As IEnumerable(Of IFeatureModel)
End Interface
