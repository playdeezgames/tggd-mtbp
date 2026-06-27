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

    Public ReadOnly Property CanMove As Boolean Implements IWorldModel.CanMove
        Get
            Return Entity.Avatar.Location.Routes.Any()
        End Get
    End Property

    Public ReadOnly Property Exits As IEnumerable(Of IExitModel) Implements IWorldModel.Exits
        Get
            Return Entity.Avatar.Location.Routes.Select(Function(x) ExitModel.Create(x.Key, x.Value))
        End Get
    End Property

    Public ReadOnly Property HasGroundItems As Boolean Implements IWorldModel.HasGroundItems
        Get
            Return Entity.Avatar.Location.Inventory.HasItems
        End Get
    End Property

    Public ReadOnly Property GroundItems As IEnumerable(Of IGroundItemModel) Implements IWorldModel.GroundItems
        Get
            Return Entity.Avatar.Location.Inventory.Items.Select(AddressOf GroundItemModel.Create)
        End Get
    End Property

    Public ReadOnly Property HasItems As Boolean Implements IWorldModel.HasItems
        Get
            Return Entity.Avatar.Inventory.HasItems
        End Get
    End Property

    Public ReadOnly Property InventoryItems As IEnumerable(Of IInventoryItemModel) Implements IWorldModel.InventoryItems
        Get
            Return Entity.Avatar.Inventory.Items.Select(AddressOf InventoryItemModel.Create)

        End Get
    End Property

    Public Sub Embark() Implements IWorldModel.Embark
        Abandon()
        Entity.Initialize(InitializationContext.Create())
        Entity.Describe()
    End Sub

    Public Sub Abandon() Implements IWorldModel.Abandon
        Dim quittable = IsQuittable
        Entity.Clear()
        If quittable Then
            Entity.SetTag(Tags.QUITTABLE)
        End If
    End Sub

    Public Sub Move(direction As String) Implements IWorldModel.Move
        Entity.ClearMessages()
        Entity.AddMessage($"{Entity.Avatar.GetName()} moves {direction}.")
        Entity.Avatar.Location = Entity.Avatar.Location.Routes(direction).Destination
        Entity.Describe()
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
