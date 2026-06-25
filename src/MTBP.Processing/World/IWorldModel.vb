Imports TGGD.Processing

Public Interface IWorldModel
    Inherits IModel
    ReadOnly Property IsQuittable As Boolean
    Sub Embark()
    Sub Abandon()
    ReadOnly Property Messages As IEnumerable(Of String)
End Interface
