﻿Module Module1

    Sub Main()
        Console.SetWindowSize(200, 50)
        Dim menuChoices As New Dictionary(Of Char, String)
        menuChoices.Add("c"c, "Cargo Hull")
        menuChoices.Add("q"c, "Inspect Quadrant")
        menuChoices.Add("r"c, "Manage Roles")
        menuChoices.Add("s"c, "Install Section")
        menuChoices.Add("a"c, "Add Crew")

        Dim ship As Ship = SetupShip()
        While True
            Console.Clear()
            Console.ForegroundColor = ConsoleColor.DarkGray
            Console.WriteLine(ship.ConsoleReport(0))
            Console.WriteLine()
            Dim choice = Menu.getListChoice(menuChoices, 0, "Select option:")
            Console.WriteLine()
            Console.WriteLine()
            Select Case choice
                Case "c"c : MenuCargoHull(ship)
                Case "q"c : MenuQuadrant(ship)
                Case "r"c : MenuRole(ship)
                Case "s"c : menuSection(ship)
                Case "a"c : menuCrew(ship)
            End Select
        End While
    End Sub

    Private Function SetupShip() As Ship
        Dim ship As Ship = ship.Generate(ShipSize.Schooner)
        ship.Add(Item.Generate("Bloodcedar", 5))
        ship.Add(SectionQuarters.Generate(Race.Mortal, 6), Directions.Aft)
        ship.Add(SectionQuarters.Generate(Race.Mortal, 6), Directions.Aft)
        ship.Add(SectionSails.Generate(SailQuality.Standard, 4), Directions.Port)
        ship.Add(SectionSails.Generate(SailQuality.Standard, 4), Directions.Port)
        Return ship
    End Function

    Private Sub MenuCargoHull(ByVal ship As Ship)
        Console.WriteLine("Inventory")
        Console.WriteLine(ship.ConsoleReportInventory(1))
        Console.ReadKey()
    End Sub
    Private Sub MenuQuadrant(ByVal ship As Ship)
        Dim dirs As New List(Of Directions) From {Directions.Fore, Directions.Starboard, Directions.Aft, Directions.Port}
        Dim d As Directions = Menu.getListChoice(Of Directions)(dirs, 0)
    End Sub
    Private Sub MenuRole(ByVal ship As Ship)

    End Sub
    Private Sub MenuSection(ByVal Ship As Ship)

    End Sub
    Private Sub MenuCrew(ByVal ship As Ship)
        Dim crew As Crew = crew.Generate(Race.Mortal)
        Console.WriteLine(crew.ConsoleReport())
        Console.WriteLine()
        If Menu.confirmChoice(0, "Add to ship? ") = False Then Exit Sub

        'check for housing
        Console.WriteLine()
        Dim quarters As List(Of Section) = ship.GetSections({"type=quarters"})
        For n = quarters.Count - 1 To 0 Step -1
            If CType(quarters(n), ShipAssignable).Addable(crew) = False Then quarters.RemoveAt(n)
        Next
        If quarters.Count = 0 Then Console.WriteLine("Insufficient berthing space.") : Console.ReadKey() : Exit Sub
        Dim quarter As SectionQuarters = Menu.getListChoice(quarters, 0, "Select berth:")

        'assign role
        Dim sections As List(Of Section) = ship.GetSections({"addable=true"})
        For n = sections.Count - 1 To 0 Step -1
            If sections(n).GetType = GetType(SectionQuarters) Then sections.RemoveAt(n)
        Next
        If sections.Count = 0 Then Exit Sub
        Dim job As Section = Menu.getListChoice(Of Section)(sections, 0, "Assign to which section?")

        'confirm
        crew.Quarters = quarter
        crew.Job = job
        Console.WriteLine(crew.Name & " has been berthed in " & quarter.Name & " and is working in " & job.Name & ".")
        Console.ReadKey()
    End Sub
End Module
