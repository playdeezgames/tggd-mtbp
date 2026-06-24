Imports BROS.Persistence

Friend Class InitializationContext
    Implements IInitializationContext
    Private Sub New(world As IWorld)
        Me.World = world
    End Sub

    Public ReadOnly Property World As IWorld Implements IInitializationContext.World

    Private _portalDestination As ILocation = Nothing

    Public Property PortalDestination As ILocation Implements IInitializationContext.PortalDestination
        Get
            Return _portalDestination
        End Get
        Set(value As ILocation)
#If DEBUG Then
            _portalDestination = value
#End If
        End Set
    End Property

    Public Property SouthTownLocation As ILocation Implements IInitializationContext.SouthTownLocation

    Public Property SouthWestTownLocation As ILocation Implements IInitializationContext.SouthWestTownLocation

    Public Property FrontYard As ILocation Implements IInitializationContext.FrontYard

    Public Property TownCenterLocation As ILocation Implements IInitializationContext.TownCenterLocation

    Public Property NorthTownLocation As ILocation Implements IInitializationContext.NorthTownLocation

    Public Property EastTownLocation As ILocation Implements IInitializationContext.EastTownLocation

    Public Property WestTownLocation As ILocation Implements IInitializationContext.WestTownLocation

    Friend Shared Function Create(world As IWorld) As IInitializationContext
        Return New InitializationContext(world)
    End Function
End Class
