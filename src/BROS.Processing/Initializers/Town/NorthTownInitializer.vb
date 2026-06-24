Imports BROS.Persistence

Friend Module NorthTownInitializer
    Friend Function Initialize(context As IInitializationContext) As Action(Of ILocation)
        Return Sub(location)
                   location.SetName("North End")
                   location.SetDescription("This is the north end of town.")
                   context.NorthTownLocation = location
                   location.CreateRoute(Directions.SOUTH, context.TownCenterLocation, AddressOf TownCenterRoute)
                   context.TownCenterLocation.CreateRoute(Directions.NORTH, location, AddressOf TownCenterRoute)
               End Sub
    End Function

    Private Sub TownCenterRoute(route As IRoute)
        route.SetName("road")
        route.SetDescription("This is a road between the north end of town and the town center.")
    End Sub
End Module
