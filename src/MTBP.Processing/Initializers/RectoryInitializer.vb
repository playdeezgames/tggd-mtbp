Imports MTBP.Persistence

Friend Module RectoryInitializer
    Friend Function Initialize(context As IInitializationContext) As LocationInitializer
        Return Sub(location)
                   location.SetName("Rectory")
                   location.CreateCharacter(InitializeRector(context))
                   context.Rectory = location
               End Sub
    End Function

    Private Function InitializeRector(context As IInitializationContext) As CharacterInitializer
        Return Sub(character)
                   character.SetName("Ölën Kÿrpä")
                   character.World.Avatar = character
                   character.SetCounter(Counters.IMMUNITY, MAXIMUM_IMMUNITY)
                   character.SetCounterMaximum(Counters.IMMUNITY, MAXIMUM_IMMUNITY)
                   character.SetCounterMinimum(Counters.IMMUNITY, MINIMUM_IMMUNITY)
                   character.SetCounter(Counters.HEALTH, MAXIMUM_HEALTH)
                   character.SetCounterMaximum(Counters.HEALTH, MAXIMUM_HEALTH)
                   character.SetCounterMinimum(Counters.HEALTH, MINIMUM_HEALTH)
               End Sub
    End Function

End Module
