Public Class InventoryData
    Public Property Prepositions As New HashSet(Of String)(StringComparer.InvariantCultureIgnoreCase)
    Public Property ItemIds As New HashSet(Of Guid)
End Class
