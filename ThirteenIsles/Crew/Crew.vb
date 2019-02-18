Public Class Crew
    Public Shared Function Generate(Optional ByVal race As Race = 0) As Crew
        Dim crew As New Crew
        With crew
            If race <> 0 Then
                .Race = race
            Else
                Dim raceLength As Integer = [Enum].GetValues(GetType(Race)).Length + 1
                .Race = Rng.Next(1, raceLength)
            End If
            .Name = CrewGenerator.GetName(race)
            .Traits.Add(CrewGenerator.GetTrait(race))
        End With
        Return crew
    End Function

    Private Race As Race
    Private Name As String
    Private Traits As New Traits
    Private Skills As New Skills
    Public Sub SkillImprove(ByVal skill As Skill, ByVal amt As Integer)
        Skills.Improve(skill, amt)
    End Sub
    Public Sub SkillTrain(ByVal skill As Skill)
        Skills.Train(skill)
    End Sub

    Public Quarters As Section
    Public Job As Section
    Public ReadOnly Property Skill(ByVal s As Skill) As Integer
        Get
            Return Skills(s) + Traits.GetSkillBonus(s)
        End Get
    End Property

    Public Function ConsoleReport(Optional ByVal id As Integer = 0) As String
        Dim total As String = vbIndent(id) & Name & ", " & Race.ToString
        If Job Is Nothing = False Then total &= " " & Job.JobDescription
        total &= vbCrLf

        total &= vbIndent(id + 1) & "Traits:" & vbCrLf
        total &= Traits.consolereport(id + 2)

        total &= vbIndent(id + 1) & "Skills:" & vbCrLf
        For Each s In [Enum].GetValues(GetType(Skill))
            total &= vbIndent(id + 2) & vbTabb(s.ToString & ":", 16)
            total &= Skill(s) & " " & ProgressBar(5, Skills.XPPercentage(s)) & vbCrLf
        Next
        Return total
    End Function
End Class

Public Enum Race
    Mortal = 1
    Seatouched
    Ghost
End Enum

Public Class CrewGenerator
    Shared Sub New()
        For Each e In [Enum].GetValues(GetType(Race))
            Prefixes.Add(e, New List(Of String))
            PopulatePrefixes(e)
            Suffixes.Add(e, New List(Of String))
            PopulateSuffixes(e)
        Next
    End Sub

    Private Shared Prefixes As New Dictionary(Of Race, List(Of String))
    Private Shared Sub PopulatePrefixes(ByVal race As Race)
        Dim path As String = "data/" & race.ToString.ToLower & "Prefixes.txt"
        Prefixes(race).AddRange(IO.ImportTextList(path))
    End Sub
    Private Shared Suffixes As New Dictionary(Of Race, List(Of String))
    Private Shared Sub PopulateSuffixes(ByVal race As Race)
        Dim path As String = "data/" & race.ToString.ToLower & "Suffixes.txt"
        Suffixes(race).AddRange(IO.ImportTextList(path))
    End Sub
    Public Shared Function GetName(ByVal race As Race)
        If Prefixes(race).Count = 0 Then PopulatePrefixes(race)
        If Suffixes(race).Count = 0 Then PopulateSuffixes(race)
        Dim name As String = GrabRandom(Prefixes(race)) & " " & GrabRandom(Suffixes(race))
        Return name
    End Function

    Public Shared Function GetTrait(ByVal race As Race) As Trait
        Dim roll As Integer = Rng.Next(1, 4)
        Select Case race
            Case ThirteenIsles.Race.Mortal
                Select Case roll
                    Case 1 : Return Trait.Flamekissed
                    Case 2 : Return Trait.Windsung
                    Case 3 : Return Trait.Woodwoden
                End Select
            Case ThirteenIsles.Race.Seatouched
                Select Case roll
                    Case 1 : Return Trait.Saltborn
                    Case 2 : Return Trait.Woodwoden
                    Case 3 : Return Trait.Cultist
                End Select
            Case ThirteenIsles.Race.Ghost
                Select Case roll
                    Case 1 : Return Trait.Saltborn
                    Case 2 : Return Trait.Windsung
                    Case 3 : Return Trait.Stormtossed
                End Select
        End Select
        Throw New Exception("Unexpected outcome in CrewGenerator.GetTrait")
    End Function
End Class