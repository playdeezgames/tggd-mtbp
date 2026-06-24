Public Class LocationData
    Inherits BROSEntityData
    Public Property CharacterIds As New HashSet(Of Guid)
    Public Property FeatureIds As New HashSet(Of Guid)
    Public Property RouteIds As New Dictionary(Of String, Guid)(StringComparer.InvariantCultureIgnoreCase)
End Class
