Public Interface IDialog
    ReadOnly Property DialogId As Guid
    Property Message As String
    ReadOnly Property RequiredTags As IEnumerable(Of String)
    ReadOnly Property AddedTags As IEnumerable(Of String)
    ReadOnly Property RemovedTags As IEnumerable(Of String)
    Sub RequireTags(ParamArray tags As String())
    Sub AddTags(ParamArray tags As String())
    Sub RemoveTags(ParamArray tags As String())
End Interface
