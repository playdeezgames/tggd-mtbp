Imports TGGD.Processing

Public Interface IWorldModel
    Inherits IModel
    ReadOnly Property IsInPlay As Boolean
    ReadOnly Property IsQuittable As Boolean
    ReadOnly Property IsMenuRequested As Boolean
    Sub Embark()
    Sub ProcessCommand(command As String)
    ReadOnly Property Messages As IEnumerable(Of IMessageModel)
End Interface
