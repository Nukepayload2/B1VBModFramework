Imports System.Runtime.CompilerServices

<Assembly: IgnoresAccessChecksTo("BtlSvr.Main")>
<Assembly: IgnoresAccessChecksTo("B1UI_GSE.Script")>

Namespace Global.System.Runtime.CompilerServices
	<AttributeUsage(AttributeTargets.Assembly, AllowMultiple:=True)>
	Friend NotInheritable Class IgnoresAccessChecksToAttribute
		Inherits Attribute

		Public Sub New(assemblyName As String)
		End Sub
	End Class
End Namespace
