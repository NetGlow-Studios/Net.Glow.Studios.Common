namespace Ngs.Common.AspNetCore.AccessControl.Enums.Privileges;

public enum RoleFeaturePrivilegesEnum
{
    //Calendar
    //-------------------------
    PrivilegeCalendarAccess = 1,
    PrivilegeCalendarEntryCreate = 2,
    PrivilegeCalendarEntryNotification = 3,
    PrivilegeCalendarAccessToAllEntries = 4,
    //-------------------------

    //Bible
    //-------------------------
    //PrivilegeBibleFormat = 10, //bookmarks, verse highlight, etc...
    //-------------------------

    //Songbook
    //-------------------------
    //PrivilegeSongbookAccess = 20,
    // PrivilegeSongbookFormat = 21, //bookmarks, favourites, etc...
    //-------------------------

    //Service Creator
    //-------------------------
    // PrivilegeServiceCreatorAccess = 30
    //-------------------------
}