Public Interface ITopic
    ReadOnly Property TopicId As Guid
    Property Message As String
    ReadOnly Property World As IWorld
End Interface
