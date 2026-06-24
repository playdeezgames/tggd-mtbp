Imports BROS.Persistence

Friend Module EquipmentCommandProcessor
    Friend Function Process(world As IWorld, tokens As Queue(Of String)) As CommandProcessorResult
        If tokens.Count <> 0 Then
            Return CommandProcessorResult.Invalid
        End If
        Dim character = world.Avatar
        If character.IsDead Then
            character.AddMessage("Dead people don't have equipment. They have loot. For others to take.")
            Return CommandProcessorResult.Processed
        End If
        character.AddMessage($"{character.GetName}'s Equipment:")
        For Each equipSlot In character.EquipSlots
            Dim item = equipSlot.Item
            character.AddMessage($"{equipSlot.GetName}: {If(item IsNot Nothing, item.GetName, "Nothing")}")
        Next
        Return CommandProcessorResult.Processed
    End Function
End Module
