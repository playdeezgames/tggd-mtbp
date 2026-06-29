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
    <Extension>
    Friend Function GetHealth(entity As IMTBPEntity) As Integer
        Return If(entity.TryGetCounter(Counters.HEALTH), 0)
    End Function
    <Extension>
    Friend Function GetMaximumHealth(entity As IMTBPEntity) As Integer
        Return entity.GetCounterMaximum(Counters.HEALTH)
    End Function
    <Extension>
    Friend Function GetImmunity(entity As IMTBPEntity) As Integer
        Return If(entity.TryGetCounter(Counters.IMMUNITY), 0)
    End Function
    <Extension>
    Friend Function GetMaximumImmunity(entity As IMTBPEntity) As Integer
        Return entity.GetCounterMaximum(Counters.IMMUNITY)
    End Function
    <Extension>
    Friend Function GetSatiety(entity As IMTBPEntity) As Integer
        Return entity.GetCounter(Counters.SATIETY)
    End Function
    <Extension>
    Friend Function GetMaximumSatiety(entity As IMTBPEntity) As Integer
        Return entity.GetCounterMaximum(Counters.SATIETY)
    End Function
    <Extension>
    Friend Sub SetView(entity As IMTBPEntity, view As String)
        entity.SetMetadata(Metadatas.VIEW, view)
    End Sub
    <Extension>
    Friend Function GetView(entity As IMTBPEntity) As String
        Return If(entity.TryGetMetadata(Metadatas.VIEW), Grimoire.DEFAULT_VIEW)
    End Function
    <Extension>
    Friend Function GetDescription(entity As IMTBPEntity) As String
        Return entity.GetMetadata(Metadatas.DESCRIPTION)
    End Function
    <Extension>
    Friend Sub SetDescription(entity As IMTBPEntity, description As String)
        entity.SetMetadata(Metadatas.DESCRIPTION, description)
    End Sub
End Module
