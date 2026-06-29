Imports MTBP.Persistence

Friend Module RectoryInitializer
    Friend Function Initialize(context As IInitializationContext) As LocationInitializer
        Return Sub(location)
                   location.SetName("Rectory")
                   location.SetDescription("A rectory is the place where a rector lives. A rector is a person who live in a rectory. The world sounds a lot like rectum. Which makes me giggle.")
                   location.CreateCharacter(InitializeRector(context))
                   context.Rectory = location
               End Sub
    End Function

    Private Function InitializeRector(context As IInitializationContext) As CharacterInitializer
        Return Sub(character)
                   character.SetName("Ölën Kÿrpä")
                   character.SetDescription("This is you.")
                   character.World.Avatar = character
                   character.SetCounter(Counters.IMMUNITY, MAXIMUM_IMMUNITY)
                   character.SetCounterMaximum(Counters.IMMUNITY, MAXIMUM_IMMUNITY)
                   character.SetCounterMinimum(Counters.IMMUNITY, MINIMUM_IMMUNITY)
                   character.SetCounter(Counters.HEALTH, MAXIMUM_HEALTH)
                   character.SetCounterMaximum(Counters.HEALTH, MAXIMUM_HEALTH)
                   character.SetCounterMinimum(Counters.HEALTH, MINIMUM_HEALTH)
                   character.SetCounter(Counters.HUNGER_RATE, 1)
                   character.SetCounter(Counters.SATIETY, MAXIMUM_SATIETY)
                   character.SetCounterMaximum(Counters.SATIETY, MAXIMUM_SATIETY)
                   character.SetCounterMinimum(Counters.SATIETY, MINIMUM_SATIETY)
               End Sub
    End Function

End Module
