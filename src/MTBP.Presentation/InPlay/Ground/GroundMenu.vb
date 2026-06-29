Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class GroundMenu
    Inherits PickerMenu

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource)
        MyBase.New(context, model, exitDialog, "Items on Ground:")
    End Sub

    Protected Overrides ReadOnly Property Launchers As IEnumerable(Of LaunchDelegate)
        Get
            Return Enumerable.Empty(Of LaunchDelegate).
                Append(AddressOf ChooseNeverMind).
                Concat(Model.Ground.Items.Select(AddressOf ChooseItem))
        End Get
    End Property

    Private Function ChooseItem(itemModel As IItemModel) As LaunchDelegate
        Return Function(c, m, e) DialogChoice.Create(True, itemModel.Text, GroundItemMenu.Launch(c, m, e, itemModel))
    End Function

    Private Function ChooseNeverMind(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(True, "Never Mind", NeutralActivity.Launch(context, model, exitDialog))
    End Function

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As DialogSource
        Return Function()
                   If model.Ground.HasItems Then
                       Return New GroundMenu(context, model, exitDialog)
                   End If
                   Return NeutralActivity.Launch(context, model, exitDialog).Invoke()
               End Function
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New GroundMenu(Context, Model, ExitDialog)
    End Function
End Class
