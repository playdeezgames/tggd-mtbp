Imports BROS.Persistence

Friend Module HelpCommandProcessor
    Friend Function Process(world As IWorld, tokens As Queue(Of String)) As CommandProcessorResult
        Select Case tokens.Count
            Case 0
                Return ShowCommands(world)
            Case 1
                Return ShowCommand(world, tokens.Single)
            Case Else
                Return CommandProcessorResult.Invalid
        End Select
    End Function

    Private Function ShowCommand(world As IWorld, command As String) As CommandProcessorResult
        world.ClearMessages()
        world.AddMessage($"Help for `{command}`:")
        For Each helpText In Processors.GetHelpTexts(command)
            world.AddMessage(helpText)
        Next
        Return CommandProcessorResult.Processed
    End Function

    Private Function ShowCommands(world As IWorld) As CommandProcessorResult
        world.ClearMessages()
        world.AddMessage("Available Commands:")
        world.AddMessage(String.Join(", ", Processors.AllCommands))
        world.AddMessage("For help with a specific command, try:")
        world.AddMessage("    HELP [COMMAND]")
        Return CommandProcessorResult.Processed
    End Function
End Module
