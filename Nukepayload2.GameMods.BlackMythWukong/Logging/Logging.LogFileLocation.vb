' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.

Namespace Logging

    ''' <summary>
    '''  Options for the location of a log's directory.
    ''' </summary>
    Public Enum LogFileLocation As Integer

        ' Changes to this enum must be reflected in ValidateLogfileLocationEnumValue()
        TempDirectory
        LocalUserApplicationDirectory
        CommonApplicationDirectory
        ExecutableDirectory
        ''' <summary>
        ''' Write to the MOD directory. It only takes effect if you have embedded this assembly into your assembly.
        ''' </summary>
        ModDirectory
        Custom
    End Enum

End Namespace
