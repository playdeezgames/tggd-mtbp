Imports MTBP.Provision
Imports TGGD.Persistence

Friend MustInherit Class MTBPEntity(Of TData As MTBPEntityData)
    Inherits Entity(Of TData)
    Implements IMTBPEntity

    Protected Sub New(world As IWorld, data As WorldData)
        Me.World = world
        Me._data = data
    End Sub

    Public ReadOnly Property World As IWorld Implements IMTBPEntity.World
    Protected ReadOnly _data As WorldData
End Class
