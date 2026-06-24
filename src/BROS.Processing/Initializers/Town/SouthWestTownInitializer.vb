Imports BROS.Persistence

Friend Module SouthWestTownInitializer
    Friend Function Initialize(context As IInitializationContext) As Action(Of ILocation)
        Return Sub(location)
                   location.SetName("southwest corner")
                   location.SetDescription("This is the dead-end southwest corner of Quotidian.")
                   location.CreateCharacter(AddressOf Beggar)
                   context.SouthWestTownLocation = location
                   location.World.CreateLocation(FrontYardInitializer.Initialize(context))
                   location.CreateRoute(Directions.EAST, context.SouthTownLocation, AddressOf CornerRoad)
                   context.SouthTownLocation.CreateRoute(Directions.WEST, location, AddressOf CornerRoad)
               End Sub
    End Function

    Private Sub CornerRoad(route As IRoute)
        route.SetName("road")
        route.SetDescription("This is a road between the south end of town and the southwest corner of town.")
    End Sub

    Private Sub Beggar(character As ICharacter)
        character.SetName("beggar")
        character.AddNouns(Nouns.BEGGAR, Nouns.STREAMBOO)
        character.SetDescription("This is Streamboo, the local beggar. He begs.")
        character.CreateDialog(AddressOf BeggarDemandSprite)
        character.CreateDialog(AddressOf BeggarGreeting)
        character.CreateTopic(Topics.SPRITE, AddressOf BeggarSpriteTopic)
    End Sub

    Private Sub BeggarSpriteTopic(topic As ITopic)
        topic.Message = "Sprites are my favorite. First, I like to pull off their wings and crunch on them for a minute as the creature wails. Then I bite its head off, and quickly shove the rest of it into my mouth while the nerves are still firing in its death throes."
    End Sub

    Private Sub BeggarDemandSprite(dialog As IDialog)
        dialog.Message = "Streamboo asks ""Where's my sprite?"""
        dialog.RequireTags(Tags.INITIAL_GREETING)
    End Sub

    Private Sub BeggarGreeting(dialog As IDialog)
        dialog.Message = "Streamboo wakes up groggily. You introduce yerself. He tells you that he can provide you with the best viewers, but in exchange he will need a sprite."
        dialog.AddTags(Tags.INITIAL_GREETING)
    End Sub
End Module
