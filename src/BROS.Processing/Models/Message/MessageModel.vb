Imports BROS.Persistence

Friend Class MessageModel
    Implements IMessageModel
    Private ReadOnly message As IMessage
    Private Sub New(message As IMessage)
        Me.message = message
    End Sub

    Public ReadOnly Property Text As String Implements IMessageModel.Text
        Get
            Return message.Text
        End Get
    End Property

    Public ReadOnly Property Mood As String Implements IMessageModel.Mood
        Get
            Return message.Mood
        End Get
    End Property

    Public ReadOnly Property NewLine As Boolean Implements IMessageModel.NewLine
        Get
            Return message.NewLine
        End Get
    End Property

    Friend Shared Function FromMessage(message As IMessage) As IMessageModel
        Return New MessageModel(message)
    End Function
End Class
