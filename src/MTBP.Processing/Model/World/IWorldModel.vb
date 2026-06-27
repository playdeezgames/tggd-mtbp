Imports TGGD.Processing

Public Interface IWorldModel
    Inherits IModel
    ReadOnly Property IsQuittable As Boolean
    Sub Embark()
    Sub Abandon()
    Sub Move(direction As String)
    ReadOnly Property HasGroundItems As Boolean
    ReadOnly Property CanMove As Boolean
    ReadOnly Property Exits As IEnumerable(Of IExitModel)
    ReadOnly Property Messages As IEnumerable(Of String)
    ReadOnly Property GroundItems As IEnumerable(Of IGroundItemModel)
    ReadOnly Property HasItems As Boolean
    ReadOnly Property InventoryItems As IEnumerable(Of IInventoryItemModel)
End Interface
