Imports MTBP.Persistence

Friend Class GroundItemModel
    Implements IGroundItemModel

    Private Sub New(item As IItem)
        Me.Item = item
    End Sub

    Public ReadOnly Property Text As String Implements IGroundItemModel.Text
        Get
            Return Item.GetName()
        End Get
    End Property

    Public ReadOnly Property Item As IItem

    Public Sub Take() Implements IGroundItemModel.Take
        Dim world = Item.World
        Dim character = world.Avatar
        Item.Inventory = character.Inventory
        world.ClearMessages()
        character.AddMessage($"{character.GetName} takes the {Item.GetName}.")
        character.Look()
    End Sub

    Friend Shared Function Create(item As IItem) As IGroundItemModel
        Return New GroundItemModel(item)
    End Function
End Class
