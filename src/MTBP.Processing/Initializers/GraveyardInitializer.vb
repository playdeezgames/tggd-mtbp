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
        mazeLocations(GRAVEYARD_CENTER_COLUMN, GRAVEYARD_ROWS - 1).CreateRoute(Directions.SOUTH, context.ChurchYard, AddressOf InitializeGraveyardExit)
        context.ChurchYard.CreateRoute(Directions.NORTH, mazeLocations(GRAVEYARD_CENTER_COLUMN, GRAVEYARD_ROWS - 1), AddressOf InitializeGraveyardEntrance)
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
