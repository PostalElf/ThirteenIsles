Public Class Quadrant
    Public Sub New(ByVal _ship As Ship, ByVal _facing As Directions, ByVal _maxCrew As Integer, ByVal _maxGuns As Integer)
        Ship = _ship
        Facing = _facing
        CrewMax = _maxCrew
        GunsMax = _maxGuns
    End Sub

    Private Ship As Ship
    Public Facing As Directions
    Private GunsMax As Integer

    Private Sections As New List(Of Section)
    Public Function GetSections(ByVal param As String()) As List(Of Section)
        Dim total As New List(Of Section)
        For Each s In Sections
            If s.GetSection(param) = True Then total.Add(s)
        Next
        Return total
    End Function
    Public Function GetCrews(ByVal param As String()) As List(Of Crew)
        Dim total As New List(Of Crew)
        For Each s In Sections
            Dim ss As List(Of Crew) = s.getcrews(param)
            If ss Is Nothing = False AndAlso ss.Count > 0 Then total.AddRange(ss)
        Next
        Return total
    End Function
    Public Function Add(ByVal section As Section) As String
        Sections.Add(section)
        section.quadrant = Me
        Return Nothing
    End Function
    Public Function Remove(ByVal section As Section) As String
        If Sections.Contains(section) = False Then Return "Section not found in quadrant."
        Sections.Remove(section)
        Return Nothing
    End Function

    Private ReadOnly Property Crews As List(Of Crew)
        Get
            'only return crews in job
            Dim total As New List(Of Crew)
            For Each Section In Sections
                If Section.GetType = GetType(SectionQuarters) Then Continue For
                total.AddRange(CType(Section, ShipAssignable).Crews)
            Next
            Return total
        End Get
    End Property
    Private CrewMax As Integer
    Public Function Addable(ByVal crew As Crew) As Boolean
        If Crews.Count + 1 > CrewMax Then Return False Else Return True
    End Function

    Private DamageTaken As Integer
    Private DamageMax As Integer
    Private ReadOnly Property DamagePercentage As Integer
        Get
            If DamageTaken = 0 Then Return 0
            If DamageMax = 0 Then Return 0
            Return DamageTaken / DamageMax * 100
        End Get
    End Property

    Public Function ConsoleReportBrief() As String
        Dim total As String = vbTabb(Facing.ToString & ":", 12)
        total &= Crews.Count & "/" & CrewMax
        total &= "  "
        total &= ProgressBar(10, DamagePercentage)
        Return total
    End Function
    Public Function ConsoleReport(ByVal ind As Integer) As String
        Dim total As String = vbIndent(ind) & Facing.ToString & ":" & vbCrLf

        total &= ParseConsoleReportBrief(GetCrews({""}), ind + 1, "Crew")
        total &= ParseConsoleReportBrief(GetSections({"type=gun"}), ind + 1, "Guns")
        total &= ParseConsoleReportBrief(GetSections({"type=sails"}), ind + 1, "Sails")
        total &= ParseConsoleReportBrief(GetSections({"type=quarters"}), ind + 1, "Quarters")
        Return total
    End Function
    Private Function ParseConsoleReportBrief(ByVal l As IEnumerable(Of Object), ByVal ind As Integer, ByVal title As String) As String
        Dim total As String = ""
        Dim count As Integer = 0
        For Each Item In l
            If TypeOf Item Is ConsoleReportBriefable = False Then Continue For
            Dim i As ConsoleReportBriefable = CType(Item, ConsoleReportBriefable)
            If i.Name.Count > count Then count = i.Name.Count
        Next
        For Each Item In l
            If TypeOf Item Is ConsoleReportBriefable = False Then Continue For
            total &= vbIndent(ind + 1) & CType(Item, ConsoleReportBriefable).ConsoleReportBrief(count) & vbCrLf
        Next
        If total = "" Then Return ""
        Return vbIndent(ind) & title & ":" & vbCrLf & total & vbCrLf
    End Function
End Class
