using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace RazorPages
{
    /// <summary>
    /// This class is the code behind of the Contacts.cshtml page
    /// </summary>
    public class Contacts: PageModel
    {
        /// <summary>
        /// This property can be used in the page using @Model.Message
        /// </summary>
        public string Message { get; private set; } = "Contacts page: ";

        /// <summary>
        /// This property can be used in the page using @Model.Name
        /// </summary>
        public string Name { get; private set; } = "Yassine BENABBAS";

        /// <summary>
        /// In this function, we can update properties before returning them to the page
        /// </summary>
        public void OnGet()
        {
            Message += $" { DateTime.Now }";
        }
    }
}