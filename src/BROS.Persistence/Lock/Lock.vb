Imports BROS.Provision

Friend Class Lock
    Inherits BROSEntity(Of LockData)
    Implements ILock

    Private Sub New(world As IWorld, data As WorldData, lockId As Guid)
        MyBase.New(world, data)
        Me.LockId = lockId
    End Sub

    Public ReadOnly Property Key As IItem Implements ILock.Key
        Get
            Return Item.Create(World, _data, Data.KeyItemId)
        End Get
    End Property

    Public ReadOnly Property LockId As Guid Implements ILock.LockId

    Protected Overrides ReadOnly Property Data As LockData
        Get
            Return _data.Locks(LockId)
        End Get
    End Property

    Friend Shared Function Create(world As IWorld, data As WorldData, lockId As Guid?) As ILock
        If lockId Is Nothing Then
            Return Nothing
        End If
        Return New Lock(world, data, lockId.Value)
    End Function
End Class
