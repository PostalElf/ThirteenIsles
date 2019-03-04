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
    Private ReadOnly Property SpeedBase As Integer
        Get
            Dim baseSpeed As Double = 0
            Select Case Quality
                Case SailQuality.Crude : baseSpeed = 5
                Case SailQuality.Basic : baseSpeed = 10
                Case SailQuality.Standard : baseSpeed = 20
                Case SailQuality.Improved : baseSpeed = 25
                Case SailQuality.Masterwork : baseSpeed = 30
                Case SailQuality.Ensorcelled : baseSpeed = 40
            End Select
            Return baseSpeed
        End Get
    End Property
    Public ReadOnly Property SpeedTotal As Integer
        Get
            Dim total As Integer = 0
            For Each Crew In GetCrews({""})
                total += Crew.GetJobSkill
            Next
            total *= SpeedBase

            If GetCrews({""}).Count < CrewMax / 2 Then total /= 2
            Return CInt(total)
        End Get
    End Property

    Public Shared Function Generate(ByVal quality As SailQuality, ByVal crewMax As Integer) As SectionSails
        Dim s As New SectionSails
        With s
            .Quality = quality
            .CrewMax = crewMax

            ._Name = "'" & .GenerateName & "'"
            ._Weight = .CrewMax * 5
        End With
        Return s
    End Function
    Private ReadOnly Property NamePrefix As String
        Get
            Select Case CrewMax
                Case Is <= 2 : Return "Small"
                Case 3 To 4 : Return "Medium"
                Case 5 To 6 : Return "Large"
                Case Is >= 7 : Return "Huge"
                Case Else : Throw New Exception("Unexpected CrewMax.")
            End Select
        End Get
    End Property
    Public Overrides ReadOnly Property NameFull As String
        Get
            Dim total As String = _Name & " (" & Quality.ToString & " " & NamePrefix & " Sails"
            If Quadrant Is Nothing = False Then total &= " - " & Quadrant.Facing.ToString & ")" Else total &= ")"
            Return total
        End Get
    End Property
    Protected Overrides Function ConsoleReportBrief(Optional ByVal colonPosition As Integer = 0) As String
        Dim total As String = vbTabb(Name, colonPosition) & ":  "
        total &= "[" & GetCrews({""}).Count & "/" & CrewMax & "]"
        total &= " +" & SpeedTotal.ToString("00.0")
        total &= " (" & Quality.ToString & ")"
        Return total
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