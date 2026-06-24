Public Class EquipSlotData
    Inherits BROSEntityData
    Public Property CharacterId As Guid
    Public Property ItemId As Guid?
    Public Property Prepositions As New HashSet(Of String)(StringComparer.InvariantCultureIgnoreCase)
End Class
