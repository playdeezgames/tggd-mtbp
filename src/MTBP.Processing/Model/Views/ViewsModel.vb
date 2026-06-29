Imports MTBP.Persistence
Imports TGGD.Processing

Friend Class ViewsModel
    Inherits BaseModel(Of IWorld)
    Implements IViewsModel

    Protected Sub New(entity As IWorld)
        MyBase.New(entity)
    End Sub

    Friend Shared Function Create(entity As IWorld) As IViewsModel
        Return New ViewsModel(entity)
    End Function

    Public Sub ShowStatus() Implements IViewsModel.ShowStatus
        Entity.ClearMessages()
        Entity.Avatar.ShowStatus()
    End Sub

    Public Sub ShowLocation(clearMessages As Boolean) Implements IViewsModel.ShowLocation
        If clearMessages Then
            Entity.ClearMessages()
        End If
        Entity.Avatar.Look()
    End Sub
End Class
