namespace BookShop.Books.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Refit;

    public abstract class BaseController : Controller
    {
        protected async Task<IActionResult> Handle(Func<Task> action, ActionResult success, ActionResult failure)
        {
            try
            {
                await action();
                return success;
            }
            catch (ApiException exception)
            {
                this.ProcessErrors(exception);
                return failure;
            }
        }

        protected async Task<bool> Handle(Func<Task> action)
        {
            try
            {
                await action();
                return true;
            }
            catch (ApiException)
            {
                return false;
            }
        }

        private void ProcessErrors(ApiException exception)
        {
            if (exception.HasContent)
            {
                JsonConvert
                    .DeserializeObject<List<string>>(exception.Content)
                    .ForEach(error => this.ModelState.AddModelError(string.Empty, error));
            }
            else
            {
                this.ModelState.AddModelError(string.Empty, "Internal server error.");
            }
        }
    }
}