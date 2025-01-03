﻿Namespace Media
    Public Class SystemSound
        Private ReadOnly _soundName As String

        Public Shared ReadOnly Property BusVolumeMute As New SystemSound("EVT_env_bus_volume_mute")
        Public Shared ReadOnly Property BusVolumeUnmute As New SystemSound("EVT_env_bus_volume_unmute")
        Public Shared ReadOnly Property PlayerBianshenFull As New SystemSound("EVT_hud_player_bianshen_full")
        Public Shared ReadOnly Property PlayerBianshenShuangtoushuFull As New SystemSound("EVT_hud_player_bianshen_shuangtoushu_full")
        Public Shared ReadOnly Property PlayerFashuFull As New SystemSound("EVT_hud_player_fashu_full")
        Public Shared ReadOnly Property PlayerGunshiLevel(bindPelevel As Integer) As SystemSound
            Get
                Return New SystemSound($"EVT_hud_player_gunshi_level{bindPelevel}")
            End Get
        End Property
        Public Shared ReadOnly Property PlayerGunshiQiehuan As New SystemSound("EVT_hud_player_gunshi_qiehuan")
        Public Shared ReadOnly Property MusicSeqEndaIntoTheWestStop As New SystemSound("EVT_music_seq_enda_into_the_west_stop")
        Public Shared ReadOnly Property MusicSeqEndbThisIsNotTheEndStop As New SystemSound("EVT_music_seq_endb_this_is_not_the_end_stop")
        Public Shared ReadOnly Property SystemChapterEnterPlay As New SystemSound("EVT_system_chapter_enter_play")
        Public Shared ReadOnly Property SystemChapterEnterStop As New SystemSound("EVT_system_chapter_enter_stop")
        Public Shared ReadOnly Property SystemChapterEnvStop As New SystemSound("EVT_system_chapter_env_stop")
        Public Shared ReadOnly Property SystemLoadingBegin As New SystemSound("EVT_system_loading_begin")
        Public Shared ReadOnly Property ChapterAwardItemShow As New SystemSound("EVT_ui_chapter_award_item_show")
        Public Shared ReadOnly Property ChapterAwardTalentponitShow As New SystemSound("EVT_ui_chapter_award_talentponit_show")
        Public Shared ReadOnly Property ChapterSwitch As New SystemSound("EVT_ui_chapter_switch")
        Public Shared ReadOnly Property EquipmentItemtabSwitch As New SystemSound("EVT_ui_equipment_itemtab_switch")
        Public Shared ReadOnly Property EquipmentObservemodeDisappear As New SystemSound("EVT_ui_equipment_observemode_disappear")
        Public Shared ReadOnly Property EquipmentObservemodeShow As New SystemSound("EVT_ui_equipment_observemode_show")
        Public Shared ReadOnly Property EquipmentStatsClose As New SystemSound("EVT_ui_equipment_stats_close")
        Public Shared ReadOnly Property EquipmentStatsOpen As New SystemSound("EVT_ui_equipment_stats_open")
        Public Shared ReadOnly Property EquipmentStatsSelfClose As New SystemSound("EVT_ui_equipment_stats_self_close")
        Public Shared ReadOnly Property EquipmentStatsSelfOpen As New SystemSound("EVT_ui_equipment_stats_self_open")
        Public Shared ReadOnly Property GenqiExpand As New SystemSound("EVT_ui_genqi_expand")
        Public Shared ReadOnly Property GenqiGenqitalentitemFocus As New SystemSound("EVT_ui_genqi_genqitalentitem_focus")
        Public Shared ReadOnly Property GenqiGenqitalentitemForget As New SystemSound("EVT_ui_genqi_genqitalentitem_forget")
        Public Shared ReadOnly Property GlobalBotbtnClick As New SystemSound("EVT_ui_global_botbtn_click")
        Public Shared ReadOnly Property GlobalDiamonditemClick As New SystemSound("EVT_ui_global_diamonditem_click")
        Public Shared ReadOnly Property GlobalDiamonditemEquip As New SystemSound("EVT_ui_global_diamonditem_equip")
        Public Shared ReadOnly Property GlobalDiamonditemFocus As New SystemSound("EVT_ui_global_diamonditem_focus")
        Public Shared ReadOnly Property GlobalDiamonditemRemove As New SystemSound("EVT_ui_global_diamonditem_remove")
        Public Shared ReadOnly Property GlobalHalfinterfaceDisappear As New SystemSound("EVT_ui_global_halfinterface_disappear")
        Public Shared ReadOnly Property GlobalHalfinterfaceShow As New SystemSound("EVT_ui_global_halfinterface_show")
        Public Shared ReadOnly Property GlobalItemDoublecheckBuy As New SystemSound("EVT_ui_global_item_doublecheck_buy")
        Public Shared ReadOnly Property GlobalItemDoublecheckCancel As New SystemSound("EVT_ui_global_item_doublecheck_cancel")
        Public Shared ReadOnly Property GlobalItemDoublecheckConfirm As New SystemSound("EVT_ui_global_item_doublecheck_confirm")
        Public Shared ReadOnly Property GlobalItemDoublecheckSell As New SystemSound("EVT_ui_global_item_doublecheck_sell")
        Public Shared ReadOnly Property GlobalRectangleitemClick As New SystemSound("EVT_ui_global_rectangleitem_click")
        Public Shared ReadOnly Property GlobalRectangleitemEquip As New SystemSound("EVT_ui_global_rectangleitem_equip")
        Public Shared ReadOnly Property GlobalRectangleitemFocus As New SystemSound("EVT_ui_global_rectangleitem_focus")
        Public Shared ReadOnly Property GlobalSquareitemReject As New SystemSound("EVT_ui_global_squareitem_reject")
        Public Shared ReadOnly Property GlobalSquareitemRemove As New SystemSound("EVT_ui_global_squareitem_remove")
        Public Shared ReadOnly Property GlobalSubinterfaceClose As New SystemSound("EVT_ui_global_subinterface_close")
        Public Shared ReadOnly Property GlobalVideoClose As New SystemSound("EVT_ui_global_video_close")
        Public Shared ReadOnly Property GlobalVideoOpen As New SystemSound("EVT_ui_global_video_open")
        Public Shared ReadOnly Property GamepadDetailsClose As New SystemSound("EVT_ui_hud_gamepad_details_close")
        Public Shared ReadOnly Property GamepadDetailsExpand As New SystemSound("EVT_ui_hud_gamepad_details_expand")
        Public Shared ReadOnly Property BaxuLoopContacted As New SystemSound("EVT_ui_hud_hint_baxu_loop_contacted")
        Public Shared ReadOnly Property BaxuLoopContactedStop As New SystemSound("EVT_ui_hud_hint_baxu_loop_contacted_stop")
        Public Shared ReadOnly Property BaxuStartContacted As New SystemSound("EVT_ui_hud_hint_baxu_start_contacted")
        Public Shared ReadOnly Property BaxuStartUncontacted As New SystemSound("EVT_ui_hud_hint_baxu_start_uncontacted")
        Public Shared ReadOnly Property BianshenFirstget As New SystemSound("EVT_ui_hud_hint_bianshen_firstget")
        Public Shared ReadOnly Property FashuItemUnfull As New SystemSound("EVT_ui_hud_hint_fashu_item_unfull")
        Public Shared ReadOnly Property FashuNocd As New SystemSound("EVT_ui_hud_hint_fashu_nocd")
        Public Shared ReadOnly Property FashuUnfull As New SystemSound("EVT_ui_hud_hint_fashu_unfull")
        Public Shared ReadOnly Property ItembigShowP2 As New SystemSound("EVT_ui_hud_hint_itembig_show_p2")
        Public Shared ReadOnly Property ItemmediumDropLevel01 As New SystemSound("EVT_ui_hud_hint_itemmedium_drop_level01")
        Public Shared ReadOnly Property ItemmediumDropLevel02 As New SystemSound("EVT_ui_hud_hint_itemmedium_drop_level02")
        Public Shared ReadOnly Property ItemmediumDropLevel03 As New SystemSound("EVT_ui_hud_hint_itemmedium_drop_level03")
        Public Shared ReadOnly Property ItemmediumDropQuest As New SystemSound("EVT_ui_hud_hint_itemmedium_drop_quest")
        Public Shared ReadOnly Property ItemsmallDrop As New SystemSound("EVT_ui_hud_hint_itemsmall_drop")
        Public Shared ReadOnly Property LingyunDrop As New SystemSound("EVT_ui_hud_hint_lingyun_drop")
        Public Shared ReadOnly Property MapnameAppear As New SystemSound("EVT_ui_hud_hint_mapname_appear")
        Public Shared ReadOnly Property MapnameDisappear As New SystemSound("EVT_ui_hud_hint_mapname_disappear")
        Public Shared ReadOnly Property TalentpointGain As New SystemSound("EVT_ui_hud_hint_talentpoint_gain")
        Public Shared ReadOnly Property TravelnoteDrop As New SystemSound("EVT_ui_hud_hint_travelnote_drop")
        Public Shared ReadOnly Property TutorialPopupboxDisappear As New SystemSound("EVT_ui_hud_hint_tutorial_popupbox_disappear")
        Public Shared ReadOnly Property TutorialPopupboxShow As New SystemSound("EVT_ui_hud_hint_tutorial_popupbox_show")
        Public Shared ReadOnly Property TutorialPopupboxSwitch As New SystemSound("EVT_ui_hud_hint_tutorial_popupbox_switch")
        Public Shared ReadOnly Property TutorialTipsDisappear As New SystemSound("EVT_ui_hud_hint_tutorial_tips_disappear")
        Public Shared ReadOnly Property TutorialTipsShow As New SystemSound("EVT_ui_hud_hint_tutorial_tips_show")
        Public Shared ReadOnly Property XpGain As New SystemSound("EVT_ui_hud_hint_xp_gain")
        Public Shared ReadOnly Property PlayerShangxianzengzhang As New SystemSound("EVT_ui_hud_player_shangxianzengzhang")
        Public Shared ReadOnly Property ShortcutitemSwitch As New SystemSound("EVT_ui_hud_shortcutitem_switch")
        Public Shared ReadOnly Property XintiaoStop As New SystemSound("EVT_ui_hud_xintiao_stop")
        Public Shared ReadOnly Property PhotoDisappear As New SystemSound("EVT_ui_photo_disappear")
        Public Shared ReadOnly Property PhotoShow As New SystemSound("EVT_ui_photo_show")
        Public Shared ReadOnly Property RbpBack As New SystemSound("EVT_ui_rbp_back")
        Public Shared ReadOnly Property RbpOpen As New SystemSound("EVT_ui_rbp_open")
        Public Shared ReadOnly Property SettingComboboxClick As New SystemSound("EVT_ui_setting_combobox_click")
        Public Shared ReadOnly Property SettingConfigkeyDeny As New SystemSound("EVT_ui_setting_configkey_deny")
        Public Shared ReadOnly Property SettingConfigkeySucess As New SystemSound("EVT_ui_setting_configkey_sucess")
        Public Shared ReadOnly Property SettingListbtnLongClick As New SystemSound("EVT_ui_setting_listbtn_long_click")
        Public Shared ReadOnly Property SettingSliderSlide As New SystemSound("EVT_ui_setting_slider_slide")
        Public Shared ReadOnly Property SettingSwitcherSwitch As New SystemSound("EVT_ui_setting_switcher_switch")
        Public Shared ReadOnly Property ShopswitchSlide As New SystemSound("EVT_ui_shopswitch_slide")
        Public Shared ReadOnly Property SubtabSwitch As New SystemSound("EVT_ui_subtab_switch")
        Public Shared ReadOnly Property TabSwitch As New SystemSound("EVT_ui_tab_switch")
        Public Shared ReadOnly Property TalentAbilityitemForget As New SystemSound("EVT_ui_talent_abilityitem_forget")
        Public Shared ReadOnly Property TalentAbilityitemForgetLongpress As New SystemSound("EVT_ui_talent_abilityitem_forget_longpress")
        Public Shared ReadOnly Property TalentAbilityitemForgetLongpressStop As New SystemSound("EVT_ui_talent_abilityitem_forget_longpress_stop")
        Public Shared ReadOnly Property TalentAllForgetLongpressHold As New SystemSound("EVT_ui_talent_all_forget_longpress_hold")
        Public Shared ReadOnly Property TalentAllForgetLongpressRelease As New SystemSound("EVT_ui_talent_all_forget_longpress_release")
        Public Shared ReadOnly Property TalentAllForgetLongpressSuccess As New SystemSound("EVT_ui_talent_all_forget_longpress_success")
        Public Shared ReadOnly Property TalentitemAutoactive As New SystemSound("EVT_ui_talent_talentitem_autoactive")
        Public Shared ReadOnly Property TalentitemForget As New SystemSound("EVT_ui_talent_talentitem_forget")
        Public Shared ReadOnly Property TalentitemForgetDeny As New SystemSound("EVT_ui_talent_talentitem_forget_deny")
        Public Shared ReadOnly Property TalentitemLearn As New SystemSound("EVT_ui_talent_talentitem_learn")
        Public Shared ReadOnly Property TitleEntergame As New SystemSound("EVT_ui_title_entergame")
        Public Shared ReadOnly Property TitleSplistbtnClick As New SystemSound("EVT_ui_title_splistbtn_click")
        Public Shared ReadOnly Property TravelnotesCollectionDetailClose As New SystemSound("EVT_ui_travelnotes_collection_detail_close")
        Public Shared ReadOnly Property TravelnotesCollectionDetailOpen As New SystemSound("EVT_ui_travelnotes_collection_detail_open")
        Public Shared ReadOnly Property TravelnotesCollectionHbpicShow As New SystemSound("EVT_ui_travelnotes_collection_hbpic_show")
        Public Shared ReadOnly Property TravelnotesCollectionMarkShow As New SystemSound("EVT_ui_travelnotes_collection_mark_show")
        Public Shared ReadOnly Property TravelnotesPictabSwitch As New SystemSound("EVT_ui_travelnotes_pictab_switch")
        Public Shared ReadOnly Property BtnRefineShow As New SystemSound("UI_Btn_Refine_Show")
        Public Shared ReadOnly Property DropPopupClose As New SystemSound("UI_Drop_Popup_Close")
        Public Shared ReadOnly Property DropPopupShow As New SystemSound("UI_Drop_Popup_Show")
        Public Shared ReadOnly Property GlobalDetailsClose As New SystemSound("UI_Global_Details_Close")
        Public Shared ReadOnly Property GlobalDetailsOpen As New SystemSound("UI_Global_Details_Open")
        Public Shared ReadOnly Property GlobalDisappear As New SystemSound("UI_Global_Disappear")
        Public Shared ReadOnly Property GlobalShow As New SystemSound("UI_Global_Show")
        Public Shared ReadOnly Property HeroFilesScrollingStart As New SystemSound("ui_hero_files_scrolling_start")
        Public Shared ReadOnly Property ItemSquareitemFocus As New SystemSound("UI_Item_SquareItem_Focus")
        Public Shared ReadOnly Property ShopStackDecrease As New SystemSound("UI_Shop_Stack_Decrease")
        Public Shared ReadOnly Property ShopStackIncrease As New SystemSound("UI_Shop_Stack_Increase")
        Public Shared ReadOnly Property SkillpointLearnRelease As New SystemSound("UI_Skill_Skillpoint_Learn_Release")
        Public Shared ReadOnly Property SkillpointLearnSucess As New SystemSound("UI_Skill_Skillpoint_Learn_Sucess")

        Private Sub New(soundName As String)
            _soundName = soundName
        End Sub

        Public Overrides Function ToString() As String
            Return _soundName
        End Function
    End Class
End Namespace
