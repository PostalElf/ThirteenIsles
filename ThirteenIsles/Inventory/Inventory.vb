Public Class Inventory
    Public Sub New(ByVal maxCargo As Integer)
        SizeMax = maxCargo
    End Sub

    Private BaseList As New List(Of Item)
    Private Function Contains(ByVal i As Item) As Item
        For Each Item In BaseList
            If Item = i Then Return Item
        Next
        Return Nothing
    End Function
    Public Function Add(ByVal i As Item) As String
        If i.Qty + Size > SizeMax Then Return "Insufficient cargo space."

        Dim item As Item = Contains(i)
        If item Is Nothing Then
            BaseList.Add(i)
        Else
            item.Qty += i.Qty
        End If
        Return Nothing
    End Function
    Public Function Remove(ByVal i As Item) As String
        Dim item As Item = Contains(i)
        If item Is Nothing Then Return i.Name & " item not found."
        If item.Qty < i.Qty Then Return i.Name & " has only " & item.Qty & " (tried to remove " & i.Qty & ")."

        item.Qty -= i.Qty
        If item.Qty = 0 Then BaseList.Remove(item)
        Return Nothing
    End Function
    Public Function Fix(ByVal i As Item, ByVal qty As Integer) As String
        Dim item As Item = Contains(i)
        If item Is Nothing Then Return i.Name & " not found."

        item.Qty = qty
        Return Nothing
    End Function

    Public SizeMax As Integer
    Public ReadOnly Property Size As Integer
        Get
            Dim total As Integer = 0
            For Each i In BaseList
                total += i.Qty
            Next
            Return total
        End Get
    End Property

    Public Function ConsoleReport(ByVal ind As Integer) As String
        Dim total As String = ""
        For Each Item In BaseList
            total &= vbIndent(ind) & "x" & Item.Qty & " "
            total &= Item.Name
            If Item.Modifier <> "" Then total &= " (" & Item.Modifier & ")"
            total &= vbCrLf
        Next
        Return total
    End Function
End Class
