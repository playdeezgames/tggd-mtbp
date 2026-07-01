Public Delegate Sub VerbInitializer(verb As IVerb)
Public Interface IVerb
    Inherits IMTBPEntity
    ReadOnly Property VerbId As Guid
End Interface
