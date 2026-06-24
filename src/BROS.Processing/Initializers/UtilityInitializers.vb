Imports BROS.Persistence

Friend Module UtilityInitializers
    Friend Sub InitializeFloor(feature As IFeature)
        feature.SetName("floor")
        feature.AddNouns(Nouns.FLOOR)
        feature.Inventory.AddPrepositions(Prepositions.ON)
        feature.SetDescription("Its a floor. You know, the thing that exerts a normal force upon yer body to keep you from plummeting to the center of the gravity well of the planet yer on.")
    End Sub
End Module
