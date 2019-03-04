Public Interface ShipCombat
    Property Name As String
    Property Quadrants As Dictionary(Of Directions, Quadrant)
    Property CurrentFacing As Directions

    Property Sailing As Integer
    Property SailingMax As Integer
    Property Maneuver As Integer
    Property ManeuverMax As Integer

    Sub Tick(ByVal battlefield As Battlefield)
    Sub Attack(ByVal ship As ShipCombat, ByVal gun As SectionGun)
End Interface
