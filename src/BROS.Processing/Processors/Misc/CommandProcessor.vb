Imports BROS.Persistence

Friend Module CommandProcessor
    Friend Function Process(world As IWorld, tokens As Queue(Of String)) As CommandProcessorResult
        Return Processors.GetProcessor(tokens.Dequeue).Invoke(world, tokens)
    End Function
End Module
