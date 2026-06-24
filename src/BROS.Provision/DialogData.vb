Public Class DialogData
    Public Property Message As String
    Public Property RequiredTags As New HashSet(Of String)(StringComparer.InvariantCultureIgnoreCase)
    Public Property AddedTags As New HashSet(Of String)(StringComparer.InvariantCultureIgnoreCase)
    Public Property RemovedTags As New HashSet(Of String)(StringComparer.InvariantCultureIgnoreCase)
End Class
