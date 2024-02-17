using Ngs.Common.AspNetCore.FluentFlow.Models;

namespace Ngs.Common.AspNetCore.FluentFlow.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="FormValidationResponse"/>.
    /// </summary>
    public static class FormValidationResponseExtensions
    {
        /// <summary>
        /// Checks if the response is successful to create a scenario where the response is successful.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static FormValidationResponse IsSucceeded(this FormValidationResponse response)
        {
            return !response.IsSuccess ? new FormValidationResponse() : response;
        }
        
        /// <summary>
        /// Checks if the response is not successful to create a scenario where the response is not successful.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static FormValidationResponse IsNotSucceeded(this FormValidationResponse response)
        {
            return response.IsSuccess ? new FormValidationResponse() : response;

        }
    }
}