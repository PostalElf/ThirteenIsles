Public Interface ShipAssignable
    Property Crews As List(Of Crew)
    Sub Add(ByVal crew As Crew)
    Sub Remove(ByVal crew As Crew)
    Function Addable(ByVal crew As Crew) As Boolean
End Interface
