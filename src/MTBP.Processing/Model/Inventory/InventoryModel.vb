Imports MTBP.Persistence
Imports TGGD.Processing

Friend Class InventoryModel
    Inherits BaseModel(Of IWorld)
    Implements IInventoryModel

    Protected Sub New(entity As IWorld)
        MyBase.New(entity)
    End Sub

    Public ReadOnly Property LegacyHasItems As Boolean Implements IInventoryModel.LegacyHasItems
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public ReadOnly Property LegacyInventoryItems As IEnumerable(Of IItemModel) Implements IInventoryModel.LegacyInventoryItems
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Friend Shared Function Create(entity As IWorld) As IInventoryModel
        Return New InventoryModel(entity)
    End Function
End Class
