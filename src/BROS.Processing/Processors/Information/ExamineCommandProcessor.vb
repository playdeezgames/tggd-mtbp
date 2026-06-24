Imports BROS.Persistence

Friend Module ExamineCommandProcessor
    Friend Function Process(world As IWorld, tokens As Queue(Of String)) As CommandProcessorResult
        If tokens.Count = 0 Then
            Return CommandProcessorResult.Invalid
        End If
        Dim noun = tokens.Dequeue
        Dim character = world.Avatar
        If Character.IsDead Then
            Character.AddMessage("Dead people don't ask things.")
            Return CommandProcessorResult.Processed
        End If
        Select Case tokens.Count
            Case 0
                Return ProcessExamineDirect(character, noun)
            Case 2
                Dim preposition = tokens.Dequeue
                Dim containerNoun = tokens.Single
                Return ProcessExamineContainer(character, containerNoun, preposition, noun)
            Case Else
                Return CommandProcessorResult.Invalid
        End Select
    End Function

    Private Function ProcessExamineContainer(
                                            character As ICharacter,
                                            containerNoun As String,
                                            preposition As String,
                                            noun As String) As CommandProcessorResult
        Dim feature = character.Location.FindFeatureByNoun(containerNoun)
        If feature IsNot Nothing Then
            Return ProcessExamineContainerFeature(character, feature, preposition, noun)
        End If
        Return CommandProcessorResult.Invalid
    End Function

    Private Function ProcessExamineContainerFeature(character As ICharacter, feature As IFeature, preposition As String, noun As String) As CommandProcessorResult
        If Not feature.Inventory.HasPreposition(preposition) Then
            Return CommandProcessorResult.Invalid
        End If
        Return ProcessExamineItem(character, feature.Inventory.FindItemByNoun(noun), noun)
    End Function

    Private Function ProcessExamineItem(character As ICharacter, item As IItem, noun As String) As CommandProcessorResult
        If item Is Nothing Then
            character.AddMessage($"{character.GetName()} sees no `{noun}` here.")
            Return CommandProcessorResult.Processed
        End If
        character.DescribeItem(item)
        Return CommandProcessorResult.Processed
    End Function

    Private Function ProcessExamineDirect(character As ICharacter, noun As String) As CommandProcessorResult
        character.World.ClearMessages()
        Dim feature = character.Location.FindFeatureByNoun(noun)
        If feature IsNot Nothing Then
            Return ProcessExamineFeature(character, feature)
        End If
        Dim item = character.Inventory.FindItemByNoun(noun)
        If item IsNot Nothing Then
            Return ProcessExamineItem(character, item, noun)
        End If
        Dim equipSlot = character.FindEquipSlotByNoun(noun)
        If equipSlot IsNot Nothing Then
            Return ProcessExamineEquipSlot(character, equipSlot)
        End If
        Dim route = character.Location.FindRouteByDirection(noun)
        If route IsNot Nothing Then
            Return ProcessExamineRoute(character, route)
        End If
        Dim otherCharacter = character.Location.FindCharacterByNoun(noun)
        If otherCharacter IsNot Nothing Then
            Return ProcessExamineCharacter(character, otherCharacter)
        End If
        character.AddMessage($"{character.GetName()} sees no `{noun}` here.")
        Return CommandProcessorResult.Processed
    End Function

    Private Function ProcessExamineCharacter(character As ICharacter, otherCharacter As ICharacter) As CommandProcessorResult
        character.DescribeCharacter(otherCharacter)
        Return CommandProcessorResult.Processed
    End Function

    Private Function ProcessExamineRoute(character As ICharacter, route As IRoute) As CommandProcessorResult
        character.DescribeRoute(route)
        Return CommandProcessorResult.Processed
    End Function

    Private Function ProcessExamineEquipSlot(character As ICharacter, equipSlot As IEquipSlot) As CommandProcessorResult
        character.DescribeEquipSlot(equipSlot)
        Dim item = equipSlot.Item
        If item IsNot Nothing Then
            character.AddMessage($"{character.GetName}'s {equipSlot.GetName()} has {item.GetName()} {equipSlot.DisplayPreposition} it.")
        End If
        Return CommandProcessorResult.Processed
    End Function

    Private Function ProcessExamineFeature(character As ICharacter, feature As IFeature) As CommandProcessorResult
        character.DescribeFeature(feature)
        Dim items = feature.Inventory.Items
        If items.Any Then
            character.AddMessage($"Items {feature.Inventory.DisplayPreposition} {feature.GetName()} include {String.Join(", ", items.Select(Function(x) x.GetName()))}.")
        End If
        Return CommandProcessorResult.Processed
    End Function
End Module
