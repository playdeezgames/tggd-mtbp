Imports MTBP.Persistence

Friend Class InventoryItemModel
    Implements IInventoryItemModel

    Private Sub New(item As IItem)
        Me.Item = item
    End Sub

    Public ReadOnly Property Text As String Implements IInventoryItemModel.Text
        Get
            Return Item.GetName()
        End Get
    End Property

    Public ReadOnly Property Item As IItem

    Public Sub Drop() Implements IInventoryItemModel.Drop
        Dim world = Item.World
        Dim character = world.Avatar
        Item.Inventory = character.Location.Inventory
        character.AddMessage($"{character.GetName} drops {Item.GetName}.")
    End Sub

    Public Sub Place(featureModel As IFeatureModel) Implements IInventoryItemModel.Place
        Dim feature = featureModel.GetFeature()
        Dim world = Item.World
        Dim character = world.Avatar
        character.AddMessage($"{character.GetName()} places {Item.GetName()} on {feature.GetName()}.")
        Item.Inventory = feature.Inventory
    End Sub

    Public Sub Take() Implements IInventoryItemModel.Take
        Dim world = Item.World
        Dim character = world.Avatar
        Item.Inventory = character.Inventory
        character.AddMessage($"{character.GetName} takes the {Item.GetName}.")
    End Sub

    Friend Shared Function Create(item As IItem) As IInventoryItemModel
        Return New InventoryItemModel(item)
    End Function

End Class
