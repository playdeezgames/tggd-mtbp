Public Interface IBaseInventory
    Sub AddPrepositions(ParamArray prepositions As String())
    Function HasPreposition(preposition As String) As Boolean
    ReadOnly Property DisplayPreposition As String
End Interface
