Public Class CharacterData
    Inherits InventoryEntityData
    Public Property LocationId As Guid
    Public Property EquipSlotIds As New HashSet(Of Guid)
    Public Property DialogIds As New List(Of Guid)
    Public Property TopicIds As New Dictionary(Of String, Guid)(StringComparer.InvariantCultureIgnoreCase)
End Class
