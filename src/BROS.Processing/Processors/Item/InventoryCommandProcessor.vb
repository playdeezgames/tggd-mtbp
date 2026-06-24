Imports BROS.Persistence

Friend Module InventoryCommandProcessor
    Friend Function Process(world As IWorld, tokens As Queue(Of String)) As CommandProcessorResult
        If tokens.Count <> 0 Then
            Return CommandProcessorResult.Invalid
        End If
        Dim character = world.Avatar
        If character.IsDead Then
            character.AddMessage("Dead people don't have inventory. They have `personal effects`.")
            Return CommandProcessorResult.Processed
        End If
        Dim items = character.Inventory.Items
        If Not items.Any Then
            character.AddMessage($"{character.GetName} is carrying no items.")
            Return CommandProcessorResult.Processed
        End If
        character.AddMessage($"{character.GetName} is carrying {String.Join(", ", items.Select(Function(x) x.GetName))}.")
        Return CommandProcessorResult.Processed
    End Function
End Module
