Public Interface ICharacter
    Inherits IMTBPEntity
    ReadOnly Property CharacterId As Guid
    Property Location As ILocation
End Interface
