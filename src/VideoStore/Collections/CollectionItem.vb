﻿Imports Microsoft.Data.SqlClient
Imports VSData

Friend Module CollectionItem
    Friend Sub Run(store As DataStore, collectionId As Integer)
        Do
            Dim command = store.Connection.CreateCommand
            command.CommandText = Commands.CollectionDetails
            command.Parameters.AddWithValue(Parameters.CollectionId, collectionId)
            Dim prompt As New SelectionPrompt(Of String) With {.Title = String.Empty}
            prompt.AddChoice(MenuItems.GoBack)
            prompt.AddChoice(MenuItems.ChangeName)
            Dim collectionName As String
            Using reader = command.ExecuteReader
                reader.Read()
                collectionName = reader.GetString(1)
                Dim mediaCount = reader.GetInt32(2)
                AnsiConsole.Clear()
                AnsiConsole.MarkupLine($"Id: {collectionId}")
                AnsiConsole.MarkupLine($"Name: {collectionName}")
                AnsiConsole.MarkupLine($"Media Count: {mediaCount}")
                If mediaCount = 0 Then
                    prompt.AddChoice(MenuItems.DeleteCollection)
                End If
            End Using
            Select Case AnsiConsole.Prompt(prompt)
                Case MenuItems.GoBack
                    Exit Do
                Case MenuItems.DeleteCollection
                    DeleteCollection.Run(store, collectionId)
                    Exit Do
                Case MenuItems.ChangeName
                    RunChangeName(store, collectionId, collectionName)
            End Select
        Loop
    End Sub
    Private Sub RunChangeName(store As DataStore, collectionId As Integer, collectionName As String)
        Dim newCollectionName = AnsiConsole.Ask(Prompts.NewCollectionName, collectionName)
        If newCollectionName = collectionName Then
            Return
        End If
        Dim command = store.Connection.CreateCommand
        command.CommandText = Commands.CollectionUpdateName
        command.Parameters.AddWithValue(Parameters.CollectionId, collectionId)
        command.Parameters.AddWithValue(Parameters.CollectionName, newCollectionName)
        command.ExecuteNonQuery()
    End Sub
End Module
