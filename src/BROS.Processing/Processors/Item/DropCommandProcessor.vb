Imports BROS.Persistence

Friend Module DropCommandProcessor
    Friend Function Process(world As IWorld, tokens As Queue(Of String)) As CommandProcessorResult
        If tokens.Count <> 1 Then
            Return CommandProcessorResult.Invalid
        End If
        Dim character = world.Avatar
        If character.IsDead Then
            character.AddMessage("Dead people don't drop things. Except for dropping dead. Which you already did. Work complete.")
            Return CommandProcessorResult.Processed
        End If
        Dim noun = tokens.Single
        Dim item = character.Inventory.FindItemByNoun(noun)
        If item Is Nothing Then
            character.AddMessage($"{character.GetName} is not carrying {noun}.")
            Return CommandProcessorResult.Processed
        End If
        Dim feature = character.Location.FindFeatureByNoun(Nouns.FLOOR)
        If feature Is Nothing Then
            feature = character.Location.CreateFeature(AddressOf InitializeFloor)
        End If
        item.Inventory = feature.Inventory
        character.AddMessage($"{character.GetName} drops {item.GetName} {feature.Inventory.DisplayPreposition} {feature.GetName}")
        Return CommandProcessorResult.Processed
    End Function

End Module
