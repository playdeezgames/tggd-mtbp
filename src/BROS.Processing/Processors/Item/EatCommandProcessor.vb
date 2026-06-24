Imports BROS.Persistence

Friend Module EatCommandProcessor
    Friend Function Process(world As IWorld, tokens As Queue(Of String)) As CommandProcessorResult
        If tokens.Count <> 1 Then
            Return CommandProcessorResult.Invalid
        End If
        Dim itemNoun = tokens.Dequeue
        Dim character = world.Avatar
        If character.IsDead Then
            character.AddMessage("Dead people don't eat things. Likely you died of eating something.")
            Return CommandProcessorResult.Processed
        End If
        Dim item = character.Inventory.FindItemByNoun(itemNoun)
        If item Is Nothing Then
            character.AddMessage($"{character.GetName} has no `{itemNoun}`.")
            Return CommandProcessorResult.Processed
        End If
        If Not item.IsEdible() Then
            character.AddMessage($"{item.GetName} is not edible.")
            Return CommandProcessorResult.Processed
        End If
        character.AddMessage($"{character.GetName} eats {item.GetName}.")
        character.Eat(item)
        item.Destroy()
        Return CommandProcessorResult.Processed
    End Function
End Module
