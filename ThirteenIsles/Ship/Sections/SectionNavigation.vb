Public Class SectionNavigation
    Inherits Section
    Public Overrides ReadOnly Property JobDescription As String
        Get
            Return "Navigator"
        End Get
    End Property
    Public Overrides ReadOnly Property JobSkill As Skill
        Get
            Return Skill.Navigating
        End Get
    End Property
    Public Overrides Function GetSection(ByVal param As String()) As Boolean
        For Each p In param
            Dim ps As String() = p.Split("=")
            Select Case ps(0).ToLower
                Case "type" : If ps(1) <> "navigation" AndAlso ps(1) <> "nav" Then Return False
                Case Else : If MyBase.GetSectionBase(ps) = False Then Return False
            End Select
        Next
        Return True
    End Function

    Public Overrides ReadOnly Property NameFull As String
        Get
            Dim total As String = _Name & " (Navigation"
            If Quadrant Is Nothing = False Then total &= " - " & Quadrant.Facing.ToString & ")" Else total &= ")"
            Return total
        End Get
    End Property
    Protected Overrides Function ConsoleReportBrief(Optional ByVal colonPosition As Integer = 0) As String

    End Function
End Class
