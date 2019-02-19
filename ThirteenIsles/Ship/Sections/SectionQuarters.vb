Public Class SectionQuarters
    Inherits Section
    Public Overrides ReadOnly Property JobDescription As String
        Get
            Return Nothing
        End Get
    End Property
    Public Overrides Function GetSection(ByVal param As String()) As Boolean
        For Each p In param
            Dim ps As String() = p.Split("=")
            Select Case ps(0).ToLower
                Case "type" : If ps(1) <> "quarters" Then Return False
            End Select
        Next
        Return True
    End Function
End Class
