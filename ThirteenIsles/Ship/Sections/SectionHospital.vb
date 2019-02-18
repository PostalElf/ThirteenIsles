Public Class SectionHospital
    Inherits Section
    Public Overrides ReadOnly Property JobDescription As String
        Get
            Return "Doctor"
        End Get
    End Property
End Class
