Public Interface ICharacter
    Inherits IInventoryEntity
    ReadOnly Property CharacterId As Guid
    Property Location As ILocation
    Function CreateEquipSlot(initializer As Action(Of IEquipSlot)) As IEquipSlot
    Function FindEquipSlotByNoun(noun As String) As IEquipSlot
    ReadOnly Property EquipSlots As IEnumerable(Of IEquipSlot)
    ReadOnly Property CurrentDialog As IDialog
    Sub AdvanceDialog()
    Function CreateDialog(Optional initializer As Action(Of IDialog) = Nothing) As IDialog
    Function FindTopicByNoun(noun As String) As ITopic
    Function CreateTopic(
                        topicName As String,
                        Optional initializer As Action(Of ITopic) = Nothing) As ITopic
    ReadOnly Property Topics As IEnumerable(Of String)
End Interface
