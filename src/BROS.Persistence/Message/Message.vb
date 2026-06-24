Imports BROS.Provision

Friend Class Message
    Implements IMessage

    Private ReadOnly data As MessageData

    Private Sub New(data As MessageData)
        Me.data = data
    End Sub

    Public ReadOnly Property Text As String Implements IMessage.Text
        Get
            Return data.Text
        End Get
    End Property

    Public ReadOnly Property Mood As String Implements IMessage.Mood
        Get
            Return data.Mood
        End Get
    End Property

    Public ReadOnly Property NewLine As Boolean Implements IMessage.NewLine
        Get
            Return data.NewLine
        End Get
    End Property

    Friend Shared Function Create(data As MessageData) As IMessage
        Return New Message(data)
    End Function
End Class
