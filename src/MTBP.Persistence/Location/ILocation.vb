Public Interface ILocation
    Inherits IMTBPEntity
    ReadOnly Property LocationId As Guid
    Function CreateCharacter(Optional initializer As Action(Of ICharacter) = Nothing) As ICharacter
    Function CreateRoute(direction As String, destination As ILocation, Optional initializer As Action(Of IRoute) = Nothing) As IRoute
    ReadOnly Property Routes As IReadOnlyDictionary(Of String, IRoute)
End Interface
