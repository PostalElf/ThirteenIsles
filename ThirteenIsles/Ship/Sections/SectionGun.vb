﻿Public Class SectionGun
    Inherits Section
    Public Overrides ReadOnly Property JobDescription As String
        Get
            Return "Gunner"
        End Get
    End Property
    Public Overrides Function GetSection(ByVal param As String()) As Boolean
        For Each p In param
            Dim ps As String() = p.Split("=")
            Select Case ps(0).ToLower
                Case "type" : If ps(1) <> "gun" Then Return False
            End Select
        Next
        Return True
    End Function
    Private LoadProgress As Integer
    Private LoadProgressMax As Integer
    Private ReadOnly Property LoadProgressPercentage As Integer
        Get
            Return LoadProgress / LoadProgressMax * 100
        End Get
    End Property

    Public Function ConsoleReportBrief() As String
        Dim total As String = vbTabb(Name & ":", 15)
        total &= ProgressBar(10, LoadProgressPercentage)
        Return total
    End Function
End Class
