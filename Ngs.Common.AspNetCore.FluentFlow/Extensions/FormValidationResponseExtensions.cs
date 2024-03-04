using Ngs.Common.AspNetCore.FluentFlow.Resp;

namespace Ngs.Common.AspNetCore.FluentFlow.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="FormFluentResponse"/>.
    /// </summary>
    public static class FormValidationResponseExtensions
    {
        /// <summary>
        /// Checks if the fluentFluentResponse is successful to create a scenario where the fluentFluentResponse is successful.
        /// </summary>
        /// <param name="fluentResponse"></param>
        /// <returns></returns>
        public static FormFluentResponse IsSucceeded(this FormFluentResponse fluentResponse)
        {
            return !fluentResponse.IsSuccess ? new FormFluentResponse() : fluentResponse;
        }
        
        /// <summary>
        /// Checks if the fluentFluentResponse is not successful to create a scenario where the fluentFluentResponse is not successful.
        /// </summary>
        /// <param name="fluentResponse"></param>
        /// <returns></returns>
        public static FormFluentResponse IsNotSucceeded(this FormFluentResponse fluentResponse)
        {
            return fluentResponse.IsSuccess ? new FormFluentResponse() : fluentResponse;
        }
    }
}