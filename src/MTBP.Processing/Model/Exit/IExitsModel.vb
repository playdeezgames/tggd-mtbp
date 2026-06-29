Public Interface IExitsModel
    Sub Move(direction As String)
    ReadOnly Property HasAny As Boolean
    ReadOnly Property All As IEnumerable(Of IExitModel)
End Interface
