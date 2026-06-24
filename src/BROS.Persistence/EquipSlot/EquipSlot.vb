Imports BROS.Provision

Friend Class EquipSlot
    Inherits BROSEntity(Of EquipSlotData)
    Implements IEquipSlot

    Private Sub New(
                   world As IWorld,
                   data As WorldData,
                   equipSlotId As Guid)
        MyBase.New(world, data)
        Me.EquipSlotId = equipSlotId
    End Sub

    Public ReadOnly Property EquipSlotId As Guid Implements IEquipSlot.EquipSlotId

    Public ReadOnly Property DisplayPreposition As String Implements IBaseInventory.DisplayPreposition
        Get
            Return Data.Prepositions.First.ToLower
        End Get
    End Property

    Public ReadOnly Property Item As IItem Implements IEquipSlot.Item
        Get
            Return If(
                Data.ItemId.HasValue,
                Persistence.Item.Create(World, _data, Data.ItemId.Value),
                Nothing)
        End Get
    End Property

    Protected Overrides ReadOnly Property Data As EquipSlotData
        Get
            Return _data.EquipSlots(EquipSlotId)
        End Get
    End Property

    Public Sub AddPrepositions(ParamArray prepositions() As String) Implements IBaseInventory.AddPrepositions
        For Each preposition In prepositions
            Data.Prepositions.Add(preposition)
        Next
    End Sub

    Friend Shared Function Create(world As IWorld, data As WorldData, equipSlotId As Guid?) As IEquipSlot
        If equipSlotId Is Nothing Then
            Return Nothing
        End If
        Return New EquipSlot(world, data, equipSlotId.Value)
    End Function

    Public Function CreateItem(initializer As Action(Of IItem)) As IItem Implements IEquipSlot.CreateItem
        Dim itemId As Guid = Guid.NewGuid
        _data.Items(itemId) = New ItemData
        Dim result = Persistence.Item.Create(World, _data, itemId)
        result.EquipSlot = Me
        initializer?.Invoke(result)
        Return result
    End Function

    Public Function HasPreposition(preposition As String) As Boolean Implements IBaseInventory.HasPreposition
        Return Data.Prepositions.Contains(preposition)
    End Function
End Class
