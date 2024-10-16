Imports System.Runtime.CompilerServices

Public Module ModifierKeysHelper
    <Extension>
    Sub AddFlag(ByRef currentFlag As ModifierKeys, newFlag As ModifierKeys)
        currentFlag = currentFlag Or newFlag
    End Sub

    <Extension>
    Function WithFlag(currentFlag As ModifierKeys, newFlag As ModifierKeys) As ModifierKeys
        Return currentFlag Or newFlag
    End Function

    <Extension>
    Sub RemoveFlag(ByRef currentFlag As ModifierKeys, flagToRemove As ModifierKeys)
        currentFlag = currentFlag And Not flagToRemove
    End Sub

    <Extension>
    Function WithoutFlag(currentFlag As ModifierKeys, flagToRemove As ModifierKeys) As ModifierKeys
        Return currentFlag And Not flagToRemove
    End Function

    <Extension>
    Function HasFlag(currentFlag As ModifierKeys, testFlag As ModifierKeys) As Boolean
        Return (currentFlag And testFlag) = testFlag
    End Function
End Module
