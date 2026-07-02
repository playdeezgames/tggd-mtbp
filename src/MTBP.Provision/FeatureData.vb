Public Class FeatureData
    Inherits InventoriedEntityData
    Public Property LocationId As Guid
    Public Property VerbIds As New HashSet(Of Guid)
End Class
