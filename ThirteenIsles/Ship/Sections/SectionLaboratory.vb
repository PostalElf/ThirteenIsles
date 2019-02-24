Public Class SectionLaboratory
    Inherits Section
    Public Overrides ReadOnly Property JobDescription As String
        Get
            Return "Alchemist"
        End Get
    End Property
    Public Overrides ReadOnly Property JobSkill As Skill
        Get
            Return Skill.Alchemy
        End Get
    End Property
    Public Overrides Function GetSection(ByVal param As String()) As Boolean
        For Each p In param
            Dim ps As String() = p.Split("=")
            Select Case ps(0).ToLower
                Case "type" : If ps(1) <> "laboratory" AndAlso ps(1) <> "lab" Then Return False
                Case Else : If MyBase.GetSectionBase(ps) = False Then Return False
            End Select
        Next
        Return True
    End Function

    Public Overrides ReadOnly Property Name As String
        Get
            Dim total As String = _Name & " (Laboratory"
            If Quadrant Is Nothing = False Then total &= " - " & Quadrant.Facing.ToString & ")" Else total &= ")"
            Return total
        End Get
    End Property
End Class
