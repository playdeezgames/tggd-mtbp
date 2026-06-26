Imports MTBP.Persistence

Friend Class InitializationContext
    Implements IInitializationContext
    Private Sub New()

    End Sub

    Public Property Rectory As ILocation Implements IInitializationContext.Rectory

    Public Property ChurchYard As ILocation Implements IInitializationContext.ChurchYard

    Friend Shared Function Create() As IInitializationContext
        Return New InitializationContext()
    End Function
End Class
