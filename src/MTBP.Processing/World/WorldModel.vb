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

    Public ReadOnly Property Messages As IEnumerable(Of String) Implements IWorldModel.Messages
        Get
            Return Entity.Messages
        End Get
    End Property

    Public Sub Embark() Implements IWorldModel.Embark
        Abandon()
        Entity.Initialize(InitializationContext.Create())
    End Sub

    Public Sub Abandon() Implements IWorldModel.Abandon
        Dim quittable = IsQuittable
        Entity.Clear()
        If quittable Then
            Entity.SetTag(Tags.QUITTABLE)
        End If
    End Sub

    Public Shared Async Function Create(quittable As Boolean, persister As IPersister) As Task(Of IWorldModel)
        Dim entity As IWorld
        Try
            entity = Await MTBP.Persistence.World.Load(SAVE_FILE_NAME, persister)
        Catch ex As Exception
            entity = MTBP.Persistence.World.Create(New Provision.WorldData, persister)
        End Try
        If quittable Then
            entity.SetTag(Tags.QUITTABLE)
        End If
        Return New WorldModel(entity)
    End Function
End Class
