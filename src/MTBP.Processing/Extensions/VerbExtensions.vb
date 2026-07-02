Imports System.Runtime.CompilerServices
Imports MTBP.Persistence

Friend Module VerbExtensions
    <Extension>
    Friend Function GetVerbType(verb As IVerb) As String
        Return verb.GetMetadata(Metadatas.VERB_TYPE)
    End Function
    <Extension>
    Friend Sub SetVerbType(verb As IVerb, verbType As String)
        verb.SetMetadata(Metadatas.VERB_TYPE, verbType)
    End Sub
End Module
