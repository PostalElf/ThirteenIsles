Module Module1

    Sub Main()
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
        ship.Add(New SectionQuarters, Directions.Port)
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
        Dim quarters As List(Of Section) = ship.GetSections({"type=quarters"})
        Dim hasSpace As Boolean = True
        For Each q In quarters
            If CType(q, ShipAssignable).Addable(crew) = True Then hasSpace = False : Exit For
        Next
        If hasSpace = False Then Console.WriteLine("Insufficient berthing space.") : Console.ReadKey() : Exit Sub

        'assign role
        Dim sections As List(Of Section) = ship.GetSections({""})
        If sections.Count = 0 Then Exit Sub

    End Sub
End Module
