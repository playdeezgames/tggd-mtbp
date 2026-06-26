Public Delegate Sub RouteInitializer(route As IRoute)
Public Interface IRoute
    Inherits IMTBPEntity
    ReadOnly Property RouteId As Guid
    ReadOnly Property Destination As ILocation
End Interface
