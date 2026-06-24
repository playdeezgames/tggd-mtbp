Imports BROS.Persistence

Friend Module SouthTownInitializer
    Friend Function Initialize(context As IInitializationContext) As Action(Of ILocation)
        Return Sub(location As ILocation)
                   context.PortalDestination = location
                   location.SetName("South End")
                   location.SetDescription("This is the south end of town.")
                   context.SouthTownLocation = location
                   location.CreateRoute(Directions.NORTH, context.TownCenterLocation, AddressOf TownCenterRoute)
                   context.TownCenterLocation.CreateRoute(Directions.SOUTH, location, AddressOf TownCenterRoute)
                   location.World.CreateLocation(SouthWestTownInitializer.Initialize(context))
                   location.CreateCharacter(AddressOf InitializeTempleGuard)
                   Dim feature = location.CreateFeature(AddressOf UtilityInitializers.InitializeFloor)
                   feature.Inventory.CreateItem(AddressOf InitializeKlart)
               End Sub
    End Function

    Private Sub TownCenterRoute(route As IRoute)
        route.SetName("road")
        route.SetDescription("This is a road between the south end of town and the town center.")
    End Sub

    Private Sub InitializeKlart(item As IItem)
        item.SetName("klart")
        item.AddNouns(Nouns.KLART, Nouns.SHIT)
        item.SetEdible()
        item.SetDescription("This is a piece of klart, which is another word for shit. Poop, crap. Caca. The stuff that comes out of butts without having to be placed there deliberately. If you eat it, you will die.")
    End Sub

    Private Sub InitializeTempleGuard(character As ICharacter)
        character.SetName("temple guard")
        character.AddNouns(Nouns.GUARD)
        character.SetDescription("This is the guard to the Temple of the Perfect Fit. He is wearing a corset that is too tight, and a little too much rouge. His shapely legs are incredibly hairy and clad in fishnet stockings.")
        character.CreateDialog(AddressOf InitializeTempleGuardDialog)
        character.CreateTopic(Topics.CORSET, AddressOf InitializeCorsetTopic)
        character.CreateTopic(Topics.CORSETS, AddressOf InitializeCorsetTopic)
        character.CreateTopic(Topics.TEMPLE, AddressOf InitializeTempleTopic)
        character.CreateTopic(Topics.FISHNETS, AddressOf InitializeFishnetsTopic)
        character.CreateTopic(Topics.HEELS, AddressOf InitializeHeelsTopic)
    End Sub

    Private Sub InitializeHeelsTopic(topic As ITopic)
        topic.Message = "If you want to borrow my spare pair, they are in my house in the northwest corner of town. In a box. In the closet. Don't open the trap door."
    End Sub

    Private Sub InitializeFishnetsTopic(topic As ITopic)
        topic.Message = "I really prefer wearing fishnets over regular hose, mostly because I don't like shaving my legs."
    End Sub

    Private Sub InitializeTempleTopic(topic As ITopic)
        topic.Message = "This is the Temple of the Perfect Fit. We worship the patron diety of squeezing entirely too much flesh into too small of an article of clothing, and looking fabulous while doing so."
    End Sub

    Private Sub InitializeCorsetTopic(topic As ITopic)
        topic.Message = "The corset is the ceremonial garb required by all attendees of the Temple of the Perfect Fit. I simply cannot allow you to enter without one."
    End Sub

    Private Sub InitializeTempleGuardDialog(dialog As IDialog)
        dialog.Message = "The guard says ""The temple has a dress code. Corsets are required. Fishnets are optional, but highly encouraged. High heels are alway nice, especially shiny red ones. I could loan you a pair."""
    End Sub
End Module
