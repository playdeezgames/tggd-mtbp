Public Interface IRoute
    Inherits IBROSEntity
    ReadOnly Property RouteId As Guid
    ReadOnly Property DestinationLocation As ILocation
    Function CreateLock(item As IItem, Optional initializer As Action(Of ILock) = Nothing) As ILock
    Sub DestroyLock()
    ReadOnly Property Lock As ILock
End Interface
