Imports TGGD.Processing

Public Interface IFeaturesModel
    Inherits IModel
    ReadOnly Property HasAny As Boolean
    ReadOnly Property All As IEnumerable(Of IFeatureModel)
End Interface
