Imports BROS.Persistence

Friend Module AskCommandProcessor
    Friend Function Process(world As IWorld, tokens As Queue(Of String)) As CommandProcessorResult
        If tokens.Count <> 3 Then
            Return CommandProcessorResult.Invalid
        End If
        Dim characterNoun = tokens.Dequeue
        Dim character = world.Avatar
        If character.IsDead Then
            character.AddMessage("Dead people don't ask things.")
            Return CommandProcessorResult.Processed
        End If
        Dim location = character.Location
        Dim target = location.FindCharacterByNoun(characterNoun)
        If target Is Nothing Then
            character.AddMessage($"{character.GetName} does not see anyone going by `{characterNoun}` here.")
            Return CommandProcessorResult.Processed
        End If
        Dim preposition = tokens.Dequeue
        If Not preposition.Equals(Prepositions.ABOUT, StringComparison.InvariantCultureIgnoreCase) Then
            Return CommandProcessorResult.Invalid
        End If
        Dim topicNoun = tokens.Single
        If topicNoun.Equals(Topics.TOPICS, StringComparison.InvariantCultureIgnoreCase) Then
            Return AskAboutTopics(character, target)
        End If
        Dim topic As ITopic = target.FindTopicByNoun(topicNoun)
        If topic Is Nothing Then
            character.AddMessage($"{target.GetName} has nothing to say about {topicNoun}.")
            Return CommandProcessorResult.Processed
        End If
        character.AddMessage(topic.Message)
        Return CommandProcessorResult.Processed
    End Function

    Private Function AskAboutTopics(character As ICharacter, target As ICharacter) As CommandProcessorResult
        Dim topics = target.Topics
        If Not topics.Any Then
            character.AddMessage($"{target.GetName} has nothing to talk about.")
            Return CommandProcessorResult.Processed
        End If
        character.AddMessage($"{target.GetName()} will talk about: {String.Join(", ", topics)}.")
        Return CommandProcessorResult.Processed
    End Function
End Module
