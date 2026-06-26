Imports MTBP.Persistence

Friend Class ExitModel
    Implements IExitModel

    Private Sub New(direction As String, route As IRoute)
        Me.Direction = direction
        Me.Text = route.GetName()
    End Sub

    Public ReadOnly Property Text As String Implements IExitModel.Text

    Public ReadOnly Property Direction As String Implements IExitModel.Direction

    Friend Shared Function Create(direction As String, route As IRoute) As IExitModel
        Return New ExitModel(direction, route)
    End Function
End Class
