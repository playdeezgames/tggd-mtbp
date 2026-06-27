Imports System.Runtime.CompilerServices
Imports MTBP.Persistence

Friend Module CharacterExtensions
    <Extension>
    Friend Function IsAvatar(character As ICharacter) As Boolean
        Return character IsNot Nothing AndAlso
            character.World.Avatar IsNot Nothing AndAlso
            character.CharacterId = character.World.Avatar.CharacterId
    End Function
    <Extension>
    Friend Sub AddMessage(character As ICharacter, message As String)
        If character.IsAvatar Then
            character.World.AddMessage(message)
        End If
    End Sub
    <Extension>
    Friend Function IsDead(character As ICharacter) As Boolean
        Return character.GetHealth() = character.GetCounterMinimum(Counters.HEALTH)
    End Function
End Module
