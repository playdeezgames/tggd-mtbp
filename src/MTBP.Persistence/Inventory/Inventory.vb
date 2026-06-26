Imports MTBP.Provision

Friend Class Inventory
    Implements IInventory

    Private Sub New(world As IWorld, data As WorldData, inventoryId As Guid)
        Me.World = world
        Me._data = data
        Me.InventoryId = inventoryId
    End Sub

    Private ReadOnly Property Data As InventoryData
        Get
            Return _data.Inventories(InventoryId)
        End Get
    End Property

    Public ReadOnly Property HasItems As Boolean Implements IInventory.HasItems
        Get
            Return Data.ItemIds.Count <> 0
        End Get
    End Property

    Public ReadOnly Property World As IWorld Implements IInventory.World

    Public ReadOnly Property InventoryId As Guid Implements IInventory.InventoryId

    Public ReadOnly Property Items As IEnumerable(Of IItem) Implements IInventory.Items
        Get
            Return Data.ItemIds.Select(Function(x) Item.Create(World, _data, x))
        End Get
    End Property

    Private ReadOnly _data As WorldData

    Friend Shared Function Create(world As IWorld, data As WorldData, inventoryId As Guid?) As IInventory
        If Not inventoryId.HasValue Then
            Return Nothing
        End If
        Return New Inventory(world, data, inventoryId.Value)
    End Function

    Public Function CreateItem(Optional initializer As ItemInitializer = Nothing) As IItem Implements IInventory.CreateItem
        Dim itemId = Guid.NewGuid
        _data.Items(itemId) = New ItemData With
            {
                .InventoryId = InventoryId
            }
        Data.ItemIds.Add(itemId)
        Dim result As IItem = Item.Create(World, _data, itemId)
        initializer?.Invoke(result)
        Return result
    End Function
End Class
