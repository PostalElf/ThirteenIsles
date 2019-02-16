Module Module1

    Sub Main()
        Dim menuChoices As New Dictionary(Of Char, String)
        menuChoices.Add("c"c, "Cargo Hull")
        menuChoices.Add("q"c, "Inspect Quadrant")
        menuChoices.Add("r"c, "Assign Roles")

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
                Case "r"c
            End Select
        End While
    End Sub

    Private Function SetupShip() As Ship
        Dim ship As Ship = ship.Generate(ShipSize.Schooner)
        ship.Add(Crew.Generate(Race.Mortal))
        ship.Add(Item.Generate("Bloodcedar", 5))
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
        Dim quadrant As Quadrant = ship.quadrants(d)
    End Sub
    Private Sub MenuRole(ByVal ship As Ship)

    End Sub
End Module
