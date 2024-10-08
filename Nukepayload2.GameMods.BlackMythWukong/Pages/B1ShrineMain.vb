﻿Imports B1UI.GSUI
Imports UnrealEngine.UMG

Public Class B1ShrineMain
    Inherits B1UIWrapper(Of UIShrineMain)

    Protected Overrides ReadOnly Property PageId As EnPageID
        Get
            Return EnPageID.ShrineMain
        End Get
    End Property

    Public ReadOnly Property ShrineMain As UIShrineMain
        Get
            Return Page
        End Get
    End Property

    Public ReadOnly Property ShrineDStore As DSShrine
        Get
            Return TryGetField(Of DSShrine)(NameOf(ShrineDStore))
        End Get
    End Property

    Public ReadOnly Property DataStore As DSShrineMain
        Get
            Return TryGetField(Of DSShrineMain)(NameOf(DataStore))
        End Get
    End Property

    Public ReadOnly Property TxtMainName As UTextBlock
        Get
            Return TryGetField(Of UTextBlock)(NameOf(TxtMainName))
        End Get
    End Property

    Public ReadOnly Property TxtSubName As UTextBlock
        Get
            Return TryGetField(Of UTextBlock)(NameOf(TxtSubName))
        End Get
    End Property

    Public ReadOnly Property TxtTips As UTextBlock
        Get
            Return TryGetField(Of UTextBlock)(NameOf(TxtTips))
        End Get
    End Property

    Public ReadOnly Property ImgMap As UImage
        Get
            Return TryGetField(Of UImage)(NameOf(ImgMap))
        End Get
    End Property

    Public ReadOnly Property RootCon As UWidget
        Get
            Return TryGetField(Of UWidget)(NameOf(RootCon))
        End Get
    End Property

    Public ReadOnly Property ShrineMenuHelper As FMenuHelper(Of EShrineMenuTag)
        Get
            Return TryGetField(Of FMenuHelper(Of EShrineMenuTag))(NameOf(ShrineMenuHelper))
        End Get
    End Property

    Public ReadOnly Property TeleportMenuHelper As FMenuHelper(Of ETeleportMenuTag)
        Get
            Return TryGetField(Of FMenuHelper(Of ETeleportMenuTag))(NameOf(TeleportMenuHelper))
        End Get
    End Property

    Public ReadOnly Property AnimRest As UWidgetAnimation
        Get
            Return TryGetField(Of UWidgetAnimation)(NameOf(AnimRest))
        End Get
    End Property

    Public ReadOnly Property AnimRestFinishedEvent As FWidgetAnimationDynamicEvent
        Get
            Return TryGetField(Of FWidgetAnimationDynamicEvent)(NameOf(AnimRestFinishedEvent))
        End Get
    End Property

    Public ReadOnly Property IsShowWineTips As Boolean?
        Get
            Return TryGetFieldValue(Of Boolean)(NameOf(IsShowWineTips))
        End Get
    End Property

    Public ReadOnly Property DelayEvent As UIDelayEvent
        Get
            Return TryGetField(Of UIDelayEvent)(NameOf(DelayEvent))
        End Get
    End Property

    Public ReadOnly Property UpdatePicDelayEvent As UIDelayEvent
        Get
            Return TryGetField(Of UIDelayEvent)(NameOf(UpdatePicDelayEvent))
        End Get
    End Property

End Class
