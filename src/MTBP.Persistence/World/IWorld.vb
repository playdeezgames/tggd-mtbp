
Imports TGGD.Persistence

Public Interface IWorld
    Inherits IEntity
    Function Save(filename As String) As Task
    Function CreateLocation(Optional initializer As Action(Of ILocation) = Nothing) As ILocation
    Property Avatar As ICharacter
    ReadOnly Property Messages As IEnumerable(Of String)
    Sub AddMessage(message As String)
    Sub ClearMessages()
End Interface
