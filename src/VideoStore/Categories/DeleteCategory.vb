﻿Imports Microsoft.Data.SqlClient
Imports VSData

Friend Module DeleteCategory
    Friend Sub Run(store As DataStore, categoryId As Integer)
        store.DeleteCategory(categoryId)
    End Sub
End Module
