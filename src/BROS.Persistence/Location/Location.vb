Imports BROS.Provision

Friend Class Location
    Inherits BROSEntity(Of LocationData)
    Implements ILocation

    Private Sub New(world As IWorld, data As WorldData, locationId As Guid)
        MyBase.New(world, data)
        Me.LocationId = locationId
    End Sub

    Public ReadOnly Property LocationId As Guid Implements ILocation.LocationId

    Protected Overrides ReadOnly Property Data As LocationData
        Get
            Return _data.Locations(LocationId)
        End Get
    End Property

    Public ReadOnly Property Features As IEnumerable(Of IFeature) Implements ILocation.Features
        Get
            Return Data.FeatureIds.Select(Function(x) Feature.Create(World, _data, x))
        End Get
    End Property

    Public ReadOnly Property Characters As IEnumerable(Of ICharacter) Implements ILocation.Characters
        Get
            Return Data.CharacterIds.Select(Function(x) Character.Create(World, _data, x))
        End Get
    End Property

    Public ReadOnly Property Exits As IReadOnlyDictionary(Of String, IRoute) Implements ILocation.Exits
        Get
            Return Data.RouteIds.ToDictionary(Function(x) x.Key, Function(x) Route.Create(World, _data, x.Value))
        End Get
    End Property

    Public Function CreateCharacter(Optional initializer As Action(Of ICharacter) = Nothing) As ICharacter Implements ILocation.CreateCharacter
        Dim characterId = Guid.NewGuid
        _data.Characters(characterId) = New CharacterData With
            {
                .LocationId = LocationId
            }
        Dim result = Character.Create(World, _data, characterId)
        initializer?.Invoke(result)
        AddCharacter(result)
        Return result
    End Function

    Private Sub AddCharacter(character As ICharacter)
        Data.CharacterIds.Add(character.CharacterId)
    End Sub

    Friend Shared Function Create(world As IWorld, data As WorldData, locationId As Guid) As ILocation
        Return New Location(world, data, locationId)
    End Function

    Public Function CreateFeature(Optional initializer As Action(Of IFeature) = Nothing) As IFeature Implements ILocation.CreateFeature
        Dim featureId = Guid.NewGuid
        _data.Features(featureId) = New FeatureData With
            {
                .LocationId = LocationId
            }
        Dim result = Feature.Create(World, _data, featureId)
        initializer?.Invoke(result)
        AddFeature(result)
        Return result
    End Function

    Private Sub AddFeature(feature As IFeature)
        Data.FeatureIds.Add(feature.FeatureId)
    End Sub

    Public Function FindFeatureByNoun(noun As String) As IFeature Implements ILocation.FindFeatureByNoun
        Return Features.FirstOrDefault(Function(x) x.HasNoun(noun))
    End Function

    Public Function CreateRoute(direction As String, destination As ILocation, Optional initializer As Action(Of IRoute) = Nothing) As IRoute Implements ILocation.CreateRoute
        Dim routeId = Guid.NewGuid
        _data.Routes(routeId) = New RouteData With
            {
                .DestinationLocationId = destination.LocationId
            }
        SetRoute(direction, routeId)
        Dim result = Route.Create(World, _data, routeId)
        initializer?.Invoke(result)
        Return result
    End Function

    Private Sub SetRoute(direction As String, routeId As Guid)
        Data.RouteIds(direction) = routeId
    End Sub

    Public Function FindRouteByDirection(direction As String) As IRoute Implements ILocation.FindRouteByDirection
        Dim routeId As Guid
        If Data.RouteIds.TryGetValue(direction, routeId) Then
            Return Route.Create(World, _data, routeId)
        End If
        Return Nothing
    End Function

    Public Function GetOtherCharacters(character As ICharacter) As IEnumerable(Of ICharacter) Implements ILocation.GetOtherCharacters
        Return Characters.Where(Function(x) x.CharacterId <> character.CharacterId)
    End Function

    Public Function FindCharacterByNoun(noun As String) As ICharacter Implements ILocation.FindCharacterByNoun
        Return Characters.FirstOrDefault(Function(x) x.HasNoun(noun))
    End Function
End Class
