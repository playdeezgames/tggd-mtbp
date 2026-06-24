Imports BROS.Provision

Friend Class Dialog
    Implements IDialog

    Private ReadOnly character As ICharacter
    Private ReadOnly _data As WorldData

    Private Sub New(character As ICharacter, data As WorldData, dialogId As Guid)
        Me.character = character
        Me._data = data
        Me.DialogId = dialogId
    End Sub

    Friend Shared Function Create(character As Character, data As WorldData, dialogId As Guid) As IDialog
        Return New Dialog(character, data, dialogId)
    End Function

    Public Sub RequireTags(ParamArray tags() As String) Implements IDialog.RequireTags
        For Each tag In tags
            Data.RequiredTags.Add(tag)
        Next
    End Sub

    Public Sub AddTags(ParamArray tags() As String) Implements IDialog.AddTags
        For Each tag In tags
            Data.AddedTags.Add(tag)
        Next
    End Sub

    Public Sub RemoveTags(ParamArray tags() As String) Implements IDialog.RemoveTags
        For Each tag In tags
            Data.RemovedTags.Add(tag)
        Next
    End Sub

    Private ReadOnly Property Data As DialogData
        Get
            Return _data.Dialogs(DialogId)
        End Get
    End Property

    Public ReadOnly Property DialogId As Guid Implements IDialog.DialogId

    Public Property Message As String Implements IDialog.Message
        Get
            Return Data.Message
        End Get
        Set(value As String)
            Data.Message = value
        End Set
    End Property

    Public ReadOnly Property RequiredTags As IEnumerable(Of String) Implements IDialog.RequiredTags
        Get
            Return Data.RequiredTags
        End Get
    End Property

    Public ReadOnly Property AddedTags As IEnumerable(Of String) Implements IDialog.AddedTags
        Get
            Return Data.AddedTags
        End Get
    End Property

    Public ReadOnly Property RemovedTags As IEnumerable(Of String) Implements IDialog.RemovedTags
        Get
            Return Data.RemovedTags
        End Get
    End Property
End Class
