Imports BROS.Persistence

Friend Module UnlockCommandProcessor
    Friend Function Process(world As IWorld, tokens As Queue(Of String)) As CommandProcessorResult
        If tokens.Count <> 3 Then
            Return CommandProcessorResult.Invalid
        End If
        Dim direction = tokens.Dequeue
        Dim character = world.Avatar
        If character.IsDead Then
            character.AddMessage("Dead people don't unlock things. They've unlocked the final mystery, and would have been disappointed.")
            Return CommandProcessorResult.Processed
        End If
        Dim route As IRoute = character.Location.FindRouteByDirection(direction)
        If route Is Nothing Then
            character.AddMessage($"{character.GetName()} sees nothing going {direction}.")
            Return CommandProcessorResult.Processed
        End If
        Dim preposition = tokens.Dequeue
        If Not preposition.Equals(Prepositions.WITH, StringComparison.InvariantCultureIgnoreCase) Then
            Return CommandProcessorResult.Invalid
        End If
        Dim noun = tokens.Single
        Dim item = character.Inventory.FindItemByNoun(noun)
        If item Is Nothing Then
            character.AddMessage($"{character.GetName()} is not carrying {noun}.")
            Return CommandProcessorResult.Processed
        End If
        Dim lock = route.Lock
        If lock Is Nothing Then
            character.AddMessage($"It is not locked.")
            Return CommandProcessorResult.Processed
        End If
        If lock.Key.ItemId <> item.ItemId Then
            character.AddMessage($"That won't unlock it.")
            Return CommandProcessorResult.Processed
        End If
        character.AddMessage($"{character.GetName} unlocks {lock.GetName()} with {item.GetName()}.")
        route.DestroyLock()
        Return CommandProcessorResult.Processed
    End Function
End Module
