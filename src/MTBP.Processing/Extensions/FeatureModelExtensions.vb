Imports System.Runtime.CompilerServices
Imports MTBP.Persistence

Friend Module FeatureModelExtensions
    <Extension>
    Friend Function GetFeature(featureModel As IFeatureModel) As IFeature
        Return CType(featureModel, FeatureModel).feature
    End Function
End Module
