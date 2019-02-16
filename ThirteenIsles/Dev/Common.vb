<DebuggerStepThrough()>
Module Common
    Public Rng As New Random(4)
    Public Function FudgeRoll(Optional ByVal dice As Integer = 4) As Integer
        Dim total As Integer = 0
        For n = 1 To dice
            Dim roll As Integer = Rng.Next(1, 4)
            Select Case roll
                Case 1 : total -= 1
                Case 2 : total += 1
            End Select
        Next
        Return total
    End Function

    Public Function GrabRandom(Of T)(ByRef sourceList As List(Of T)) As T
        Dim roll As Integer = Rng.Next(sourceList.Count)
        GrabRandom = sourceList(roll)
        sourceList.RemoveAt(roll)
    End Function
    Public Function GetRandom(Of T)(ByVal sourceList As List(Of T)) As T
        Dim roll As Integer = Rng.Next(sourceList.Count)
        Return sourceList(roll)
    End Function

    Public Function StringToEnum(Of T)(ByVal str As String) As T
        For Each e In [Enum].GetValues(GetType(T))
            If e.ToString = str Then Return e
        Next
        Throw New Exception("StringToEnum: no match found for " & str)
    End Function
    Public Function ListToCommaString(ByVal str As String()) As String
        Dim total As String = ""
        For n = 0 To str.Count - 1
            total &= str(n)
            If n <> str.Count - 1 Then total &= ", "
        Next
        Return total
    End Function
    Public Function ListToCommaString(ByVal str As List(Of String), Optional ByVal lastConjunction As String = ", ") As String
        Dim total As String = ""
        For n = 0 To str.Count - 1
            total &= str(n)
            If n = str.Count - 2 Then
                total &= lastConjunction
            ElseIf n <> str.Count - 1 Then
                total &= ", "
            End If
        Next
        Return total
    End Function
    Public Function CommaStringToList(ByVal str As String) As List(Of String)
        Dim total As New List(Of String)
        Dim ln As String() = str.Split(",")
        For Each l In ln
            total.Add(l.Trim)
        Next
        Return total
    End Function

    Public Function ProgressBar(ByVal length As Integer, ByVal percentage As Integer, _
                                Optional ByVal bookend As Char = "|"c, Optional ByVal prog As Char = "*"c, Optional ByVal empty As Char = "-"c, Optional ByVal full As Char = "+"c) As String
        Dim progstep As Integer = 100 / length

        Dim total As String = bookend
        If percentage = 100 Then
            For n = 1 To length
                total &= full
            Next
        Else
            While percentage > 0
                total &= prog
                percentage -= progstep
                length -= 1
            End While
            For n = 1 To length
                total &= empty
            Next
        End If
        total &= bookend
        Return total
    End Function

    Public Function vbIndent(ByVal i As Integer) As String
        Const space As String = "  "
        Dim total As String = ""
        For n = 1 To i
            total &= space
        Next
        Return total
    End Function
    Public Function vbTabb(ByVal word As String, ByVal totalLength As Integer)
        Dim spaceCount As Integer = totalLength - word.Length
        Dim spaces As String = ""
        For n = 1 To spaceCount
            spaces &= " "
        Next
        Return word & spaces
    End Function
End Module
