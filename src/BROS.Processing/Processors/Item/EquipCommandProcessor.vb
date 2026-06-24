Imports BROS.Persistence

Friend Module EquipCommandProcessor
    Friend Function Process(world As IWorld, tokens As Queue(Of String)) As CommandProcessorResult
        If tokens.Count <> 3 Then
            Return CommandProcessorResult.Invalid
        End If
        Dim itemNoun = tokens.Dequeue
        Dim character = world.Avatar
        If character.IsDead Then
            character.AddMessage("Dead people don't equip things, but other people can dress you in silly clothing, put you in a boat, douse you with flammable liquids, etc.")
            Return CommandProcessorResult.Processed
        End If
        Dim item = character.Inventory.FindItemByNoun(itemNoun)
        If item Is Nothing Then
            character.AddMessage($"{character.GetName} is not carrying anything called `{itemNoun}`.")
            Return CommandProcessorResult.Processed
        End If
        Dim preposition = tokens.Dequeue
        Dim equipSlotNoun = tokens.Single
        Dim equipSlot = character.FindEquipSlotByNoun(equipSlotNoun)
        If equipSlot Is Nothing Then
            character.AddMessage($"{character.GetName} does not have a `{equipSlotNoun}`.")
            Return CommandProcessorResult.Processed
        End If
        If Not equipSlot.HasPreposition(preposition) Then
            Return CommandProcessorResult.Invalid
        End If
        If Not equipSlot.CanEquip(item) Then
            character.AddMessage($"{character.GetName} cannot equip {item.GetName} {equipSlot.DisplayPreposition} {equipSlot.GetName}.")
            Return CommandProcessorResult.Processed
        End If
        item.EquipSlot = equipSlot
        character.AddMessage($"{character.GetName} equips {item.GetName} {equipSlot.DisplayPreposition} {equipSlot.GetName}.")
        Return CommandProcessorResult.Processed
    End Function
End Module
