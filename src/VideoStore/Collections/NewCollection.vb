﻿Imports Microsoft.Data.SqlClient
Imports VSData

Friend Module NewCollection
    Friend Sub Run(store As DataStore)
        Dim collectionName = AnsiConsole.Ask(Prompts.NewCollectionName, String.Empty)
        If String.IsNullOrWhiteSpace(collectionName) Then
            Return
        End If
        Dim command = store.Connection.CreateCommand
        command.CommandText = Commands.CollectionInsert
        command.Parameters.AddWithValue(Parameters.CollectionName, collectionName)
        command.ExecuteNonQuery()
    End Sub
End Module
