Imports System.Runtime.CompilerServices
Imports MTBP.Persistence

Friend Module ItemExtensions
    <Extension>
    Friend Function IsRing(item As IItem) As Boolean
        Return item.HasTag(Tags.RING)
    End Function
End Module
