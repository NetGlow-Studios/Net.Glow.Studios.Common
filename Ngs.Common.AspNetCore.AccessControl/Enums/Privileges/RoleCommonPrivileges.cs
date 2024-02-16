namespace Ngs.Common.AspNetCore.AccessControl.Enums.Privileges;

public enum RoleCommonPrivileges
{
    //Newsletter
    //-------------------------
    PrivilegeNewsletterAccess = 1,
    PrivilegeNewsletterEmailSubscription = 2,
    PrivilegeNewsletterSmsSubscription = 3,
    //-------------------------
    
    //AboutUs
    //-------------------------
    PrivilegeAboutUsAccess = 10,
    //-------------------------
    
    //Contact
    //-------------------------
    PrivilegeContactAccess = 20,
    PrivilegeContactForm = 21,
    //-------------------------
    
    //Explores 
    //-------------------------
    PrivilegeExploresAccess = 30,
    //-------------------------
    
    //Bible
    //-------------------------
    PrivilegeBibleAccess = 40,
    PrivilegeBibleSearchAccess = 41,
    PrivilegeBibleCompareAccess = 42,
    //-------------------------
    
    //Message
    //-------------------------
    PrivilegeMessageAccess = 50,
    PrivilegeMessageDownloadPdfAccess = 51,
    PrivilegeMessageDownloadAudioAccess = 52,
    //-------------------------
}