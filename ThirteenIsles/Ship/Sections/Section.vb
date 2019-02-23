Public MustInherit Class Section
    Implements ShipAssignable
    Protected _Name As String
    Public ReadOnly Property Name As String
        Get
            Return _Name & " (" & Quadrant.facing.tostring & ")"
        End Get
    End Property
    Private Shared Names As New List(Of String)
    Protected Function GenerateName() As String
        If Names.Count = 0 Then Names = IO.ImportTextList("data/sectionNames.txt")
        Return GrabRandom(Of String)(Names)
    End Function

    Public Quadrant As Quadrant
    Protected _Weight As Integer
    Public ReadOnly Property Weight As Integer
        Get
            Return _Weight
        End Get
    End Property

    Public MustOverride ReadOnly Property JobDescription As String
    Public MustOverride ReadOnly Property JobSkill As Skill
    Public MustOverride Function GetSection(ByVal param As String()) As Boolean
    Protected Function GetSectionBase(ByVal ps As String()) As Boolean
        Select Case ps(0).ToLower
            Case "addable"
                If ps(1).ToLower = "false" AndAlso Addable(New Crew) = True Then Return False
                If ps(1).ToLower = "true" AndAlso Addable(New Crew) = False Then Return False
            Case "noquarters", "noquarter"
                If ps(1).ToLower = "true" AndAlso TypeOf Me Is SectionQuarters Then Return False
        End Select
        Return True
    End Function
    Public Function GetCrews(ByVal param As String()) As List(Of Crew)
        Dim total As New List(Of Crew)
        For Each c In Crews
            If c.getcrew(param) = True Then total.Add(c)
        Next
        Return total
    End Function

    Private Property Crews As New List(Of Crew) Implements ShipAssignable.Crews
    Protected CrewMax As Integer
    Private Sub Add(ByVal crew As Crew) Implements ShipAssignable.Add
        If Crews.Contains(crew) Then Exit Sub
        Crews.Add(crew)
    End Sub
    Protected Overridable Function Addable(ByVal crew As Crew) As Boolean Implements ShipAssignable.Addable
        If Quadrant.Addable(crew) = False Then Return False
        If Crews.Count + 1 > CrewMax Then Return False
        Return True
    End Function
    Private Sub Remove(ByVal crew As Crew) Implements ShipAssignable.Remove
        If Crews.Contains(crew) = False Then Exit Sub
        Crews.Remove(crew)
    End Sub

    Public Overrides Function ToString() As String
        Return _Name
    End Function
End Class
