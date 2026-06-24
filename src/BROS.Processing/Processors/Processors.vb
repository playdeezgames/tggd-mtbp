Imports BROS.Persistence

Friend Module Processors
    Private ReadOnly commandList As IReadOnlyList(Of (Command As String, HelpTexts As IEnumerable(Of String), Processor As Func(Of IWorld, Queue(Of String), CommandProcessorResult))) =
        New List(Of (String, IEnumerable(Of String), Func(Of IWorld, Queue(Of String), CommandProcessorResult))) From
        {
            ("menu", {"Brings up the main menu.", "Example:", "    MENU"}, AddressOf MenuCommandProcessor.Process),
            ("help", {"Shows context sensitive help.", "Examples:", "    HELP", "    HELP [COMMAND]"}, AddressOf HelpCommandProcessor.Process),
            ("look", {"Describes the immediate area.", "Example:", "    LOOK"}, AddressOf LookCommandProcessor.Process),
            ("examine", {"Looks at something closely.", "Example:", "    EXAMINE [ITEM/FEATURE/DIRECTION]", "    EXAMINE [ITEM] [PREPOSITION] [FEATURE]"}, AddressOf ExamineCommandProcessor.Process),
            ("check", {"Alias for EXAMINE. For lore reasons. Bend over!"}, AddressOf ExamineCommandProcessor.Process),
            ("take", {"Transfers an item into yer inventory.", "Example:", "    TAKE [ITEM] FROM [FEATURE/EQUIP-SLOT]"}, AddressOf TakeCommandProcessor.Process),
            ("inventory", {"Shows the items in yer inventory.", "Example:", "    INVENTORY"}, AddressOf InventoryCommandProcessor.Process),
            ("drop", {"Drops an item onto the floor.", "Example:", "    DROP [ITEM]"}, AddressOf DropCommandProcessor.Process),
            ("equipment", {"Shows equipment slots and the equipment thereof.", "Example:", "    EQUIPMENT"}, AddressOf EquipmentCommandProcessor.Process),
            ("go", {"Attempts to move in a particular direction.", "Example:", "    GO [DIRECTION]"}, AddressOf GoCommandProcessor.Process),
            ("unlock", {"Attempts to unlock a route in a particular direction with a particular item.", "Example:", "    UNLOCK [DIRECTION] WITH [ITEM]"}, AddressOf UnlockCommandProcessor.Process),
            ("talk", {"Attempts communication with another character.", "Example:", "    TALK TO [CHARACTER]"}, AddressOf TalkCommandProcessor.Process),
            ("ask", {"Posits a query about a selected motif to an interlocutor. If you don't know what to ask about, ask about `TOPICS`.", "Example:", "    ASK [CHARACTER] ABOUT [TOPIC]", "    ASK [CHARACTER] ABOUT TOPICS"}, AddressOf AskCommandProcessor.Process),
            ("equip", {"Attempts to equip an item.", "Example:", "    EQUIP [ITEM] [PREPOSITION] [EQUIP-SLOT]"}, AddressOf EquipCommandProcessor.Process),
            ("give", {"Attempts to give an item to another character.", "Example:", "    GIVE [ITEM] TO [CHARACTER]"}, AddressOf GiveCommandProcessor.Process),
            ("eat", {"Attempts to consume an item with yer mouth. (To stick things up yer bum, try the EQUIP command instead.)", "Example:", "    EAT [ITEM]"}, AddressOf EatCommandProcessor.Process)
        }
    Private ReadOnly processorTable As IReadOnlyDictionary(Of String, Func(Of IWorld, Queue(Of String), CommandProcessorResult)) =
        commandList.ToDictionary(
            Function(x) x.Command,
            Function(x) x.Processor,
            StringComparer.InvariantCultureIgnoreCase)
    Private ReadOnly processorHelp As Dictionary(Of String, IEnumerable(Of String)) =
        commandList.ToDictionary(
            Function(x) x.Command,
            Function(x) x.HelpTexts,
            StringComparer.InvariantCultureIgnoreCase)
    Friend ReadOnly Property AllCommands As IEnumerable(Of String)
        Get
            Return processorTable.Keys.Order()
        End Get
    End Property
    Friend Function GetProcessor(command As String) As Func(Of IWorld, Queue(Of String), CommandProcessorResult)
        Dim result As Func(Of IWorld, Queue(Of String), CommandProcessorResult) = Nothing
        If processorTable.TryGetValue(command, result) Then
            Return result
        End If
        Return Function(x, y) CommandProcessorResult.Invalid
    End Function
    Friend Function GetHelpTexts(command As String) As IEnumerable(Of String)
        Dim result As IEnumerable(Of String) = Nothing
        If processorHelp.TryGetValue(command, result) Then
            Return result
        End If
        Return {$"There ain't no `{command}` command, fool!"}
    End Function
End Module
