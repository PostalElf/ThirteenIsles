Public Class Skills
    Public Sub New()
        For Each e In [Enum].GetValues(GetType(Skill))
            BaseList.Add(e, 0)
            XPList.Add(e, 0)
        Next
    End Sub
    Private BaseList As New Dictionary(Of Skill, Integer)
    Private XPList As New Dictionary(Of Skill, Integer)
    Default Public Property Name(ByVal skill As Skill) As Integer
        Get
            Return BaseList(skill)
        End Get
        Set(ByVal value As Integer)
            BaseList(skill) = value
        End Set
    End Property

    Public Sub Improve(ByVal skill As Skill, Optional ByVal amt As Integer = 1)
        XPList(skill) = Math.Min((XPList(skill) + amt), Threshold(skill))
    End Sub
    Public Function XPPercentage(ByVal skill As Skill) As Integer
        If XPList(skill) = 0 Then Return 0
        Return XPList(skill) / Threshold(skill) * 100
    End Function
    Public Function Threshold(ByVal skill As Skill) As Integer
        Return (BaseList(skill) + 1) * 5
    End Function
    Public Sub Train(ByVal skill As Skill)
        If XPPercentage(skill) < 100 Then Exit Sub

        XPList(skill) = 0
        BaseList(skill) += 1
    End Sub

    Public Function BestSkill() As Skill
        Dim skill As Skill = 0
        Dim value As Integer = -1
        For Each e In [Enum].GetValues(GetType(Skill))
            If BaseList(e) > value Then
                skill = e
                value = BaseList(e)
            End If
        Next
        Return skill
    End Function

End Class

Public Enum Skill
    Gunnery = 1
    Healing
    Cooking
    Sailing
    Navigating
    Steering
    Carpentry
    Alchemy

    Firearms = 11
    Melee
    Stormstriking
End Enum