Imports TGGD.Provision

Public Class WorldData
    Inherits EntityData
    Public Property AvatarId As Guid?
    Public Property Characters As New Dictionary(Of Guid, CharacterData)
    Public Property Locations As New Dictionary(Of Guid, LocationData)
    Public Property Messages As New List(Of MessageData)
    Public Property Features As New Dictionary(Of Guid, FeatureData)
    Public Property Inventories As New Dictionary(Of Guid, InventoryData)
    Public Property Items As New Dictionary(Of Guid, ItemData)
    Public Property EquipSlots As New Dictionary(Of Guid, EquipSlotData)
    Public Property Routes As New Dictionary(Of Guid, RouteData)
    Public Property Locks As New Dictionary(Of Guid, LockData)
    Public Property Dialogs As New Dictionary(Of Guid, DialogData)
    Public Property Topics As New Dictionary(Of Guid, TopicData)
End Class
