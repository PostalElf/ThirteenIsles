Public Class Ship
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
    Private MaxSailing As Integer
    Private MaxManeuver As Integer
    Private Property CurrentFacing As Directions Implements ShipCombat.CurrentFacing

    Private Inventory As Inventory
    Public Function Add(ByVal item As Item) As String
        Return Inventory.Add(item)
    End Function
    Public Function Remove(ByVal item As Item) As String
        Return Inventory.Remove(item)
    End Function

    Private Maneuver As Integer
    Public Property Quadrants As New Dictionary(Of Directions, Quadrant) Implements ShipCombat.Quadrants
    Private UnassignedCrew As New List(Of Crew)
    Private CrewSizeMax As Integer
    Public Function Add(ByVal crew As Crew) As String
        If UnassignedCrew.Contains(crew) Then Return "Crew already assigned."

        UnassignedCrew.Add(crew)
        Return Nothing
    End Function
    Public Function Remove(ByVal crew As Crew) As String
        If UnassignedCrew.Contains(crew) = False Then Return "Crew not found."

        UnassignedCrew.Remove(crew)
        Return Nothing
    End Function

    Public Function ConsoleReport(ByVal id As Integer) As String
        Dim total As String = vbIndent(id) & "The " & Size.ToString & " '" & Name & "'" & vbCrLf
        total &= vbIndent(id + 1) & vbTabb("Cargo Hull:", 12) & Inventory.Size & "/" & Inventory.SizeMax & vbCrLf
        total &= vbIndent(id + 1) & vbTabb("Sailgauge:", 12) & MaxSailing & vbCrLf
        total &= vbIndent(id + 1) & vbTabb("Maneuvers:", 12) & Maneuver & "/" & MaxManeuver & vbCrLf

        For n As Directions = 1 To 4
            total &= Quadrants(n).ConsoleReport(id + 1)
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