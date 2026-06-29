Imports MTBP.Persistence

Friend Class ItemModel
    Implements IItemModel

    Private Sub New(item As IItem)
        Me.Item = item
    End Sub

    Public ReadOnly Property Text As String Implements IItemModel.Text
        Get
            Return Item.GetName()
        End Get
    End Property

    Public ReadOnly Property Item As IItem

    Public Sub Drop() Implements IItemModel.Drop
        Dim world = Item.World
        world.ClearMessages()
        Dim character = world.Avatar
        Item.Inventory = character.Location.Inventory
        character.AddMessage($"{character.GetName} drops {Item.GetName}.")
    End Sub

    Public Sub Place(featureModel As IFeatureModel) Implements IItemModel.Place
        Dim feature = featureModel.GetFeature()
        Dim world = Item.World
        world.ClearMessages()
        Dim character = world.Avatar
        character.AddMessage($"{character.GetName()} places {Item.GetName()} on {feature.GetName()}.")
        Item.Inventory = feature.Inventory
    End Sub

    Public Sub Take() Implements IItemModel.Take
        Dim world = Item.World
        world.ClearMessages()
        Dim character = world.Avatar
        Item.Inventory = character.Inventory
        character.AddMessage($"{character.GetName} takes {Item.GetName}.")
    End Sub

    Friend Shared Function Create(item As IItem) As IItemModel
        Return New ItemModel(item)
    End Function

End Class
