Imports b1
Imports b1.GSMUI.GSWidget
Imports B1UI.GSUI
Imports BtlShare
Imports GSE.GSUI
Imports UnrealEngine.UMG

Public Class B1MiniGM
    Inherits B1UIWrapper(Of UIMiniGM)

    Protected Overrides ReadOnly Property PageId As EnPageID
        Get
            Return EnPageID.MiniGM
        End Get
    End Property

    Public ReadOnly Property DataStore As DSMiniGMPage
        Get
            Return TryGetField(Of DSMiniGMPage)(NameOf(DataStore))
        End Get
    End Property

    Public ReadOnly Property PanelRefWidget As UUserWidget
        Get
            Return TryGetField(Of UUserWidget)(NameOf(PanelRefWidget))
        End Get
    End Property

    Public ReadOnly Property SoulsPanelRefWidget As UUserWidget
        Get
            Return TryGetField(Of UUserWidget)(NameOf(SoulsPanelRefWidget))
        End Get
    End Property

    Public ReadOnly Property MonsterPanelRefWidget As UUserWidget
        Get
            Return TryGetField(Of UUserWidget)(NameOf(MonsterPanelRefWidget))
        End Get
    End Property

    Public ReadOnly Property QuickPanelRefWidget As UUserWidget
        Get
            Return TryGetField(Of UUserWidget)(NameOf(QuickPanelRefWidget))
        End Get
    End Property

    Public ReadOnly Property TransPanelRefWidget As UUserWidget
        Get
            Return TryGetField(Of UUserWidget)(NameOf(TransPanelRefWidget))
        End Get
    End Property

    Public ReadOnly Property SequencePanelRefWidget As UUserWidget
        Get
            Return TryGetField(Of UUserWidget)(NameOf(SequencePanelRefWidget))
        End Get
    End Property

    Public ReadOnly Property CollectPanelRefWidget As UUserWidget
        Get
            Return TryGetField(Of UUserWidget)(NameOf(CollectPanelRefWidget))
        End Get
    End Property

    Public ReadOnly Property ButtonTabRefWidget As UUserWidget
        Get
            Return TryGetField(Of UUserWidget)(NameOf(ButtonTabRefWidget))
        End Get
    End Property

    Public ReadOnly Property CheckBoxWidget As UUserWidget
        Get
            Return TryGetField(Of UUserWidget)(NameOf(CheckBoxWidget))
        End Get
    End Property

    Public ReadOnly Property CenterCon As UWidget
        Get
            Return TryGetField(Of UWidget)(NameOf(CenterCon))
        End Get
    End Property

    Public ReadOnly Property OpenButton As GSButton
        Get
            Return TryGetField(Of GSButton)(NameOf(OpenButton))
        End Get
    End Property

    Public ReadOnly Property MinButton As GSButton
        Get
            Return TryGetField(Of GSButton)(NameOf(MinButton))
        End Get
    End Property

    Public ReadOnly Property CloseButton As GSButton
        Get
            Return TryGetField(Of GSButton)(NameOf(CloseButton))
        End Get
    End Property

    Public ReadOnly Property HelpWorkingTabName As UTextBlock
        Get
            Return TryGetField(Of UTextBlock)(NameOf(HelpWorkingTabName))
        End Get
    End Property

    Public ReadOnly Property HelpButton As GSButton
        Get
            Return TryGetField(Of GSButton)(NameOf(HelpButton))
        End Get
    End Property

    Public ReadOnly Property BugReportButton As GSButton
        Get
            Return TryGetField(Of GSButton)(NameOf(BugReportButton))
        End Get
    End Property

    Public ReadOnly Property AreaIdBlock As UTextBlock
        Get
            Return TryGetField(Of UTextBlock)(NameOf(AreaIdBlock))
        End Get
    End Property

    Public ReadOnly Property InputCon As UCanvasPanel
        Get
            Return TryGetField(Of UCanvasPanel)(NameOf(InputCon))
        End Get
    End Property

    Public ReadOnly Property GMCmd As UEditableTextBox
        Get
            Return TryGetField(Of UEditableTextBox)(NameOf(GMCmd))
        End Get
    End Property

    Public ReadOnly Property LogScrollBox As UScrollBox
        Get
            Return TryGetField(Of UScrollBox)(NameOf(LogScrollBox))
        End Get
    End Property

    Public ReadOnly Property TabGroup As GSBUIButtonGroup
        Get
            Return TryGetField(Of GSBUIButtonGroup)(NameOf(TabGroup))
        End Get
    End Property

    Public ReadOnly Property DefaultPanelView As VIMiniGMPanel
        Get
            Return TryGetField(Of VIMiniGMPanel)(NameOf(DefaultPanelView))
        End Get
    End Property

    Public ReadOnly Property PanelViewDict As Dictionary(Of EnGMTab, IMiniGMPanel)
        Get
            Return TryGetField(Of Dictionary(Of EnGMTab, IMiniGMPanel))(NameOf(PanelViewDict))
        End Get
    End Property

    Public ReadOnly Property CheckOutBoxDict As Dictionary(Of EDeadReason, VIMiniGMCheckOutBox)
        Get
            Return TryGetField(Of Dictionary(Of EDeadReason, VIMiniGMCheckOutBox))(NameOf(CheckOutBoxDict))
        End Get
    End Property

    Public ReadOnly Property CheckOutBoxMgrList As List(Of VIMiniGMCheckOutBox)
        Get
            Return TryGetField(Of List(Of VIMiniGMCheckOutBox))(NameOf(CheckOutBoxMgrList))
        End Get
    End Property

    Public ReadOnly Property TabViewList As List(Of VIMiniGMTab)
        Get
            Return TryGetField(Of List(Of VIMiniGMTab))(NameOf(TabViewList))
        End Get
    End Property

    Public ReadOnly Property DelayTimeForExecute As Single?
        Get
            Return TryGetFieldValue(Of Single)(NameOf(DelayTimeForExecute))
        End Get
    End Property

    Public ReadOnly Property DelayCountForQuick As Integer?
        Get
            Return TryGetFieldValue(Of Integer)(NameOf(DelayCountForQuick))
        End Get
    End Property

    Public ReadOnly Property CurShowStat As Boolean?
        Get
            Return TryGetFieldValue(Of Boolean)(NameOf(CurShowStat))
        End Get
    End Property

    Public ReadOnly Property CurMapId As Integer?
        Get
            Return TryGetFieldValue(Of Integer)(NameOf(CurMapId))
        End Get
    End Property

    Public ReadOnly Property MapAreaBaseData As GSMapAreaBaseData
        Get
            Return TryGetField(Of GSMapAreaBaseData)(NameOf(MapAreaBaseData))
        End Get
    End Property

    Public ReadOnly Property MapAreaDetailData As GSMapAreaDetailData
        Get
            Return TryGetField(Of GSMapAreaDetailData)(NameOf(MapAreaDetailData))
        End Get
    End Property

    Public ReadOnly Property IsMapAreaIdVisibility As Boolean?
        Get
            Return TryGetFieldValue(Of Boolean)(NameOf(IsMapAreaIdVisibility))
        End Get
    End Property

    Public ReadOnly Property IsAutoLoadedSnapShoot As Boolean?
        Get
            Return TryGetFieldValue(Of Boolean)(NameOf(IsAutoLoadedSnapShoot))
        End Get
    End Property

End Class