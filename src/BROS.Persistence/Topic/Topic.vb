Imports BROS.Provision

Friend Class Topic
    Implements ITopic

    Private Sub New(world As IWorld, data As WorldData, topicId As Guid)
        Me.World = world
        Me._data = data
        Me.TopicId = topicId
    End Sub

    Private ReadOnly Property Data As TopicData
        Get
            Return _data.Topics(TopicId)
        End Get
    End Property

    Public ReadOnly Property TopicId As Guid Implements ITopic.TopicId

    Public Property Message As String Implements ITopic.Message
        Get
            Return Data.Message
        End Get
        Set(value As String)
            Data.Message = value
        End Set
    End Property

    Public ReadOnly Property World As IWorld Implements ITopic.World
    Private ReadOnly _data As WorldData

    Friend Shared Function Create(world As IWorld, data As WorldData, topicId As Guid) As ITopic
        Return New Topic(world, data, topicId)
    End Function
End Class
