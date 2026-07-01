Imports System.Runtime.CompilerServices
Imports MTBP.Persistence

Friend Module FeatureExtensions
    <Extension>
    Friend Function IsAlcove(feature As IFeature) As Boolean
        Return feature.HasTag(Tags.ALCOVE)
    End Function
End Module
