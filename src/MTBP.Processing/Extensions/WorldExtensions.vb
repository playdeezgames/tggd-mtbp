Imports System.Runtime.CompilerServices
Imports MTBP.Persistence

Friend Module WorldExtensions
    <Extension>
    Sub Describe(world As IWorld)
        Dim character = world.Avatar
        Dim location = character.Location
        world.AddMessage($"{character.GetName} is in {location.GetName}.")
        world.AddMessage($"Toxicity Level: {location.GetToxicity()}")
        Dim routes = location.Routes
        If routes.Any Then
            world.AddMessage("Exits:")
            For Each route In routes
                world.AddMessage($"- {route.Key}: {route.Value.GetName}")
            Next
        End If
        If location.Inventory.HasItems Then
            world.AddMessage("There are items on the ground.")
        End If
    End Sub
End Module
