Imports BROS.Persistence

Friend Module GoCommandProcessor
    Friend Function Process(world As IWorld, tokens As Queue(Of String)) As CommandProcessorResult
        world.ClearMessages()
        If tokens.Count <> 1 Then
            Return CommandProcessorResult.Invalid
        End If
        Dim character = world.Avatar
        If character.IsDead Then
            character.AddMessage("Dead people don't go anywhere. They are already gone.")
            Return CommandProcessorResult.Processed
        End If
        Dim location = character.Location
        Dim direction = tokens.First
        Dim route = location.FindRouteByDirection(direction)
        If route Is Nothing Then
            character.AddMessage($"{character.GetName} cannot go {direction} from here.")
            Return CommandProcessorResult.Processed
        End If
        Return ProcessRoute(character, direction, route)
    End Function

    Private Function ProcessRoute(character As ICharacter, direction As String, route As IRoute) As CommandProcessorResult
        Dim keyItem = route.Lock?.Key
        If keyItem IsNot Nothing Then
            character.AddMessage($"{character.GetName} cannot go {direction.ToLower} through {route.GetName}, because it is locked by {keyItem.GetName}.")
            Return CommandProcessorResult.Processed
        End If
        character.AddMessage($"{character.GetName} goes {direction.ToLower} through {route.GetName}.")
        character.Location = route.DestinationLocation
        character.DescribeLocation()
        Return CommandProcessorResult.Processed
    End Function
End Module
