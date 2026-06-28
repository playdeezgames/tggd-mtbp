Imports TGGD.Provision

Public Class WorldData
    Inherits EntityData
    Public Property Locations As New Dictionary(Of Guid, LocationData)
    Public Property Characters As New Dictionary(Of Guid, CharacterData)
    Public Property AvatarId As Guid?
    Public Property Messages As New List(Of String)
    Public Property Routes As New Dictionary(Of Guid, RouteData)
    Public Property Inventories As New Dictionary(Of Guid, InventoryData)
    Public Property Items As New Dictionary(Of Guid, ItemData)
    Public Property Features As New Dictionary(Of Guid, FeatureData)
End Class
