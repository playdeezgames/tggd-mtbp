Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class NavigationDialog
    Inherits LauncherModelDialog

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog))
        MyBase.New(context, model, exitDialog, "Now What?")
    End Sub

    Protected Overrides ReadOnly Property Launchers As IEnumerable(Of LaunchDelgate)
        Get
            Return {
                    AddressOf ChooseMoveMenu,
                    AddressOf ChooseGameMenu
                }
        End Get
    End Property

    Private Shared Function ChooseMoveMenu(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog)) As IDialogChoice
        Return DialogChoice.Create(model.CanMove, "Move...", MoveMenuDialog.Launch(context, model, exitDialog))
    End Function

    Private Shared Function ChooseGameMenu(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog)) As IDialogChoice
        Return DialogChoice.Create(True, "Game Menu", GameMenuDialog.Launch(context, model, exitDialog))
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New NavigationDialog(Context, Model, ExitDialog)
    End Function

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As Func(Of IDialog)) As Func(Of IDialog)
        Return Function() New NavigationDialog(context, model, exitDialog)
    End Function
End Class
