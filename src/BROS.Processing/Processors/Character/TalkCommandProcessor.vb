Imports BROS.Persistence

Friend Module TalkCommandProcessor
    Friend Function Process(world As IWorld, tokens As Queue(Of String)) As CommandProcessorResult
        If tokens.Count <> 2 Then
            Return CommandProcessorResult.Invalid
        End If
        Dim preposition = tokens.Dequeue
        If Not preposition.Equals(Prepositions.TO, StringComparison.InvariantCultureIgnoreCase) Then
            Return CommandProcessorResult.Invalid
        End If
        Dim noun = tokens.Single
        Dim character = world.Avatar
        If character.IsDead Then
            character.AddMessage("Dead people don't talk. And here I thought you'd never shut up!")
            Return CommandProcessorResult.Processed
        End If
        Dim location = character.Location
        Dim target = location.FindCharacterByNoun(noun)
        If target Is Nothing Then
            character.AddMessage($"{character.GetName()} sees no one called `{noun}` here.")
            Return CommandProcessorResult.Processed
        End If
        Dim dialog = target.CurrentDialog
        If dialog IsNot Nothing Then
            character.AddMessage(dialog.Message)
            target.AdvanceDialog()
        Else
            character.AddMessage($"{character.GetName} does not respond.")
        End If
        Return CommandProcessorResult.Processed
    End Function
End Module
