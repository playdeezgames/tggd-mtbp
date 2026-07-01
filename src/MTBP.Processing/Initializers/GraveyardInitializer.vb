Imports MTBP.Persistence
Imports TGGD.Processing

Friend Module GraveyardInitializer
    Private ReadOnly directionTable As IReadOnlyDictionary(Of String, MazeDirection(Of String)) =
        New Dictionary(Of String, MazeDirection(Of String)) From
        {
            {Directions.NORTH, New MazeDirection(Of String)(Directions.SOUTH, 0, -1)},
            {Directions.EAST, New MazeDirection(Of String)(Directions.WEST, 1, 0)},
            {Directions.SOUTH, New MazeDirection(Of String)(Directions.NORTH, 0, 1)},
            {Directions.WEST, New MazeDirection(Of String)(Directions.EAST, -1, 0)}
        }
    Friend Sub Initialize(world As IWorld, context As IInitializationContext)
        Dim maze As New Maze(Of String)(GRAVEYARD_COLUMNS, GRAVEYARD_ROWS, directionTable)
        maze.Generate()
        Dim mazeLocations As ILocation(,) = GenerateMazeLocations(world, maze)
        Dim graveYardEntrance = mazeLocations(GRAVEYARD_CENTER_COLUMN, GRAVEYARD_ROWS - 1)
        SetDepth(graveYardEntrance, 0)
        graveYardEntrance.CreateRoute(Directions.SOUTH, context.ChurchYard, AddressOf InitializeGraveyardExit)
        context.ChurchYard.CreateRoute(Directions.NORTH, graveYardEntrance, AddressOf InitializeGraveyardEntrance)
        world.SetGodName(PopulateGraveyard(mazeLocations.Cast(Of ILocation), context))
    End Sub

    Private ReadOnly ringSpawners As IReadOnlyList(Of LocationInitializer) =
        New List(Of LocationInitializer) From
        {
            SpawnRing("Amber Ring", "This is an amber ring. A is for amber. Hint hint!", {Tags.RING}, RingTypes.AMBER),
            SpawnRing("Bone Ring", "This is a bone ring. B is for bone. Hint hint!", {Tags.RING}, RingTypes.BONE),
            SpawnRing("Jade Ring", "This is a jade ring. J is for jade. Hint hint!", {Tags.RING}, RingTypes.JADE),
            SpawnRing("Ebony Ring", "This is a ebony ring. E is for ebony. Hint hint!", {Tags.RING}, RingTypes.EBONY),
            SpawnRing("Ivory Ring", "This is a ivory ring. I is for ivory. Hint hint!", {Tags.RING}, RingTypes.IVORY),
            SpawnRing("Silver Ring", "This is a silver ring. S is for silver. Hint hint!", {Tags.RING}, RingTypes.SILVER)
        }

    Private Function SpawnRing(itemName As String, itemDescription As String, itemTags() As String, ringType As String) As LocationInitializer
        Return Sub(location) location.Inventory.CreateItem(CreateItem(itemName, itemDescription, itemTags, ringType))
    End Function

    Private Function CreateItem(itemName As String, itemDescription As String, itemTags() As String, ringType As String) As ItemInitializer
        Return Sub(item)
                   item.SetName(itemName)
                   item.SetDescription(itemDescription)
                   For Each itemTag In itemTags
                       item.SetTag(itemTag)
                   Next
                   item.SetRingType(ringType)
               End Sub
    End Function

    Private Function PopulateGraveyard(graveyardLocations As IEnumerable(Of ILocation), context As IInitializationContext) As String
        Dim deadEnds As New Queue(Of ILocation)(graveyardLocations.Where(Function(x) x.Routes.Count = 1).OrderByDescending(Function(x) x.GetCounter(Counters.DEPTH)))
        For Each ringSpawner In ringSpawners
            ringSpawner.Invoke(deadEnds.Dequeue)
        Next
        Return PopulateClues(graveyardLocations, context)
    End Function

    Private Function PopulateClues(graveyardLocations As IEnumerable(Of ILocation), context As IInitializationContext) As String
        Dim ringTypes As New HashSet(Of String)(Processing.RingTypes.ALL)
        Dim clues As New List(Of String)
        Dim result As String = ""
        For Each alcoveNumber In Enumerable.Range(1, ALCOVE_COUNT)
            Dim alcoveRingType = context.AlcoveTags.Dequeue
            ringTypes.Remove(alcoveRingType)
            Dim letter = alcoveRingType.First
            result = result & letter
            For Each guess In Enumerable.Range(1, ALCOVE_COUNT)
                If guess = alcoveNumber Then
                    Continue For
                End If
                clues.Add($"{letter} is not in the {GetOrdinal(guess)} position.")
            Next
        Next
        For Each ringTag In ringTypes
            For Each alcoveNumber In Enumerable.Range(1, ALCOVE_COUNT)
                clues.Add($"{ringTag.First} is not in the {GetOrdinal(alcoveNumber)} position.")
            Next
        Next
        Dim clueQueue As New Queue(Of String)(clues.OrderBy(Function(x) Guid.NewGuid))
        Dim locationQueue As New Queue(Of ILocation)(graveyardLocations.OrderBy(Function(x) Guid.NewGuid))
        For Each clueNumber In Enumerable.Range(1, clues.Count)
            locationQueue.Dequeue().CreateFeature(CreateClue(clueNumber, clueQueue.Dequeue))
        Next
        Return result
    End Function

    Private Function CreateClue(clueNumber As Integer, clueText As String) As FeatureInitializer
        Return Sub(feature)
                   feature.SetName($"Grave Marker")
                   feature.SetDescription($"The inscription says ""{clueText}""")
               End Sub
    End Function

    Private Function GetOrdinal(number As Integer) As String
        Select Case number
            Case 1
                Return "first"
            Case 2
                Return "second"
            Case 3
                Return "third"
            Case 4
                Return "fourth"
            Case 5
                Return "fifth"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Sub SetDepth(location As ILocation, depth As Integer)
        If location.HasCounter(Counters.DEPTH) Then
            Return
        End If
        location.SetCounter(Counters.DEPTH, depth)
        For Each route In location.Routes
            SetDepth(route.Value.Destination, depth + 1)
        Next
    End Sub

    Private Sub InitializeGraveyardEntrance(route As IRoute)
        route.SetName("Graveyard Entrance")
        route.SetDescription("You can enter the graveyard here. You may abandon all hope if you wish, but you don't have do, because there isn't a sign telling you to.")
    End Sub

    Private Sub InitializeGraveyardExit(route As IRoute)
        route.SetName("Graveyard Exit")
        route.SetDescription("You can exit the graveyard here. If you abandoned you hope here, you can pick it back up if you like.")
    End Sub

    Private Function GenerateMazeLocations(world As IWorld, maze As Maze(Of String)) As ILocation(,)
        Dim mazeLocations(CInt(maze.Columns) - 1, CInt(maze.Rows) - 1) As ILocation
        For Each column In Enumerable.Range(0, CInt(maze.Columns))
            For Each row In Enumerable.Range(0, CInt(maze.Rows))
                mazeLocations(column, row) = world.CreateLocation(AddressOf InitializeGraveyardLocation)
            Next
        Next
        For Each column In Enumerable.Range(0, CInt(maze.Columns))
            For Each row In Enumerable.Range(0, CInt(maze.Rows))
                Dim mazeCell = maze.GetCell(column, row)
                Dim location = mazeLocations(column, row)
                For Each direction In directionTable.Keys
                    Dim mazeDoor = mazeCell.GetDoor(direction)
                    If If(mazeDoor?.Open, False) Then
                        Dim nextColumn = column + CInt(directionTable(direction).DeltaX)
                        Dim nextRow = row + CInt(directionTable(direction).DeltaY)
                        Dim nextLocation = mazeLocations(nextColumn, nextRow)
                        location.CreateRoute(direction, nextLocation, AddressOf InitializeGraveyardPath)
                    End If
                Next
            Next
        Next
        Return mazeLocations
    End Function

    Private Sub InitializeGraveyardPath(route As IRoute)
        route.SetName("path")
        route.SetDescription("Go this way to enjoy yet another portion of this graveyard!")
    End Sub

    Private Sub InitializeGraveyardLocation(location As ILocation)
        location.SetName("graveyard")
        location.SetDescription("You know, in most actual graveyards, the grass is green and well tended. But for some reason, in games like this, it's all brown and gray and foggy and creepy. I don't even know where that tense music is coming from.")
        location.SetCounter(Counters.TOXICITY, RNG.RollDice(GRAVEYARD_TOXICITY_DICE))
    End Sub
End Module
