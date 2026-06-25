Imports TGGD.Provision

Public Class WorldData
    Inherits EntityData
    Public Property Locations As New Dictionary(Of Guid, LocationData)
    Public Property Characters As New Dictionary(Of Guid, CharacterData)
    Public Property AvatarId As Guid?
End Class
