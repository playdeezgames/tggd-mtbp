Imports BROS.Persistence

Friend Module MenuCommandProcessor
    Friend Function Process(world As IWorld, tokens As Queue(Of String)) As CommandProcessorResult
        If tokens.Count <> 0 Then
            Return CommandProcessorResult.Invalid
        End If
        Return CommandProcessorResult.MenuRequest
    End Function
End Module
