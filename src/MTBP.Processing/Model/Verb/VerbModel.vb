Imports MTBP.Persistence

Friend Class VerbModel
    Implements IVerbModel

    Private ReadOnly verb As IVerb

    Private Sub New(verb As IVerb)
        Me.verb = verb
    End Sub

    Public ReadOnly Property Text As String Implements IVerbModel.Text
        Get
            Return verb.GetName
        End Get
    End Property

    Public Sub Perform(featureModel As IFeatureModel) Implements IVerbModel.Perform
        verb.World.ClearMessages()
        Dim feature As IFeature = featureModel.GetFeature()
        feature.World.Avatar.PerformFeatureVerb(feature, verb)
    End Sub

    Friend Shared Function Create(verb As IVerb) As IVerbModel
        Return New VerbModel(verb)
    End Function
End Class
