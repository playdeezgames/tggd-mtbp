Imports BROS.Persistence

Friend Module LookCommandProcessor
    Friend Function Process(world As IWorld, tokens As Queue(Of String)) As CommandProcessorResult
        Dim character = world.Avatar
        If Character.IsDead Then
            character.AddMessage("Dead people don't look at things, even with their lifeless staring eyes gazing at the infinite.")
            Return CommandProcessorResult.Processed
        End If
        Select Case tokens.Count
            Case 0
                world.ClearMessages()
                character.DescribeLocation()
                Return CommandProcessorResult.Processed
            Case Else
                Return CommandProcessorResult.Invalid
        End Select
    End Function
End Module
