Imports BROS.Persistence

Friend Module TownCenterInitializer
    Friend Function Initialize(context As IInitializationContext) As Action(Of ILocation)
        Return Sub(location)
                   location.SetName("Town Center")
                   location.SetDescription("This is the center of town.")
                   context.TownCenterLocation = location
                   location.World.CreateLocation(SouthTownInitializer.Initialize(context))
                   location.World.CreateLocation(NorthTownInitializer.Initialize(context))
                   location.World.CreateLocation(EastTownInitializer.Initialize(context))
                   location.World.CreateLocation(WestTownInitializer.Initialize(context))
                   location.CreateFeature(AddressOf Shrine)
                   location.CreateFeature(AddressOf JobBoard)
               End Sub
    End Function

    Private Sub JobBoard(feature As IFeature)
        feature.SetName("job board")
        feature.AddNouns(Nouns.BOARD)
        feature.Inventory.AddPrepositions(Prepositions.ON)
        feature.SetDescription("This is the local job board. People post jobs on it.")
        feature.Inventory.CreateItem(AddressOf JobBoardPosting)
    End Sub

    Private Sub JobBoardPosting(item As IItem)
        item.SetName("job posting")
        item.AddNouns(Nouns.POSTING, Nouns.JOB)
        item.SetDescription("The posting reads: ""This is not a joke! Slap Dattass and earn 10 jools! Find Dattass wherever he is, and slap him!""")
    End Sub

    Private Sub Shrine(feature As IFeature)
        feature.SetName("shrine")
        feature.AddNouns(Nouns.SHRINE, Nouns.BUTTPLUG)
        feature.Inventory.AddPrepositions(Prepositions.ON)
        feature.SetDescription("This is the memorial shrine for Captain Jack. He was a good kitty. RIP Jack. To be honest, however, the shrine does indeed look more than a little bit like a giant buttplug.")
    End Sub
End Module
