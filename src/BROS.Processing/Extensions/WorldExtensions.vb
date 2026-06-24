Imports System.Runtime.CompilerServices
Imports BROS.Persistence

Friend Module WorldExtensions
    <Extension>
    Friend Sub Abandon(world As IWorld)
        Dim isQuittable = world.HasTag(Tags.QUITTABLE)
        world.Clear()
        If isQuittable Then
            world.SetTag(Tags.QUITTABLE)
        End If
    End Sub
End Module
