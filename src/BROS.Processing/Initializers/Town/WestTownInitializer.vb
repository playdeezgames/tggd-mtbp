Imports BROS.Persistence

Friend Module WestTownInitializer
    Friend Function Initialize(context As IInitializationContext) As Action(Of ILocation)
        Return Sub(location)
                   location.SetName("West End")
                   location.SetDescription("This is the west end of town.")
                   context.WestTownLocation = location
                   location.CreateRoute(Directions.EAST, context.TownCenterLocation, AddressOf TownCenterRoute)
                   context.TownCenterLocation.CreateRoute(Directions.WEST, location, AddressOf TownCenterRoute)
               End Sub
    End Function

    Private Sub TownCenterRoute(route As IRoute)
        route.SetName("road")
        route.SetDescription("This is a road between the west end of town and the town center.")
    End Sub
End Module
