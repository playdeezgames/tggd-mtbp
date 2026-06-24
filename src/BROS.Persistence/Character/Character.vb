Imports BROS.Provision

Friend Class Character
    Inherits InventoryEntity(Of CharacterData)
    Implements ICharacter

    Public Sub New(world As IWorld, data As WorldData, characterId As Guid)
        MyBase.New(world, data)
        Me.CharacterId = characterId
    End Sub

    Public ReadOnly Property CharacterId As Guid Implements ICharacter.CharacterId

    Public Property Location As ILocation Implements ICharacter.Location
        Get
            Return Persistence.Location.Create(World, _data, Data.LocationId)
        End Get
        Set(value As ILocation)
            If Location.LocationId <> value.LocationId Then
                _data.Locations(Location.LocationId).CharacterIds.Remove(CharacterId)
                Data.LocationId = value.LocationId
                _data.Locations(Location.LocationId).CharacterIds.Add(CharacterId)
            End If
        End Set
    End Property

    Protected Overrides ReadOnly Property Data As CharacterData
        Get
            Return _data.Characters(CharacterId)
        End Get
    End Property

    Public ReadOnly Property EquipSlots As IEnumerable(Of IEquipSlot) Implements ICharacter.EquipSlots
        Get
            Return Data.EquipSlotIds.Select(Function(x) EquipSlot.Create(World, _data, x))
        End Get
    End Property

    Private ReadOnly Property Dialogs As IEnumerable(Of IDialog)
        Get
            Return Data.DialogIds.Select(Function(x) Dialog.Create(Me, _data, x))
        End Get
    End Property

    Public ReadOnly Property CurrentDialog As IDialog Implements ICharacter.CurrentDialog
        Get
            Return Dialogs.FirstOrDefault(Function(x) x.RequiredTags.All(Function(y) Me.HasTag(y)))
        End Get
    End Property

    Public ReadOnly Property Topics As IEnumerable(Of String) Implements ICharacter.Topics
        Get
            Return Data.TopicIds.Keys.Order(StringComparer.InvariantCultureIgnoreCase)
        End Get
    End Property

    Friend Shared Function Create(world As IWorld, data As WorldData, characterId As Guid) As ICharacter
        Return New Character(world, data, characterId)
    End Function

    Public Function CreateEquipSlot(initializer As Action(Of IEquipSlot)) As IEquipSlot Implements ICharacter.CreateEquipSlot
        Dim equipSlotId = Guid.NewGuid
        _data.EquipSlots(equipSlotId) = New EquipSlotData With
            {
                .CharacterId = CharacterId
            }
        Dim result = EquipSlot.Create(World, _data, equipSlotId)
        initializer?.Invoke(result)
        AddEquipSlot(result)
        Return result
    End Function

    Private Sub AddEquipSlot(equipSlot As IEquipSlot)
        Data.EquipSlotIds.Add(equipSlot.EquipSlotId)
    End Sub

    Public Function FindEquipSlotByNoun(noun As String) As IEquipSlot Implements ICharacter.FindEquipSlotByNoun
        Return EquipSlots.FirstOrDefault(Function(x) x.HasNoun(noun))
    End Function

    Public Sub AdvanceDialog() Implements ICharacter.AdvanceDialog
        Dim dialog = CurrentDialog
        If dialog Is Nothing Then
            Return
        End If
        SetTags(dialog.AddedTags.ToArray)
        ClearTags(dialog.RemovedTags.ToArray)
    End Sub

    Public Function CreateDialog(Optional initializer As Action(Of IDialog) = Nothing) As IDialog Implements ICharacter.CreateDialog
        Dim dialogId = Guid.NewGuid
        _data.Dialogs(dialogId) = New DialogData
        Data.DialogIds.Add(dialogId)
        Dim result As IDialog = Dialog.Create(Me, _data, dialogId)
        initializer?.Invoke(result)
        Return result
    End Function

    Public Function FindTopicByNoun(noun As String) As ITopic Implements ICharacter.FindTopicByNoun
        Dim topicId As Guid
        If Data.TopicIds.TryGetValue(noun, topicId) Then
            Return Topic.Create(World, _data, topicId)
        End If
        Return Nothing
    End Function

    Public Function CreateTopic(topicName As String, Optional initializer As Action(Of ITopic) = Nothing) As ITopic Implements ICharacter.CreateTopic
        Dim topicId = Guid.NewGuid
        _data.Topics(topicId) = New TopicData
        Data.TopicIds(topicName) = topicId
        Dim result = Topic.Create(World, _data, topicId)
        initializer?.Invoke(result)
        Return result
    End Function
End Class
