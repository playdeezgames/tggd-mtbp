Imports MTBP.Provision

Friend Class Character
    Inherits InventoriedEntity(Of CharacterData)
    Implements ICharacter

    Private Sub New(world As IWorld, data As WorldData, characterId As Guid)
        MyBase.New(world, data)
        Me.CharacterId = characterId
    End Sub

    Public ReadOnly Property CharacterId As Guid Implements ICharacter.CharacterId

    Public Property Location As ILocation Implements ICharacter.Location
        Get
            Return Persistence.Location.Create(World, _data, Data.LocationId)
        End Get
        Set(value As ILocation)
            _data.Locations(Data.LocationId).CharacterIds.Remove(CharacterId)
            Data.LocationId = value.LocationId
            _data.Locations(Data.LocationId).CharacterIds.Add(CharacterId)
        End Set
    End Property

    Protected Overrides ReadOnly Property Data As CharacterData
        Get
            Return _data.Characters(CharacterId)
        End Get
    End Property

    Friend Shared Function Create(world As IWorld, data As WorldData, characterId As Guid?) As ICharacter
        If Not characterId.HasValue Then
            Return Nothing
        End If
        Return New Character(world, data, characterId.Value)
    End Function
End Class
