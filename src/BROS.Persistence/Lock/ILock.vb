Public Interface ILock
    Inherits IBROSEntity
    ReadOnly Property LockId As Guid
    ReadOnly Property Key As IItem
End Interface
