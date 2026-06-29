Imports MTBP.Persistence

Friend Module ChurchYardInitializer

    Friend Function Initialize(context As IInitializationContext) As LocationInitializer
        Return Sub(location)
                   location.SetName("Church Yard")
                   location.SetDescription("This is the yard in front of the church. Can your milkshake bring all of the boys?")
                   context.ChurchYard = location
                   location.CreateRoute(Directions.SOUTH, context.Rectory, InitializeRectoryEntrance(context))
                   context.Rectory.CreateRoute(Directions.NORTH, location, InitializeRectoryExit(context))
               End Sub
    End Function

    Private Function InitializeRectoryExit(context As IInitializationContext) As RouteInitializer
        Return Sub(route)
                   route.SetName("Rectory Exit")
                   route.SetDescription("Through here, you can exit the rectory. Which seems fairly normal.")
               End Sub
    End Function

    Private Function InitializeRectoryEntrance(context As IInitializationContext) As RouteInitializer
        Return Sub(route)
                   route.SetName("Rectory Entrance")
                   route.SetDescription("Through here you can enter the rectory. Yes, entering the rectory is fine. Why do you ask?")
               End Sub
    End Function

End Module
