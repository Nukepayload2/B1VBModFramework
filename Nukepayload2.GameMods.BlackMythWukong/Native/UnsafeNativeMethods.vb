Imports System.Runtime.InteropServices
Imports System.Runtime.Versioning
Imports System.Security

Friend Class UnsafeNativeMethods

    ''' **************************************************************************
    ''' ;GetDiskFreeSpaceEx
    ''' <summary>
    ''' Used to determine how much free space is on a disk
    ''' </summary>
    ''' <param name="Directory">Path including drive we're getting information about</param>
    ''' <param name="UserSpaceFree">The amount of free sapce available to the current user</param>
    ''' <param name="TotalUserSpace">The total amount of space on the disk relative to the current user</param>
    ''' <param name="TotalFreeSpace">The amount of free spave on the disk.</param>
    ''' <returns>True if function succeeds in getting info otherwise False</returns>
    <SecurityCritical()>
    <ResourceExposure(ResourceScope.None)>
    <DllImport("Kernel32.dll", CharSet:=CharSet.Auto, BestFitMapping:=False, SetLastError:=True)>
    Friend Shared Function GetDiskFreeSpaceEx(Directory As String, ByRef UserSpaceFree As Long, ByRef TotalUserSpace As Long, ByRef TotalFreeSpace As Long) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

End Class
