Public Delegate Sub LocationInitializer(location As ILocation)
Public Interface ILocation
    Inherits IInventoriedEntity
    ReadOnly Property LocationId As Guid
    Function CreateCharacter(Optional initializer As CharacterInitializer = Nothing) As ICharacter
    Function CreateRoute(direction As String, destination As ILocation, Optional initializer As RouteInitializer = Nothing) As IRoute
    ReadOnly Property Routes As IReadOnlyDictionary(Of String, IRoute)
    Function CreateFeature(Optional initializer As FeatureInitializer = Nothing) As IFeature
    ReadOnly Property Features As IEnumerable(Of IFeature)
End Interface
