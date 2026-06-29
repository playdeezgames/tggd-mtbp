Imports MTBP.Persistence
Imports TGGD.Processing

Friend Class ExitsModel
    Inherits BaseModel(Of IWorld)
    Implements IExitsModel

    Protected Sub New(entity As IWorld)
        MyBase.New(entity)
    End Sub

    Public ReadOnly Property HasAny As Boolean Implements IExitsModel.HasAny
        Get
            Return Not Entity.Avatar.IsDead AndAlso Entity.Avatar.Location.Routes.Any()
        End Get
    End Property

    Public ReadOnly Property All As IEnumerable(Of IExitModel) Implements IExitsModel.All
        Get
            Return Entity.Avatar.Location.Routes.Select(Function(x) ExitModel.Create(x.Key, x.Value))
        End Get
    End Property

    Public Sub Move(direction As String) Implements IExitsModel.Move
        Entity.ClearMessages()
        Entity.AddMessage($"{Entity.Avatar.GetName()} moves {direction}.")
        Entity.Avatar.Location = Entity.Avatar.Location.Routes(direction).Destination
        Entity.Avatar.HandleToxicity()
        Entity.Avatar.HandleHunger()
    End Sub

    Friend Shared Function Create(entity As IWorld) As IExitsModel
        Return New ExitsModel(entity)
    End Function
End Class
