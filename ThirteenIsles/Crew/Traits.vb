Public Class Traits
    Private BaseList As New List(Of Trait)
    Public Function Add(ByVal trait As Trait) As String
        If BaseList.Contains(trait) Then Return "Trait already exists."
        BaseList.Add(trait)
        Return Nothing
    End Function
    Public Function GetSkillBonus(ByVal skill As Skill) As Integer
        Dim total As Integer = 0
        Select Case skill
            Case ThirteenIsles.Skill.Gunnery
                If BaseList.Contains(Trait.Flamekissed) Then total += 1
                If BaseList.Contains(Trait.Powdermonkey) Then total += 1
            Case ThirteenIsles.Skill.Firearms
                If BaseList.Contains(Trait.Flamekissed) Then total += 1
                If BaseList.Contains(Trait.Deadeye) Then total += 1
            Case ThirteenIsles.Skill.Healing
                If BaseList.Contains(Trait.Saltborn) Then total += 1
                If BaseList.Contains(Trait.Barber) Then total += 1
            Case ThirteenIsles.Skill.Cooking
                If BaseList.Contains(Trait.Cultist) Then total += 1
                If BaseList.Contains(Trait.Gourmet) Then total += 1
            Case ThirteenIsles.Skill.Stormstriking
                If BaseList.Contains(Trait.Stormtossed) Then total += 1
            Case ThirteenIsles.Skill.Sailing
                If BaseList.Contains(Trait.Windsung) Then total += 1
                If BaseList.Contains(Trait.Whistler) Then total += 1
            Case ThirteenIsles.Skill.Navigating
                If BaseList.Contains(Trait.Windsung) Then total += 1
                If BaseList.Contains(Trait.Explorer) Then total += 1
            Case ThirteenIsles.Skill.Steering
                If BaseList.Contains(Trait.Woodwoden) Then total += 1
                If BaseList.Contains(Trait.Coxswain) Then total += 1
            Case ThirteenIsles.Skill.Carpentry
                If BaseList.Contains(Trait.Woodwoden) Then total += 1
                If BaseList.Contains(Trait.Carpenter) Then total += 1
            Case ThirteenIsles.Skill.Alchemy
                If BaseList.Contains(Trait.Chrysopoet) Then total += 1
                If BaseList.Contains(Trait.Saltborn) Then total += 1
            Case ThirteenIsles.Skill.Melee
                If BaseList.Contains(Trait.Scrapper) Then total += 1
        End Select
        Return total
    End Function

    Public Function ConsoleReport(ByVal id As Integer) As String
        Dim total As String = ""
        For Each t In BaseList
            total &= vbIndent(id) & t.ToString & vbCrLf
        Next
        Return total
    End Function
End Class

Public Enum Trait
    Flamekissed = 0
    Saltborn
    Stormtossed
    Windsung
    Woodwoden
    Cultist

    Powdermonkey = 11
    Deadeye
    Barber
    Gourmet
    Whistler
    Explorer
    Carpenter
    Coxswain
    Chrysopoet
    Scrapper
End Enum