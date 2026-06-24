Imports MTBP.Persistence
Imports TGGD.Persistence
Imports TGGD.Processing

Public Class WorldModel
    Inherits BaseModel(Of IWorld)
    Implements IWorldModel

    Protected Sub New(entity As IWorld)
        MyBase.New(entity)
    End Sub

    Public ReadOnly Property IsQuittable As Boolean Implements IWorldModel.IsQuittable
        Get
            Return Entity.HasTag(Tags.QUITTABLE)
        End Get
    End Property

    Public Shared Async Function Create(quittable As Boolean, persister As IPersister) As Task(Of IWorldModel)
        Dim world As IWorld
        Try
            world = Await MTBP.Persistence.World.Load(SAVE_FILE_NAME, persister)
        Catch ex As Exception
            world = MTBP.Persistence.World.Create(New Provision.WorldData, persister)
        End Try
        If quittable Then
            world.SetTag(Tags.QUITTABLE)
        End If
        Return New WorldModel(world)
    End Function
End Class
