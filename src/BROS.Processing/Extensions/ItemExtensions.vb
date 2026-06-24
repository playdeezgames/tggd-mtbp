Imports System.Runtime.CompilerServices
Imports BROS.Persistence

Friend Module ItemExtensions
    <Extension>
    Friend Function IsTakeable(item As IItem) As Boolean
        Return Not item.HasTag(Tags.LUREABLE)
    End Function
    Private Function GetEquippableTag(equipSlotType As String) As String
        Return $"equippable-{equipSlotType}"
    End Function
    <Extension>
    Friend Function IsEquippable(item As IItem, equipSlotType As String) As Boolean
        Return item.HasTag(GetEquippableTag(equipSlotType))
    End Function
    <Extension>
    Friend Sub SetEquippable(item As IItem, equipSlotType As String)
        item.SetTag(GetEquippableTag(equipSlotType))
    End Sub
    <Extension>
    Friend Function IsEdible(item As IItem) As Boolean
        Return item.HasTag(Tags.EDIBLE)
    End Function
    <Extension>
    Friend Sub SetEdible(item As IItem)
        item.SetTag(Tags.EDIBLE)
    End Sub
End Module
