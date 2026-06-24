Imports BROS.Provision

Friend Class Route
    Inherits BROSEntity(Of RouteData)
    Implements IRoute

    Private Sub New(world As IWorld, data As WorldData, routeId As Guid)
        MyBase.New(world, data)
        Me.RouteId = routeId
    End Sub

    Public ReadOnly Property RouteId As Guid Implements IRoute.RouteId

    Public ReadOnly Property DestinationLocation As ILocation Implements IRoute.DestinationLocation
        Get
            Return Location.Create(World, _data, Data.DestinationLocationId)
        End Get
    End Property

    Public ReadOnly Property Lock As ILock Implements IRoute.Lock
        Get
            Return Persistence.Lock.Create(World, _data, Data.LockId)
        End Get
    End Property

    Protected Overrides ReadOnly Property Data As RouteData
        Get
            Return _data.Routes(RouteId)
        End Get
    End Property

    Public Sub DestroyLock() Implements IRoute.DestroyLock
        Dim lockId = Data.LockId
        Data.LockId = Nothing
        If lockId.HasValue Then
            _data.Locks.Remove(lockId.Value)
        End If
    End Sub

    Friend Shared Function Create(world As IWorld, data As WorldData, routeId As Guid?) As IRoute
        If Not routeId.HasValue Then
            Return Nothing
        End If
        Return New Route(world, data, routeId.Value)
    End Function

    Public Function CreateLock(item As IItem, Optional initializer As Action(Of ILock) = Nothing) As ILock Implements IRoute.CreateLock
        Dim lockId = Guid.NewGuid
        _data.Locks(lockId) = New LockData With
            {
                .KeyItemId = item.ItemId
            }
        Data.LockId = lockId
        Dim result = Persistence.Lock.Create(World, _data, lockId)
        initializer?.Invoke(result)
        Return result
    End Function
End Class
