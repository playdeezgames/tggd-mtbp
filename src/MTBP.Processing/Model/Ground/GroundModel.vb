Imports MTBP.Persistence
Imports TGGD.Processing

Friend Class GroundModel
    Inherits BaseModel(Of IWorld)
    Implements IGroundModel

    Protected Sub New(entity As IWorld)
        MyBase.New(entity)
    End Sub

    Public ReadOnly Property LegacyHasGroundItems As Boolean Implements IGroundModel.LegacyHasGroundItems
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public ReadOnly Property LegacyGroundItems As IEnumerable(Of IItemModel) Implements IGroundModel.LegacyGroundItems
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Friend Shared Function Create(entity As IWorld) As IGroundModel
        Return New GroundModel(entity)
    End Function
End Class
