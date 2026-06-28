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
    <Extension>
    Friend Sub HandleToxicity(character As ICharacter)
        Dim toxicity = character.Location.GetToxicity()
        If toxicity <= 0 Then
            Return
        End If
        character.AddMessage($"{character.GetName} reacts to {toxicity} toxicity.")
        Dim immunity = Math.Min(toxicity, character.GetImmunity())
        If immunity > 0 Then
            character.AddMessage($"{character.GetName} loses {immunity} immunity.")
            character.ChangeCounter(Counters.IMMUNITY, -immunity)
        End If
        toxicity -= immunity
        If toxicity > 0 Then
            character.AddMessage($"{character.GetName} loses {toxicity} health.")
            character.ChangeCounter(Counters.HEALTH, -toxicity)
        End If
    End Sub
    <Extension>
    Friend Sub HandleHunger(character As ICharacter)
        Dim hunger = If(character.TryGetCounter(Counters.HUNGER_RATE), 0)
        If hunger = 0 Then
            Return
        End If
        character.AddMessage($"{character.GetName} experiences {hunger} hunger!")
        Dim satiety = Math.Min(hunger, character.GetSatiety())
        If satiety > 0 Then
            character.AddMessage($"{character.GetName} loses {satiety} satiety!")
            character.ChangeCounter(Counters.SATIETY, -satiety)
        End If
        hunger -= satiety
        If hunger > 0 Then
            character.AddMessage($"{character.GetName} loses {hunger} health!")
            character.ChangeCounter(Counters.HEALTH, -hunger)
        End If
    End Sub
    <Extension>
    Friend Sub ShowStatus(character As ICharacter)
        character.SetView(Views.STATUS)
        character.AddMessage($"{character.GetName}'s Status:")
        character.AddMessage($"Health: {character.GetHealth}/{character.GetMaximumHealth}")
        character.AddMessage($"Satiety: {character.GetSatiety}/{character.GetMaximumSatiety}")
        character.AddMessage($"Immunity: {character.GetImmunity}/{character.GetMaximumImmunity}")
    End Sub
    <Extension>
    Sub Look(character As ICharacter)
        character.SetView(Views.LOOK)
        If character.IsDead() Then
            character.AddMessage($"{character.GetName} is dead.")
            Return
        End If
        Dim location = character.Location
        character.AddMessage($"{character.GetName} is in {location.GetName}.")
        character.AddMessage($"Local Toxicity Level: {location.GetToxicity()}")
        Dim routes = location.Routes
        If routes.Any Then
            character.AddMessage("Exits:")
            For Each route In routes
                character.AddMessage($"- {route.Key}: {route.Value.GetName}")
            Next
        End If
        If location.Inventory.HasItems Then
            character.AddMessage("There are items on the ground.")
        End If
    End Sub
End Module
