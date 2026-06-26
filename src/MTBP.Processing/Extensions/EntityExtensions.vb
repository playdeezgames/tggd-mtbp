Imports System.Runtime.CompilerServices
Imports MTBP.Persistence

Friend Module EntityExtensions
    <Extension>
    Friend Sub SetName(entity As IMTBPEntity, name As String)
        entity.SetMetadata(Metadatas.NAME, name)
    End Sub
    <Extension>
    Friend Function GetName(entity As IMTBPEntity) As String
        Return entity.GetMetadata(Metadatas.NAME)
    End Function
    <Extension>
    Friend Function GetToxicity(entity As IMTBPEntity) As Integer
        Return If(entity.TryGetCounter(Counters.TOXICITY), 0)
    End Function
End Module
