Imports MTBP.Persistence

Friend Module AbandonedHouseInitializer
    Friend Function Initialize(context As IInitializationContext) As LocationInitializer
        Return Sub(location As ILocation)
                   location.SetName("Abandoned House")
                   location.SetDescription("Nobody lives here. That's what abandoned means. Or at least, nobody is supposed to live here. There might be somebody here, but they ain't the owner.")
                   context.AbandonedHouse = location
                   location.CreateRoute(Directions.WEST, context.ChurchYard, AddressOf InitializeHouseExit)
                   context.ChurchYard.CreateRoute(Directions.EAST, location, AddressOf InitializeHouseEntrance)
                   location.Inventory.CreateItem(AddressOf InitializeDestroyedPrinter)
               End Sub
    End Function

    Private Sub InitializeDestroyedPrinter(item As IItem)
        item.SetName("Destroyed Printer")
        item.SetDescription("This printer has been hit numerous times by a medium length blunt object such that there is no way that the item can function.")
    End Sub

    Private Sub InitializeHouseEntrance(route As IRoute)
        route.SetName("Abandoned House Entrance")
        route.SetDescription("Through here, you may enter the abandoned house.")
    End Sub

    Private Sub InitializeHouseExit(route As IRoute)
        route.SetName("Abandoned House Exit")
        route.SetDescription("Through here, you may exit the abandoned house.")
    End Sub
End Module
