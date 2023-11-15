﻿Imports Microsoft.Data.SqlClient
Friend Module CategoryReport
    Friend Sub Run(connection As SqlConnection)
        Dim command = connection.CreateCommand
        command.CommandText = Commands.CategoryReport
        Utility.ExportHtml(
            "Category Report",
            "CategoryReport",
            New String() {
                "Category Name",
                "Abbreviation",
                "Media Count"
            },
            command,
            New Func(Of SqlDataReader, Object)() {
                Function(reader) reader.GetString(0),
                Function(reader) reader.GetString(1),
                Function(reader) reader.GetInt32(2)
            })
    End Sub
End Module
