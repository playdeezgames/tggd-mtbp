Public Interface IExitsModel
    Sub LegacyMove(direction As String)
    ReadOnly Property LegacyCanMove As Boolean
    ReadOnly Property LegacyExits As IEnumerable(Of IExitModel)
End Interface
