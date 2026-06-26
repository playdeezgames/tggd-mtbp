Imports MTBP.Persistence

Friend Module ChurchYardInitializer

    Friend Function Initialize(context As IInitializationContext) As LocationInitializer
        Return Sub(location)
                   location.SetName("Church Yard")
                   context.ChurchYard = location
                   location.CreateRoute(Directions.SOUTH, context.Rectory, InitializeRectoryEntrance(context))
                   context.Rectory.CreateRoute(Directions.NORTH, location, InitializeRectoryExit(context))
               End Sub
    End Function

    Private Function InitializeRectoryExit(context As IInitializationContext) As RouteInitializer
        Return Sub(route)
                   route.SetName("Rectory Exit")
               End Sub
    End Function

    Private Function InitializeRectoryEntrance(context As IInitializationContext) As RouteInitializer
        Return Sub(route)
                   route.SetName("Rectory Entrance")
               End Sub
    End Function

End Module
