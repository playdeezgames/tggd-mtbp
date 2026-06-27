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


    Friend Shared Function Create(item As IItem) As IInventoryItemModel
        Return New InventoryItemModel(item)
    End Function

End Class
