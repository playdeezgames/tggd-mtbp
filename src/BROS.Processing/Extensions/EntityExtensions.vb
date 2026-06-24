Imports System.Runtime.CompilerServices
Imports TGGD.Persistence

Friend Module EntityExtensions
    <Extension>
    Friend Sub SetName(entity As IEntity, name As String)
        entity.SetMetadata(Metadatas.NAME, name)
    End Sub
    <Extension>
    Friend Function GetName(entity As IEntity) As String
        Return entity.GetMetadata(Metadatas.NAME)
    End Function
    <Extension>
    Friend Function GetDescription(entity As IEntity) As String
        Return entity.GetMetadata(Metadatas.DESCRIPTION)
    End Function
    <Extension>
    Friend Sub SetDescription(entity As IEntity, description As String)
        entity.SetMetadata(Metadatas.DESCRIPTION, description)
    End Sub
End Module
