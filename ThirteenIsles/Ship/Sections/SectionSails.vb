Public Class SectionSails
    Inherits Section
    Public Overrides ReadOnly Property JobDescription As String
        Get
            Return "Sailor"
        End Get
    End Property
    Public Overrides ReadOnly Property JobSkill As Skill
        Get
            Return Skill.Sailing
        End Get
    End Property
    Public Overrides Function GetSection(ByVal param As String()) As Boolean
        For Each p In param
            Dim ps As String() = p.Split("=")
            Select Case ps(0).ToLower
                Case "type" : If ps(1) <> "sails" Then Return False
                Case Else : If MyBase.GetSectionBase(ps) = False Then Return False
            End Select
        Next
        Return True
    End Function

    Private Quality As SailQuality

    Public Shared Function Generate(ByVal quality As SailQuality, ByVal crewMax As Integer) As SectionSails
        Dim s As New SectionSails
        With s
            .Quality = quality
            .CrewMax = crewMax

            Dim p As String = ""
            Select Case .CrewMax
                Case Is <= 2 : p = "Small "
                Case 3 To 4 : p = "Medium "
                Case 5 To 6 : p = "Large "
                Case Is >= 7 : p = "Huge "
            End Select
            ._Name = "'" & .GenerateName & "' (" & p & .Quality.ToString & " Sails)"
            ._Weight = .CrewMax * 5
        End With
        Return s
    End Function
End Class

Public Enum SailQuality
    Crude = 0
    Basic
    Standard
    Improved
    Masterwork
    Ensorcelled
End Enum