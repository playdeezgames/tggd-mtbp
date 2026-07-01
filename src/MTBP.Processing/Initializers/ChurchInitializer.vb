Imports MTBP.Persistence
Imports TGGD.Processing

Friend Module ChurchInitializer
    Friend Function Initialize(context As IInitializationContext) As LocationInitializer
        Return Sub(location As ILocation)
                   location.SetName("Church")
                   location.SetDescription("This is a building for talking to the invisible sky-man. I ain't yer judge.")
                   context.Church = location
                   location.CreateFeature(AddressOf InitializeAltar)
                   context.AlcoveTags = CreateAlcoves(location)

                   location.CreateRoute(Directions.EAST, context.ChurchYard, AddressOf InitializeChurchExit)
                   context.ChurchYard.CreateRoute(Directions.WEST, location, AddressOf InitializeChurchEntrance)
               End Sub
    End Function

    Private Function CreateAlcoves(location As ILocation) As Queue(Of String)
        Dim result As New Queue(Of String)
        Dim consonants As New Queue(Of String)({RingTypes.BONE, RingTypes.JADE, RingTypes.SILVER}.OrderBy(Function(x) Guid.NewGuid))
        Dim vowels As New Queue(Of String)({RingTypes.AMBER, RingTypes.EBONY, RingTypes.IVORY}.OrderBy(Function(x) Guid.NewGuid))
        Dim isVowel = RNG.FromGenerator(RNG.MakeBooleanGenerator(1, 1))
        For Each alcoveNumber In Enumerable.Range(1, ALCOVE_COUNT)
            Dim ringType = If(isVowel, vowels.Dequeue, consonants.Dequeue)
            result.Enqueue(ringType)
            isVowel = Not isVowel
            location.CreateFeature(InitializeAlcove(alcoveNumber, ringType))
        Next
        Return result
    End Function

    Private Function InitializeAlcove(alcoveNumber As Integer, ringType As String) As FeatureInitializer
        Return Sub(feature)
                   feature.SetName($"Alcove #{alcoveNumber}")
                   feature.SetDescription($"This is an alcove. There is a ring shaped recess in the midst of it.")
                   feature.SetRingType(ringType)
                   feature.SetTag(Tags.ALCOVE)
                   feature.SetCounter(Counters.ALCOVE_NUMBER, alcoveNumber)
               End Sub
    End Function

    Private Sub InitializeAltar(feature As IFeature)
        feature.SetName("Altar")
        feature.SetDescription("Upon this you can place victims, and sacrifice them. You know, if yer into that. If not, this is really just a table. A sturdy, sturdy table.")
    End Sub

    Private Sub InitializeChurchEntrance(route As IRoute)
        route.SetName("Church Entrance")
        route.SetDescription("Through here you can enter the church.")
    End Sub

    Private Sub InitializeChurchExit(route As IRoute)
        route.SetName("Church Exit")
        route.SetDescription("Through here, you can exit the church.")
    End Sub
End Module
