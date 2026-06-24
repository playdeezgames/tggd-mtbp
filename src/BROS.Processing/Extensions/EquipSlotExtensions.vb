Imports System.Runtime.CompilerServices
Imports BROS.Persistence

Friend Module EquipSlotExtensions
    <Extension>
    Friend Function GetEquipSlotType(equipSlot As IEquipSlot) As String
        Return equipSlot.GetMetadata(Metadatas.EQUIP_SLOT_TYPE)
    End Function
    <Extension>
    Friend Sub SetEquipSlotType(equipSlot As IEquipSlot, equipSlotType As String)
        equipSlot.SetMetadata(Metadatas.EQUIP_SLOT_TYPE, equipSlotType)
    End Sub
    <Extension>
    Friend Function CanEquip(equipSlot As IEquipSlot, item As IItem) As Boolean
        If equipSlot.Item IsNot Nothing Then
            Return False
        End If
        Return item.IsEquippable(equipSlot.GetEquipSlotType())
    End Function
End Module
