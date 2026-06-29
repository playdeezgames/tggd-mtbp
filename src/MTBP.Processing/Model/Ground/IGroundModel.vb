Imports TGGD.Processing

Public Interface IGroundModel
    Inherits IModel
    ReadOnly Property LegacyHasGroundItems As Boolean
    ReadOnly Property LegacyGroundItems As IEnumerable(Of IItemModel)
End Interface
