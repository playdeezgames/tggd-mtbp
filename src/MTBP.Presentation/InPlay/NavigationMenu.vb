Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class NavigationMenu
    Inherits PickerMenu

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource)
        MyBase.New(context, model, exitDialog, "Now What?")
    End Sub

    Protected Overrides ReadOnly Property Launchers As IEnumerable(Of LaunchDelegate)
        Get
            Return Enumerable.Empty(Of LaunchDelegate).
                Append(AddressOf ChooseMoveMenu).
                Append(AddressOf ChooseGroundMenu).
                Append(AddressOf ChooseInventoryMenu).
                Append(AddressOf ChooseFeatureMenu).
                Append(AddressOf ChooseStatusMenu).
                Append(AddressOf ChooseLookMenu).
                Append(AddressOf ChooseGameMenu)
        End Get
    End Property

    Private Function ChooseLookMenu(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(
            model.LookChoiceVisible,
            "Look",
            LookActivity.Launch(context, model, exitDialog))
    End Function

    Private Function ChooseStatusMenu(
                                     context As IDisplayContext,
                                     model As IWorldModel,
                                     exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(
            model.StatusChoiceVisible,
            "Status",
            StatusActivity.Launch(context, model, exitDialog))
    End Function

    Private Function ChooseGroundMenu(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(
            model.HasGroundItems,
            "Ground...",
            GroundMenu.Launch(context, model, exitDialog))
    End Function

    Private Function ChooseInventoryMenu(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(
            model.HasItems,
            "Inventory...",
            InventoryMenu.Launch(context, model, exitDialog))
    End Function

    Private Function ChooseFeatureMenu(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(
            model.HasFeatures,
            "Feature...",
            FeaturesMenu.Launch(context, model, exitDialog))
    End Function

    Private Shared Function ChooseMoveMenu(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(model.CanMove, "Move...", MoveMenu.Launch(context, model, exitDialog))
    End Function

    Private Shared Function ChooseGameMenu(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(True, "Game Menu", GameMenu.Launch(context, model, exitDialog))
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New NavigationMenu(Context, Model, ExitDialog)
    End Function

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As DialogSource
        Return Function() New NavigationMenu(context, model, exitDialog)
    End Function
End Class
