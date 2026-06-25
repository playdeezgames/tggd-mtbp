Friend Class InitializationContext
    Implements IInitializationContext
    Private Sub New()

    End Sub

    Friend Shared Function Create() As IInitializationContext
        Return New InitializationContext()
    End Function
End Class
