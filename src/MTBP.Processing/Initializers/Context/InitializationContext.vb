Imports MTBP.Persistence

Friend Class InitializationContext
    Implements IInitializationContext
    Private Sub New()

    End Sub

    Public Property Rectory As ILocation Implements IInitializationContext.Rectory

    Public Property ChurchYard As ILocation Implements IInitializationContext.ChurchYard

    Public Property AbandonedHouse As ILocation Implements IInitializationContext.AbandonedHouse

    Public Property Church As ILocation Implements IInitializationContext.Church

    Public ReadOnly Property IsDebug As Boolean Implements IInitializationContext.IsDebug
        Get
#If DEBUG Then
            Return True
#Else
            Return False    
#End If
        End Get
    End Property

    Friend Shared Function Create() As IInitializationContext
        Return New InitializationContext()
    End Function
End Class
