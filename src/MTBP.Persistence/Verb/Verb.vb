Imports MTBP.Provision

Friend Class Verb
    Inherits MTBPEntity(Of VerbData)
    Implements IVerb

    Private Sub New(world As IWorld, data As WorldData, verbId As Guid)
        MyBase.New(world, data)
        Me.VerbId = verbId
    End Sub

    Public ReadOnly Property VerbId As Guid Implements IVerb.VerbId

    Protected Overrides ReadOnly Property Data As VerbData
        Get
            Return _data.Verbs(VerbId)
        End Get
    End Property

    Friend Shared Function Create(world As IWorld, data As WorldData, verbId As Guid) As IVerb
        Return New Verb(world, data, verbId)
    End Function
End Class
