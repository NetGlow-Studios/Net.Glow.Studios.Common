using Ngs.Common.AspNetCore.FluentFlow.Resp;

namespace Ngs.Common.AspNetCore.FluentFlow.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="FormValidationFluentResponse"/>.
    /// </summary>
    public static class FormValidationResponseExtensions
    {
        /// <summary>
        /// Checks if the fluentFluentResponse is successful to create a scenario where the fluentFluentResponse is successful.
        /// </summary>
        /// <param name="fluentResponse"></param>
        /// <returns></returns>
        public static FormValidationFluentResponse IsSucceeded(this FormValidationFluentResponse fluentResponse)
        {
            return !fluentResponse.IsSuccess ? new FormValidationFluentResponse() : fluentResponse;
        }
        
        /// <summary>
        /// Checks if the fluentFluentResponse is not successful to create a scenario where the fluentFluentResponse is not successful.
        /// </summary>
        /// <param name="fluentResponse"></param>
        /// <returns></returns>
        public static FormValidationFluentResponse IsNotSucceeded(this FormValidationFluentResponse fluentResponse)
        {
            return fluentResponse.IsSuccess ? new FormValidationFluentResponse() : fluentResponse;
        }
    }
}