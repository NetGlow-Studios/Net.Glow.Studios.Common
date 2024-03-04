namespace Ngs.Common.AspNetCore.AccessControl.Enums.Privileges;

public enum RoleManagementPrivilegesEnum
{
    //Home Management
    //-------------------------
    PrivilegeHomeManagement = 1,
    
    PrivilegeHomeNewsPublish = 2,
    PrivilegeHomeNewsRemove = 3,
    PrivilegeHomeEventsPublish = 4,
    PrivilegeHomeEventsRemove = 5,
    PrivilegeHomeAnnouncementsPublish = 6,
    PrivilegeHomeAnnouncementsRemove = 7,
    //-------------------------
    
    //Contact Management
    //-------------------------
    PrivilegeContactManagement = 10,
    
    PrivilegeContactInfoUpdate = 11, // Update contact data (info, social media, map, etc...)
    PrivilegeContactFormUpdate = 12, // Update form data
    //-------------------------
    
    //AboutUs Management
    //-------------------------
    PrivilegeAboutUsManagement = 20,
    
    PrivilegeAboutUsInfoUpdate = 21,
    PrivilegeAboutUsGalleryUpdate = 22,
    //-------------------------
    
    //Bible Management
    //-------------------------
    PrivilegeBibleManagement = 30,
    
    PrivilegeBibleImport = 31,
    PrivilegeBibleExport = 32,
    PrivilegeBibleRemove = 33,
    PrivilegeBibleUpdateVisibility = 34,
    PrivilegeBibleSetPrimary = 35,
    //-------------------------
    
    //Message Management
    //-------------------------
    PrivilegeMessageManagement = 40,
    PrivilegeMessageImport = 41,
    PrivilegeMessageExport = 42,
    PrivilegeMessageRemove = 43,
    PrivilegeMessageUpdateVisibility = 44,
    //-------------------------
    
    //User Management
    //-------------------------
    PrivilegeUserManagement = 50,
    
    PrivilegeUserAccountConfirmation = 51,
    PrivilegeUserAccountRemove = 52,
    PrivilegeUserAccountBan = 53,
    //-------------------------
    
    //Role Management
    //-------------------------
    PrivilegeRoleManagement = 60,
    
    PrivilegeRoleCreate = 61,
    PrivilegeRoleRemove = 62,
    PrivilegeRoleAssign = 63,
    //-------------------------
    
    //Explore Management
    //-------------------------
    
    PrivilegeHomeExploreManagement = 70,
    
    PrivilegeHomeExploreCreate = 71,
    PrivilegeHomeExploreRemove = 72,
    PrivilegeHomeExploreUpdateVisibility = 73,
    //-------------------------
    
    //Calendar Management
    //-------------------------
    PrivilegeCalendarManagement = 80,
    //-------------------------
    
    
    //App Configuration Management
    //-------------------------
    PrivilegeAppConfigurationManagement = 90,
    
    PrivilegeAppConfigurationSmtpUpdate = 91,
    PrivilegeAppConfigurationSmsUpdate = 92,
    //-------------------------
    
    //Songbook
    //-------------------------
    PrivilegeSongbookManagement = 100,
    PrivilegeSongbookUpdateSongVisibility = 101,
    PrivilegeSongbookUpdate = 102,
    PrivilegeSongbookRemove = 103,
    PrivilegeSongbookSongUpdate = 104,
    PrivilegeSongbookSongRemove = 105
    //-------------------------
}