Imports System.Runtime.CompilerServices
Imports MTBP.Persistence

Friend Module WorldInitializer
    <Extension>
    Sub Initialize(
                  world As IWorld,
                  context As IInitializationContext)
        world.CreateLocation(InitializeRectory(context))
        world.CreateLocation(InitializeChurchYard(context))
        world.Avatar.AddMessage("So it begins!")
    End Sub

    Private Function InitializeChurchYard(context As IInitializationContext) As Action(Of ILocation)
        Return Sub(location)
                   location.SetName("Church Yard")
                   context.ChurchYard = location
                   location.CreateRoute(Directions.SOUTH, context.Rectory, InitializeRectoryEntrance(context))
                   context.Rectory.CreateRoute(Directions.NORTH, location, InitializeRectoryExit(context))
               End Sub
    End Function

    Private Function InitializeRectoryExit(context As IInitializationContext) As Action(Of IRoute)
        Return Sub(route)
                   route.SetName("Rectory Exit")
               End Sub
    End Function

    Private Function InitializeRectoryEntrance(context As IInitializationContext) As Action(Of IRoute)
        Return Sub(route)
                   route.SetName("Rectory Entrance")
               End Sub
    End Function

    Private Function InitializeRectory(context As IInitializationContext) As Action(Of ILocation)
        Return Sub(location)
                   location.SetName("Rectory")
                   location.CreateCharacter(InitializeRector(context))
                   context.Rectory = location
               End Sub
    End Function

    Private Function InitializeRector(context As IInitializationContext) As Action(Of ICharacter)
        Return Sub(character)
                   character.SetName("Ölën Kÿrpä")
                   character.World.Avatar = character
               End Sub
    End Function
End Module
