
Imports TGGD.Persistence

Public Interface IWorld
    Inherits IEntity
    Property Avatar As ICharacter
    Function Save(filename As String) As Task
    Function CreateLocation(Optional initializer As Action(Of ILocation) = Nothing) As ILocation
    ReadOnly Property Messages As IEnumerable(Of IMessage)
    Sub AddMessage(text As String, Optional mood As String = Nothing, Optional newLine As Boolean = True)
    Sub ClearMessages()
End Interface
