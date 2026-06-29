Imports TGGD.Processing

Public Interface IGroundModel
    Inherits IModel
    ReadOnly Property HasItems As Boolean
    ReadOnly Property Items As IEnumerable(Of IItemModel)
End Interface
