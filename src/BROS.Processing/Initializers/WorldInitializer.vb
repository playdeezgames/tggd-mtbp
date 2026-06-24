Imports BROS.Persistence

Friend Module WorldInitializer
    Friend Sub InitializeWorld(world As IWorld)
        world.Abandon()
        Dim townCenter = world.CreateLocation(TownCenterInitializer.Initialize(InitializationContext.Create(world)))
        Embark(world)
    End Sub


    Private Sub Embark(world As IWorld)
        world.AddMessage("Welcome to The Bluer Room of SPLORR!!")
        world.AddMessage("There is a HELP command. You should use it. Unless yer THAT guy.")
        world.Avatar.DescribeLocation()
    End Sub
End Module
