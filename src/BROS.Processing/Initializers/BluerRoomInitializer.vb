Imports BROS.Persistence

Friend Module BluerRoomInitializer
    Private Sub InitializePortal(route As IRoute)
        route.SetName("portal")
        route.SetDescription($"Portal to {route.DestinationLocation.GetName}.")
    End Sub

    Private Sub InitializeExitLock(lock As ILock)
        lock.SetName("Mister Lock")
        lock.SetDescription("This is a standard Mister Lock. Legally distinct.")
    End Sub

    Private Sub InitializeEntrance(route As IRoute)
        route.SetName("entrance")
        route.SetDescription("Through this, you can enter the Bluer Room!")
    End Sub

    Private Sub InitializeExit(route As IRoute)
        route.SetName("exit")
        route.SetDescription("Through this, you can exit the Bluer Room!")
    End Sub

    Private Sub InitializeTable(feature As IFeature)
        feature.SetName("table")
        feature.AddNouns(Nouns.TABLE)
        feature.SetDescription("It is an old table that only wobbles a little bit.")
        feature.Inventory.CreateItem(AddressOf InitializeNote)
        feature.Inventory.AddPrepositions(Prepositions.ON)
    End Sub

    Private Sub InitializeNote(item As IItem)
        item.SetName("note")
        item.AddNouns(Nouns.NOTE)
        item.SetDescription("The note reads: ""Hello! I woke up this morning to find that my tits were missing! I have gone to look them. - Yermom""")
    End Sub

    Private Sub InitializeN00b(character As ICharacter)
        character.SetName("Olen Kyrpa")
        character.AddNouns(Nouns.MYSELF, Nouns.ME)
        character.SetTag(Tags.IS_AVATAR)
        character.CreateEquipSlot(AddressOf InitializeButthole)
        character.World.Avatar = character
    End Sub

    Private Sub InitializeButthole(equipSlot As IEquipSlot)
        equipSlot.SetName("butthole")
        equipSlot.AddNouns(Nouns.BUTTHOLE)
        equipSlot.AddPrepositions(Prepositions.IN)
        equipSlot.SetEquipSlotType(EquipSlotTypes.RECTUM)
        equipSlot.SetDescription("It's a butthole. It looks kinda like this: *.")
        equipSlot.CreateItem(AddressOf InitializeKey)
    End Sub

    Private Sub InitializeKey(item As IItem)
        item.SetName("Ass-Key")
        item.AddNouns(Nouns.ASSKEY, Nouns.KEY)
        item.SetEquippable(EquipSlotTypes.RECTUM)
        item.SetDescription("This key smells like poop. I wonder why. Quit sniffing it, and maybe go wash yer hands.")
    End Sub

    Friend Function Initialize(context As IInitializationContext) As Action(Of ILocation)
        Return Sub(location)
                   location.SetName("The Bluer Room")
                   Dim character = location.CreateCharacter(AddressOf InitializeN00b)
                   location.CreateFeature(AddressOf InitializeTable)
                   Dim exitRoute = location.CreateRoute(Directions.OUT, context.FrontYard, AddressOf InitializeExit)
                   context.FrontYard.CreateRoute(Directions.IN, location, AddressOf InitializeEntrance)
                   exitRoute.CreateLock(character.FindEquipSlotByNoun(Nouns.BUTTHOLE).Item, AddressOf InitializeExitLock)
                   If context.PortalDestination IsNot Nothing Then
                       location.CreateRoute(Directions.SIDEWAYS, context.PortalDestination, AddressOf InitializePortal)
                   End If
               End Sub
    End Function
End Module
