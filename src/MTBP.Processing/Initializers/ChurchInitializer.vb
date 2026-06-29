Imports MTBP.Persistence

Friend Module ChurchInitializer
    Friend Function Initialize(context As IInitializationContext) As LocationInitializer
        Return Sub(location As ILocation)
                   location.SetName("Church")
                   location.SetName("This is a building for talking to the invisible sky-man. I ain't yer judge.")
                   context.Church = location
                   location.CreateFeature(AddressOf InitializeAltar)
                   location.CreateRoute(Directions.EAST, context.ChurchYard, AddressOf InitializeChurchExit)
                   context.ChurchYard.CreateRoute(Directions.WEST, location, AddressOf InitializeChurchEntrance)
               End Sub
    End Function

    Private Sub InitializeAltar(feature As IFeature)
        feature.SetName("Altar")
        feature.SetDescription("Upon this you can place victims, and sacrifice them. You know, if yer into that. If not, this is really just a table. A sturdy, sturdy table.")
    End Sub

    Private Sub InitializeChurchEntrance(route As IRoute)
        route.SetName("Church Entrance")
        route.SetDescription("Through here you can enter the church.")
    End Sub

    Private Sub InitializeChurchExit(route As IRoute)
        route.SetName("Church Exit")
        route.SetDescription("Through here, you can exit the church.")
    End Sub
End Module
