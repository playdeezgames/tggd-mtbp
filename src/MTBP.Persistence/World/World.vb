Imports System.Text.Json
Imports MTBP.Provision
Imports TGGD.Persistence

Public Class World
    Inherits Entity(Of WorldData)
    Implements IWorld

    Private Sub New(data As WorldData, persister As IPersister)
        Me.Data = data
        Me.persister = persister
    End Sub

    Protected Overrides ReadOnly Property Data As WorldData

    Public Property Avatar As ICharacter Implements IWorld.Avatar
        Get
            Return Character.Create(Me, Data, Data.AvatarId)
        End Get
        Set(value As ICharacter)
            Data.AvatarId = value?.CharacterId
        End Set
    End Property

    Public ReadOnly Property Messages As IEnumerable(Of String) Implements IWorld.Messages
        Get
            Return Data.Messages
        End Get
    End Property

    Private ReadOnly persister As IPersister

    Public Async Function Save(filename As String) As Task Implements IWorld.Save
        Await persister.SaveAsync(filename, JsonSerializer.Serialize(Data))
    End Function

    Public Shared Function Create(data As WorldData, persister As IPersister) As IWorld
        Return New World(data, persister)
    End Function

    Public Shared Async Function Load(filename As String, persister As IPersister) As Task(Of IWorld)
        Return New World(JsonSerializer.Deserialize(Of WorldData)(Await persister.LoadAsync(filename)), persister)
    End Function

    Public Overrides Sub Clear()
        MyBase.Clear()
        ClearMessages()
        Data.Characters.Clear()
        Data.Locations.Clear()
        Data.AvatarId = Nothing
    End Sub

    Public Function CreateLocation(Optional initializer As Action(Of ILocation) = Nothing) As ILocation Implements IWorld.CreateLocation
        Dim locationId = Guid.NewGuid
        Data.Locations(locationId) = New LocationData
        Dim result = Location.Create(Me, Data, locationId)
        initializer?.Invoke(result)
        Return result
    End Function

    Public Sub AddMessage(message As String) Implements IWorld.AddMessage
        Data.Messages.Add(message)
    End Sub

    Public Sub ClearMessages() Implements IWorld.ClearMessages
        Data.Messages.Clear()
    End Sub
End Class
