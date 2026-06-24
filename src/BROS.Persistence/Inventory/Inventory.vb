Imports BROS.Provision

Friend Class Inventory
    Implements IInventory
    Private Sub New(world As IWorld, data As WorldData, inventoryId As Guid)
        Me.world = world
        Me.InventoryId = inventoryId
        Me._data = data
    End Sub

    Private ReadOnly world As IWorld
    Public ReadOnly Property InventoryId As Guid Implements IInventory.InventoryId
    Private ReadOnly _data As WorldData
    Private ReadOnly Property Data As InventoryData
        Get
            Return _data.Inventories(InventoryId)
        End Get
    End Property

    Public ReadOnly Property Items As IEnumerable(Of IItem) Implements IInventory.Items
        Get
            Return Data.ItemIds.Select(Function(x) Item.Create(world, _data, x))
        End Get
    End Property

    Public ReadOnly Property DisplayPreposition As String Implements IInventory.DisplayPreposition
        Get
            Return Data.Prepositions.First.ToLower
        End Get
    End Property

    Friend Shared Function Create(world As IWorld, data As WorldData, inventoryId As Guid?) As IInventory
        If inventoryId Is Nothing Then
            Return Nothing
        End If
        Return New Inventory(world, data, inventoryId.Value)
    End Function

    Public Function CreateItem(Optional initializer As Action(Of IItem) = Nothing) As IItem Implements IInventory.CreateItem
        Dim itemId As Guid = Guid.NewGuid
        _data.Items(itemId) = New ItemData
        Dim result = Item.Create(world, _data, itemId)
        result.Inventory = Me
        initializer?.Invoke(result)
        Return result
    End Function

    Public Sub AddPrepositions(ParamArray prepositions() As String) Implements IInventory.AddPrepositions
        For Each preposition In prepositions
            Data.Prepositions.Add(preposition)
        Next
    End Sub

    Public Function HasPreposition(preposition As String) As Boolean Implements IInventory.HasPreposition
        Return Data.Prepositions.Any(Function(x) x.Equals(preposition, StringComparison.InvariantCultureIgnoreCase))
    End Function

    Public Function FindItemByNoun(noun As String) As IItem Implements IInventory.FindItemByNoun
        Return Items.FirstOrDefault(Function(x) x.HasNoun(noun))
    End Function
End Class
