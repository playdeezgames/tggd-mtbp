Imports TGGD.Processing

Public MustInherit Class ExitableModelDialog(Of TContext As IDisplayContext, TModel As IModel)
    Inherits BaseModelDialog(Of TContext, TModel)

    Protected ReadOnly ExitDialog As DialogSource

    Protected Sub New(
                     context As TContext,
                     model As TModel,
                     exitDialog As DialogSource)
        MyBase.New(context, model)
        Me.ExitDialog = exitDialog
    End Sub
End Class
