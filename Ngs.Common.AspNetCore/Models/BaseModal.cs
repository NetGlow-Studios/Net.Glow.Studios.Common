namespace Ngs.Common.AspNetCore.Models;

/// <summary>
/// Base class for all modals, configure common properties here
/// </summary>
public abstract class BaseModal
{
   /// <summary>
   /// Disable outside click to close modal
   /// </summary>
   public bool IsStatic { get; set; }
   
   /// <summary>
   /// Use keyboard to close modal
   /// </summary>
   public bool UseKeyboard { get; set; } = true;
   
   /// <summary>
   /// Show close button
   /// </summary>
   public bool UseCloseButton { get; set; } = true;
   
   /// <summary>
   /// Modal id
   /// </summary>
   public string ModalId { get; set; } = "modal-to-show";
}