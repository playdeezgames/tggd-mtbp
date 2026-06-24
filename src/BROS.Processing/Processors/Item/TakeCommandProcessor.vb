Imports BROS.Persistence

Friend Module TakeCommandProcessor
    Friend Function Process(world As IWorld, tokens As Queue(Of String)) As CommandProcessorResult
        If tokens.Count <> 3 Then
            Return CommandProcessorResult.Invalid
        End If
        Dim noun = tokens.Dequeue
        Dim preposition = tokens.Dequeue
        If Not preposition.Equals(Prepositions.FROM, StringComparison.InvariantCultureIgnoreCase) Then
            Return CommandProcessorResult.Invalid
        End If
        Dim containerNoun = tokens.Single
        Dim character = world.Avatar
        If character.IsDead Then
            character.AddMessage("Dead people don't take things. They also don't leave things. They already left everything.")
            Return CommandProcessorResult.Processed
        End If
        Dim feature = character.Location.FindFeatureByNoun(containerNoun)
        If feature IsNot Nothing Then
            Return TakeFromFeature(noun, character, feature)
        End If
        Dim equipSlot = character.FindEquipSlotByNoun(containerNoun)
        If equipSlot IsNot Nothing Then
            Return TakeFromEquipSlot(noun, character, equipSlot)
        End If
        character.AddMessage($"{character.GetName} sees no {containerNoun} here.")
        Return CommandProcessorResult.Processed
    End Function

    Private Function TakeFromEquipSlot(noun As String, character As ICharacter, equipSlot As IEquipSlot) As CommandProcessorResult
        Dim item = equipSlot.Item
        Return TakeItem(noun, character, item)
    End Function

    Private Function TakeItem(noun As String, character As ICharacter, ByRef item As IItem) As CommandProcessorResult
        If item Is Nothing OrElse Not item.HasNoun(noun) Then
            character.AddMessage($"{character.GetName} sees no {noun} here.")
            Return CommandProcessorResult.Processed
        End If
        If Not item.IsTakeable() Then
            character.AddMessage($"{character.GetName} cannot take {item.GetName()}.")
            Return CommandProcessorResult.Processed
        End If
        character.AddMessage($"{character.GetName} takes {item.GetName}.")
        item.Inventory = character.Inventory
        Return CommandProcessorResult.Processed
    End Function

    Private Function TakeFromFeature(noun As String, character As ICharacter, feature As IFeature) As CommandProcessorResult
        Dim item = feature.Inventory.FindItemByNoun(noun)
        Return TakeItem(noun, character, item)
    End Function
End Module
