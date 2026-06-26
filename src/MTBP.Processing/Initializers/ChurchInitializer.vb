Imports MTBP.Persistence

Friend Module ChurchInitializer
    Friend Function Initialize(context As IInitializationContext) As LocationInitializer
        Return Sub(location As ILocation)
                   location.SetName("Church")
                   context.Church = location
                   location.CreateRoute(Directions.EAST, context.ChurchYard, AddressOf InitializeChurchExit)
                   context.ChurchYard.CreateRoute(Directions.WEST, location, AddressOf InitializeChurchEntrance)
               End Sub
    End Function

    Private Sub InitializeChurchEntrance(route As IRoute)
        route.SetName("Church Entrance")
    End Sub

    Private Sub InitializeChurchExit(route As IRoute)
        route.SetName("Church Exit")
    End Sub
End Module
