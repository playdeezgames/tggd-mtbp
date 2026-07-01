Imports System.Runtime.CompilerServices
Imports MTBP.Persistence

Friend Module WorldExtensions
    <Extension>
    Friend Function GetGodName(world As IWorld) As String
        Return world.GetMetadata(Metadatas.GOD_NAME)
    End Function
    <Extension>
    Friend Sub SetGodName(world As IWorld, godName As String)
        world.SetMetadata(Metadatas.GOD_NAME, godName)
    End Sub
End Module
