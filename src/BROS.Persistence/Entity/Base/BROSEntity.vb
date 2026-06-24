Imports BROS.Provision
Imports TGGD.Persistence

Friend MustInherit Class BROSEntity(Of TData As BROSEntityData)
    Inherits Entity(Of TData)
    Implements IBROSEntity

    Public ReadOnly Property World As IWorld Implements IBROSEntity.World
    Protected _data As WorldData

    Protected Sub New(world As IWorld, data As WorldData)
        Me.World = world
        Me._data = data
    End Sub

    Public Function HasNoun(noun As String) As Boolean Implements IBROSEntity.HasNoun
        Return Data.Nouns.Any(Function(x) x.Equals(noun, StringComparison.InvariantCultureIgnoreCase))
    End Function

    Public Sub AddNouns(ParamArray nouns() As String) Implements IBROSEntity.AddNouns
        For Each noun In nouns
            Data.Nouns.Add(noun)
        Next
    End Sub
End Class
