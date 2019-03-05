Public Class SectionGun
    Inherits Section
    Public Overrides ReadOnly Property JobDescription As String
        Get
            Return "Gunner"
        End Get
    End Property
    Public Overrides ReadOnly Property JobSkill As Skill
        Get
            Return Skill.Gunnery
        End Get
    End Property
    Public Overrides Function GetSection(ByVal param As String()) As Boolean
        For Each p In param
            Dim ps As String() = p.Split("=")
            Select Case ps(0).ToLower
                Case "type" : If ps(1) <> "gun" AndAlso ps(1) <> "guns" Then Return False
                Case Else : If MyBase.GetSectionBase(ps) = False Then Return False
            End Select
        Next
        Return True
    End Function
    Public Overrides ReadOnly Property NameFull As String
        Get
            Dim total As String = _Name & " (Guns"
            If Quadrant Is Nothing = False Then total &= " - " & Quadrant.Facing.ToString & ")" Else total &= ")"
            Return total
        End Get
    End Property

    Private LoadProgress As Integer
    Private LoadProgressMax As Integer
    Private ReadOnly Property LoadProgressPercentage As Integer
        Get
            Return LoadProgress / LoadProgressMax * 100
        End Get
    End Property
    Private ReadOnly Property LoadProgressIncome As Integer
        Get
            Dim total As Integer = 0
            For Each Crew In GetCrews({""})
                total += Crew.GetJobSkill
            Next
            Return total
        End Get
    End Property
    Private Accuracy As Integer
    Private DamageCrew As Integer
    Private DamageHull As Integer

    Public ReadOnly Property IsReady As Boolean
        Get
            If LoadProgressPercentage = 100 Then Return True Else Return False
        End Get
    End Property
    Public Sub CombatTick()
        If IsReady = True Then Exit Sub
        LoadProgress += LoadProgressIncome
        If LoadProgress > LoadProgressMax Then LoadProgress = LoadProgressMax
    End Sub

    Protected Overrides Function ConsoleReportBrief(Optional ByVal colonPosition As Integer = 0) As String
        Dim total As String = vbTabb(Name & ":", colonPosition)
        total &= ProgressBar(10, LoadProgressPercentage)
        Return total
    End Function
End Class
