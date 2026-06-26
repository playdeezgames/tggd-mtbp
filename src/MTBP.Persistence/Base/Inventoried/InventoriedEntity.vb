Imports MTBP.Provision

Friend MustInherit Class InventoriedEntity(Of TData As InventoriedEntityData)
    Inherits MTBPEntity(Of TData)

    Protected Sub New(world As IWorld, data As WorldData)
        MyBase.New(world, data)
    End Sub
End Class
