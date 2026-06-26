Imports MTBP.Provision

Friend Class Location
    Inherits MTBPEntity(Of LocationData)
    Implements ILocation

    Private Sub New(world As IWorld, data As WorldData, locationId As Guid)
        MyBase.New(world, data)
        Me.LocationId = locationId
    End Sub

    Public ReadOnly Property LocationId As Guid Implements ILocation.LocationId

    Public ReadOnly Property Routes As IReadOnlyDictionary(Of String, IRoute) Implements ILocation.Routes
        Get
            Return Data.RouteIds.ToDictionary(Function(x) x.Key, Function(x) Route.Create(World, _data, x.Value))
        End Get
    End Property

    Protected Overrides ReadOnly Property Data As LocationData
        Get
            Return _data.Locations(LocationId)
        End Get
    End Property

    Friend Shared Function Create(world As IWorld, data As WorldData, locationId As Guid) As ILocation
        Return New Location(world, data, locationId)
    End Function

    Public Function CreateCharacter(Optional initializer As CharacterInitializer = Nothing) As ICharacter Implements ILocation.CreateCharacter
        Dim characterId = Guid.NewGuid
        _data.Characters(characterId) = New CharacterData With
            {
                .LocationId = LocationId
            }
        Data.CharacterIds.Add(characterId)
        Dim result As ICharacter = Character.Create(World, _data, characterId)
        initializer?.Invoke(result)
        Return result
    End Function

    Public Function CreateRoute(direction As String, destination As ILocation, Optional initializer As RouteInitializer = Nothing) As IRoute Implements ILocation.CreateRoute
        Dim routeId = Guid.NewGuid
        _data.Routes(routeId) = New RouteData With
            {
                .DestinationLocationId = destination.LocationId
            }
        Data.RouteIds(direction) = routeId
        Dim result As IRoute = Route.Create(World, _data, routeId)
        initializer?.Invoke(result)
        Return result
    End Function
End Class
