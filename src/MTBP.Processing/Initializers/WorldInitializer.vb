Imports System.Runtime.CompilerServices
Imports MTBP.Persistence

Friend Module WorldInitializer
    <Extension>
    Sub Initialize(
                  world As IWorld,
                  context As IInitializationContext)
        world.CreateLocation(RectoryInitializer.Initialize(context))
        world.CreateLocation(ChurchYardInitializer.Initialize(context))
        world.CreateLocation(ChurchInitializer.Initialize(context))
        world.Avatar.AddMessage("So it begins!")
    End Sub

End Module
