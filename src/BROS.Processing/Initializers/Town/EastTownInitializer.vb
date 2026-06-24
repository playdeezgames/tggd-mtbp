Imports BROS.Persistence

Friend Module EastTownInitializer
    Friend Function Initialize(context As IInitializationContext) As Action(Of ILocation)
        Return Sub(location)
                   location.SetName("East End")
                   location.SetDescription("This is the east end of town.")
                   context.EastTownLocation = location
                   location.CreateRoute(Directions.WEST, context.TownCenterLocation, AddressOf TownCenterRoute)
                   context.TownCenterLocation.CreateRoute(Directions.EAST, location, AddressOf TownCenterRoute)
                   location.CreateCharacter(AddressOf SpriteVendor)
               End Sub
    End Function

    Private Sub SpriteVendor(character As ICharacter)
        character.SetName("sprite vendor")
        character.AddNouns(Nouns.VENDOR)
        character.SetDescription("This is a sprite vendor. They sell sprites. In cages. For money.")
        character.CreateDialog(AddressOf SpriteVendorGreeting)
        character.CreateTopic(Topics.SPRITE, AddressOf SpriteVendorSpriteTopic)
        character.CreateTopic(Topics.CAGE, AddressOf SpriteVendorCageTopic)
    End Sub

    Private Sub SpriteVendorCageTopic(topic As ITopic)
        topic.Message = "Once you have consumed the sprite to your satisfaction, which is guaranteed, simply return this cage for a refund of yer 5 jools deposit."
    End Sub

    Private Sub SpriteVendorSpriteTopic(topic As ITopic)
        topic.Message = "I sell only the freshest, tastiest sprites! Just 5 jools plus the 5 jool cage deposit!"
    End Sub

    Private Sub SpriteVendorGreeting(dialog As IDialog)
        dialog.Message = "Sprites! Refreshing sprites sold here! Only the crunchiest wings! Only 5 jools each, with a 5 jool deposit for the cage."
    End Sub

    Private Sub TownCenterRoute(route As IRoute)
        route.SetName("road")
        route.SetDescription("This is a road between the east end of town and the town center.")
    End Sub
End Module
