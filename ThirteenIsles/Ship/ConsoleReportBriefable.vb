Public Interface ConsoleReportBriefable
    ReadOnly Property Name As String
    Function ConsoleReportBrief(Optional ByVal colonPosition As Integer = 0) As String
End Interface
