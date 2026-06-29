Imports MTBP.Processing
Imports TGGD.Presentation

Friend MustInherit Class PickerMenu
    Inherits ExitableModelDialog(Of IDisplayContext, IWorldModel)
    Friend Delegate Function LaunchDelegate(
                                     context As IDisplayContext,
                                     model As IWorldModel,
                                     exitDialog As DialogSource) As IDialogChoice

    ReadOnly Property PromptText As String

    Protected Sub New(context As IDisplayContext, model As IWorldModel, exitDialog As DialogSource, promptText As String)
        MyBase.New(context, model, exitDialog)
        Me.PromptText = promptText
    End Sub

    Protected MustOverride ReadOnly Property Launchers As IEnumerable(Of LaunchDelegate)

    Public Overrides Function Run() As IDialogPrompt
        For Each message In Model.Messages
            Context.Render(message)
        Next
        Return DialogPrompt.CreateChoicePrompt(
            PromptText,
            Launchers.Select(Function(x) x(Context, Model, ExitDialog)).ToArray)
    End Function
End Class
