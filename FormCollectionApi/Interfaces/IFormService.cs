using FormCollectionApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FormCollectionApi.Interfaces
{
    public interface IFormService
    {
        Task<IActionResult> SaveUserFeedBack(UserFeedBackForm feedBack);

        Task<IActionResult> GetProductsList();
    }
}
