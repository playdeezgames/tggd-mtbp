Imports MTBP.Persistence

Friend Module AbandonedHouseInitializer
    Friend Function Initialize(context As IInitializationContext) As LocationInitializer
        Return Sub(location As ILocation)
                   location.SetName("Abandoned House")
                   context.AbandonedHouse = location
                   location.CreateRoute(Directions.WEST, context.ChurchYard, AddressOf InitializeHouseExit)
                   context.ChurchYard.CreateRoute(Directions.EAST, location, AddressOf InitializeHouseEntrance)
                   'TODO: destroyed printer
               End Sub
    End Function

    Private Sub InitializeHouseEntrance(route As IRoute)
        route.SetName("Abandoned House Entrance")
    End Sub

    Private Sub InitializeHouseExit(route As IRoute)
        route.SetName("Abandoned House Exit")
    End Sub
End Module
