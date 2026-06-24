Imports BROS.Persistence

Friend Module GiveCommandProcessor
    Friend Function Process(world As IWorld, tokens As Queue(Of String)) As CommandProcessorResult
        If tokens.Count <> 3 Then
            Return CommandProcessorResult.Invalid
        End If
        Dim itemNoun = tokens.Dequeue
        Dim character = world.Avatar
        If character.IsDead Then
            character.AddMessage("Dead people don't give things. Mostly they just lay there until the scavengers show up.")
            Return CommandProcessorResult.Processed
        End If
        Dim item = character.Inventory.FindItemByNoun(itemNoun)
        If item Is Nothing Then
            character.AddMessage($"{character.GetName} has no item called `{itemNoun}`.")
            Return CommandProcessorResult.Processed
        End If
        Dim preposition = tokens.Dequeue
        If Not preposition.Equals(Prepositions.TO, StringComparison.InvariantCultureIgnoreCase) Then
            Return CommandProcessorResult.Invalid
        End If
        Dim characterNoun = tokens.Dequeue
        Dim target = character.Location.FindCharacterByNoun(characterNoun)
        If target Is Nothing Then
            character.AddMessage($"{character.GetName} sees no one called `{characterNoun}` here.")
            Return CommandProcessorResult.Processed
        End If
        If Not target.CanAccept(item) Then
            character.AddMessage($"{target.GetName} will not take {item.GetName}.")
            Return CommandProcessorResult.Processed
        End If
        item.Inventory = target.Inventory
        character.AddMessage($"{character.GetName} gives {item.GetName} to {target.GetName}.")
        Return CommandProcessorResult.Processed
    End Function
End Module
