Imports TGGD.Persistence

Public Interface IBROSEntity
    Inherits IEntity
    ReadOnly Property World As IWorld
    Function HasNoun(noun As String) As Boolean
    Sub AddNouns(ParamArray nouns As String())
End Interface
