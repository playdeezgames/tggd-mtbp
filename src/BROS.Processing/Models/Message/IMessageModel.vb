Imports TGGD.Processing

Public Interface IMessageModel
    Inherits IModel
    ReadOnly Property Text As String
    ReadOnly Property Mood As String
    ReadOnly Property NewLine As Boolean
End Interface
