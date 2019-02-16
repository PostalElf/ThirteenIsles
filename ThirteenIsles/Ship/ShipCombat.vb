Public Interface ShipCombat
    Property Name As String
    Property Quadrants As Dictionary(Of Directions, Quadrant)
    Property CurrentFacing As Directions
End Interface
