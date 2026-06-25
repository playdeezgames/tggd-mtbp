Imports System.Runtime.CompilerServices
Imports MTBP.Persistence

Friend Module WorldInitializer
    <Extension>
    Sub Initialize(
                  world As IWorld,
                  context As IInitializationContext)
        world.CreateLocation(InitializeRectory(context))
        world.Avatar.AddMessage("So it begins!")
    End Sub

    Private Function InitializeRectory(context As IInitializationContext) As Action(Of ILocation)
        Return Sub(location)
                   location.SetName("Rectory")
                   location.CreateCharacter(InitializeRector(context))
               End Sub
    End Function

    Private Function InitializeRector(context As IInitializationContext) As Action(Of ICharacter)
        Return Sub(character)
                   character.SetName("Ölën Kÿrpä")
                   character.World.Avatar = character
               End Sub
    End Function
End Module
