Public Interface IVerbModel
    ReadOnly Property Text As String
    Sub Perform(featureModel As IFeatureModel)
End Interface
