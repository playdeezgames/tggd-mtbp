Imports BROS.Persistence
Imports TGGD.Persistence
Imports TGGD.Processing

Public Class WorldModel
    Inherits BaseModel(Of IWorld)
    Implements IWorldModel

    Private _isMenuRequested As Boolean = False

    Protected Sub New(entity As IWorld)
        MyBase.New(entity)
    End Sub

    Public ReadOnly Property IsInPlay As Boolean Implements IWorldModel.IsInPlay
        Get
            Return Entity.Avatar IsNot Nothing
        End Get
    End Property

    Public ReadOnly Property IsQuittable As Boolean Implements IWorldModel.IsQuittable
        Get
            Return Entity.HasTag(Tags.QUITTABLE)
        End Get
    End Property

    Public ReadOnly Property IsMenuRequested As Boolean Implements IWorldModel.IsMenuRequested
        Get
            Dim result = _isMenuRequested
            _isMenuRequested = False
            Return result
        End Get
    End Property

    Public ReadOnly Property Messages As IEnumerable(Of IMessageModel) Implements IWorldModel.Messages
        Get
            Return Entity.Messages.Select(AddressOf MessageModel.FromMessage)
        End Get
    End Property

    Public Sub Embark() Implements IWorldModel.Embark
        WorldInitializer.InitializeWorld(Entity)
    End Sub

    Private Sub RequestMenu()
        _isMenuRequested = True
    End Sub

    Public Sub ProcessCommand(command As String) Implements IWorldModel.ProcessCommand
        Dim tokens = New Queue(Of String)(command.Split(" "c))
        Select Case CommandProcessor.Process(Entity, tokens)
            Case CommandProcessorResult.Invalid
                Entity.AddMessage($"Invalid Command: `{command}`!", mood:=Moods.ERROR)
            Case CommandProcessorResult.MenuRequest
                RequestMenu()
        End Select
    End Sub

    Public Shared Async Function Create(quittable As Boolean, persister As IPersister) As Task(Of IWorldModel)
        Dim world As IWorld
        Try
            world = Await BROS.Persistence.World.Load(SAVE_FILE_NAME, persister)
        Catch ex As Exception
            world = BROS.Persistence.World.Create(New Provision.WorldData, persister)
        End Try
        If quittable Then
            world.SetTag(Tags.QUITTABLE)
        End If
        Return New WorldModel(world)
    End Function
End Class
