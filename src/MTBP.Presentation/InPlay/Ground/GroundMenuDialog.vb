Imports MTBP.Processing
Imports TGGD.Presentation

Friend Class GroundMenuDialog
    Inherits PickerDialog

    Private Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource)
        MyBase.New(context, model, exitDialog, "Items on Ground:")
    End Sub

    Protected Overrides ReadOnly Property Launchers As IEnumerable(Of LaunchDelegate)
        Get
            Return Enumerable.Empty(Of LaunchDelegate).
                Append(AddressOf ChooseNeverMind).
                Concat(Model.GroundItems.Select(AddressOf ChooseItem))
        End Get
    End Property

    Private Function ChooseItem(itemModel As IInventoryItemModel) As LaunchDelegate
        Return Function(c, m, e) DialogChoice.Create(True, itemModel.Text, GroundItemMenuDialog.Launch(c, m, e, itemModel))
    End Function

    Private Function ChooseNeverMind(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As IDialogChoice
        Return DialogChoice.Create(True, "Never Mind", NeutralDialog.Launch(context, model, exitDialog))
    End Function

    Friend Shared Function Launch(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource) As DialogSource
        Return Function()
                   If model.HasGroundItems Then
                       Return New GroundMenuDialog(context, model, exitDialog)
                   End If
                   Return NeutralDialog.Launch(context, model, exitDialog).Invoke()
               End Function
    End Function

    Protected Overrides Function Relaunch() As IDialog
        Return New GroundMenuDialog(Context, Model, ExitDialog)
    End Function
End Class
