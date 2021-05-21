using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FarmApp.Areas.Identity.Pages.Account.Manage
{
    /// <summary>
    /// Helping class for routing for change user information.
    /// </summary>
    public static class ManageNavPages
    {
        /// <summary>
        /// Index.
        /// </summary>
        public static string Index => "Index";

        /// <summary>
        /// Billing information.
        /// </summary>
        public static string BillingInformation => "BillingInformation";

        /// <summary>
        /// Method for navigate to index.
        /// </summary>
        /// <param name="viewContext">Context.</param>
        /// <returns>Page address.</returns>
        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        /// <summary>
        /// Method for navigate to billing information.
        /// </summary>
        /// <param name="viewContext">Context.</param>
        /// <returns>Page address.</returns>
        public static string BillingInformationNavClass(ViewContext viewContext) => PageNavClass(viewContext, BillingInformation);

        /// <summary>
        /// Method for navigate to some page.
        /// </summary>
        /// <param name="viewContext">Context.</param>
        /// <param name="page">Page to navigate.</param>
        /// <returns>Page address.</returns>
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
