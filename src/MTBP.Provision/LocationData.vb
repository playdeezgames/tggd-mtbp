Public Class LocationData
    Inherits MTBPEntityData
    Public Property CharacterIds As New HashSet(Of Guid)
    Public Property RouteIds As New Dictionary(Of String, Guid)
End Class
