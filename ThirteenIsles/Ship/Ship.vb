﻿Public Class Ship
    Implements ShipCombat
    Public Shared Function Generate(ByVal size As ShipSize) As Ship
        Dim ship As New Ship
        With ship
            .Name = ShipGenerator.GetName
            .Size = size
            Select Case size
                Case ShipSize.Sloop
                    .Quadrants.Add(Directions.Fore, New Quadrant(ship, Directions.Fore, 3, 0))
                    .Quadrants.Add(Directions.Starboard, New Quadrant(ship, Directions.Starboard, 3, 1))
                    .Quadrants.Add(Directions.Aft, New Quadrant(ship, Directions.Aft, 3, 0))
                    .Quadrants.Add(Directions.Port, New Quadrant(ship, Directions.Port, 3, 1))
                    .Inventory = New Inventory(20)
                    .MaxSailing = 20
                    .MaxManeuver = 5
                Case ShipSize.Schooner
                    .Quadrants.Add(Directions.Fore, New Quadrant(ship, Directions.Fore, 4, 1))
                    .Quadrants.Add(Directions.Starboard, New Quadrant(ship, Directions.Starboard, 4, 1))
                    .Quadrants.Add(Directions.Aft, New Quadrant(ship, Directions.Aft, 4, 0))
                    .Quadrants.Add(Directions.Port, New Quadrant(ship, Directions.Port, 4, 1))
                    .Inventory = New Inventory(30)
                    .MaxSailing = 20
                    .MaxManeuver = 4
                Case ShipSize.Brig
                    .Quadrants.Add(Directions.Fore, New Quadrant(ship, Directions.Fore, 5, 1))
                    .Quadrants.Add(Directions.Starboard, New Quadrant(ship, Directions.Starboard, 5, 2))
                    .Quadrants.Add(Directions.Aft, New Quadrant(ship, Directions.Aft, 5, 0))
                    .Quadrants.Add(Directions.Port, New Quadrant(ship, Directions.Port, 5, 2))
                    .Inventory = New Inventory(50)
                    .MaxSailing = 20
                    .MaxManeuver = 3
                Case ShipSize.Frigate
                    .Quadrants.Add(Directions.Fore, New Quadrant(ship, Directions.Fore, 6, 1))
                    .Quadrants.Add(Directions.Starboard, New Quadrant(ship, Directions.Starboard, 6, 3))
                    .Quadrants.Add(Directions.Aft, New Quadrant(ship, Directions.Aft, 6, 1))
                    .Quadrants.Add(Directions.Port, New Quadrant(ship, Directions.Port, 6, 3))
                    .Inventory = New Inventory(70)
                    .MaxSailing = 16
                    .MaxManeuver = 2
                Case ShipSize.Manowar
                    .Quadrants.Add(Directions.Fore, New Quadrant(ship, Directions.Fore, 7, 1))
                    .Quadrants.Add(Directions.Starboard, New Quadrant(ship, Directions.Starboard, 7, 4))
                    .Quadrants.Add(Directions.Aft, New Quadrant(ship, Directions.Aft, 7, 1))
                    .Quadrants.Add(Directions.Port, New Quadrant(ship, Directions.Port, 7, 4))
                    .Inventory = New Inventory(80)
                    .MaxSailing = 15
                    .MaxManeuver = 1
            End Select
        End With
        Return ship
    End Function

    Private Property Name As String Implements ShipCombat.Name
    Private Size As ShipSize
    Private Property CurrentFacing As Directions Implements ShipCombat.CurrentFacing

    Private Inventory As Inventory
    Public Function Add(ByVal item As Item) As String
        Return Inventory.Add(item)
    End Function
    Public Function Remove(ByVal item As Item) As String
        Return Inventory.Remove(item)
    End Function

    Private Sailing As Integer
    Private MaxSailing As Integer
    Private Maneuver As Integer
    Private MaxManeuver As Integer

    Private Property Quadrants As New Dictionary(Of Directions, Quadrant) Implements ShipCombat.Quadrants
    Public Function Add(ByVal section As Section, ByVal facing As Directions) As String
        Dim q As Quadrant = Quadrants(facing)
        Return q.Add(section)
    End Function
    Public Function Remove(ByVal section As Section, ByVal facing As Directions) As String
        Dim q As Quadrant = Quadrants(facing)
        Return q.remove(section)
    End Function
    Public Function GetSections(ByVal param As String()) As List(Of Section)
        Dim total As New List(Of Section)
        For Each q In Quadrants.Values
            Dim qs As List(Of Section) = q.GetSections(param)
            If qs Is Nothing = False AndAlso qs.Count > 0 Then total.AddRange(qs)
        Next
        Return total
    End Function
    Public Function GetCrews(ByVal param As String()) As List(Of Crew)
        Dim total As New List(Of Crew)
        For Each q In Quadrants.Values
            Dim qs As List(Of Crew) = q.getcrews(param)
            If qs Is Nothing = False AndAlso qs.Count > 0 Then total.AddRange(qs)
        Next
        Return total
    End Function

    Public Function ConsoleReport(ByVal id As Integer) As String
        Dim total As String = vbIndent(id) & "The " & Size.ToString & " '" & Name & "'" & vbCrLf
        total &= vbIndent(id + 1) & vbTabb("Cargo:", 12) & Inventory.Size & "/" & Inventory.SizeMax & vbCrLf
        total &= vbIndent(id + 1) & vbTabb("Sailgauge:", 12) & MaxSailing & vbCrLf
        total &= vbIndent(id + 1) & vbTabb("Maneuvers:", 12) & Maneuver & "/" & MaxManeuver & vbCrLf

        total &= vbIndent(id + 1) & "Quadrants:" & vbCrLf
        For n As Directions = 1 To 4
            total &= vbIndent(id + 2) & Quadrants(n).ConsoleReportBrief() & vbCrLf
        Next

        Return total
    End Function
    Public Function ConsoleReportInventory(ByVal id As Integer) As String
        Return Inventory.ConsoleReport(id)
    End Function
End Class

Public Enum ShipSize
    Sloop = 1
    Schooner
    Brig
    Frigate
    Manowar
End Enum

Public Enum Directions
    Fore = 1
    Starboard
    Aft
    Port
End Enum

Public Class ShipGenerator
    Private Shared Prefixes As New List(Of String)
    Private Shared Suffixes As New List(Of String)
    Public Shared Function GetName() As String
        If Prefixes.Count = 0 Then Prefixes = IO.ImportTextList("data/shipPrefixes.txt")
        If Suffixes.Count = 0 Then Suffixes = IO.ImportTextList("data/shipSuffixes.txt")
        Return GrabRandom(Prefixes) & " " & GrabRandom(Suffixes)
    End Function
End Class