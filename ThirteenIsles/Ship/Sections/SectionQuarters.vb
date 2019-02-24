Public Class SectionQuarters
    Inherits Section
    Public Overrides ReadOnly Property JobDescription As String
        Get
            Return Nothing
        End Get
    End Property
    Public Overrides ReadOnly Property JobSkill As Skill
        Get
            Return Nothing
        End Get
    End Property
    Public Overrides Function GetSection(ByVal param As String()) As Boolean
        For Each p In param
            Dim ps As String() = p.Split("=")
            Select Case ps(0).ToLower
                Case "type" : If ps(1).ToLower <> "quarters" Then Return False
                Case "addable" : If ps(1).ToLower <> Me.Race.ToString.ToLower Then Return False
                Case Else : If MyBase.GetSectionBase(ps) = False Then Return False
            End Select
        Next
        Return True
    End Function

    Private Race As Race

    Public Shared Function Generate(ByVal race As Race, ByVal crewMax As Integer) As SectionQuarters
        Dim q As New SectionQuarters
        With q
            .Race = race
            .CrewMax = crewMax

            ._Name = "'" & .GenerateName() & "'"
            ._Weight = .CrewMax * 5
        End With
        Return q
    End Function
    Private Function NamePrefix() As String
        Select Case CrewMax
            Case Is <= 2 : Return "Private"
            Case 3 To 4 : Return "Standard"
            Case 5 To 6 : Return "Medium"
            Case 7 To 8 : Return "Large"
            Case Is >= 9 : Return "Huge"
            Case Else : Throw New Exception("Invalid crewmax size.")
        End Select
    End Function
    Public Overrides ReadOnly Property Name As String
        Get
            Dim total As String = _Name & " (" & NamePrefix() & " " & Race.ToString & " Quarters"
            If Quadrant Is Nothing = False Then total &= " - " & Quadrant.Facing.ToString & ")" Else total &= ")"
            Return total
        End Get
    End Property
    Protected Overrides Function Addable(ByVal Crew As Crew) As Boolean
        If Crew.Race <> Race Then Return False
        If MyBase.Addable(Crew) = False Then Return False
        Return True
    End Function
End Class
