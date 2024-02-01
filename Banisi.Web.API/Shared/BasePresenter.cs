using Microsoft.AspNetCore.Mvc;

namespace Banisi.Web.API.Shared
{
    public class BasePresenter
    {
        public ActionResult ActionResult { get; set; }

        public void NotFound()
        {
            ActionResult = new NotFoundResult();
        }

        public void SetOkObject(object data)
        {
            ActionResult = new OkObjectResult(data);
        }

        public void SetOk()
        {
            ActionResult = new OkResult();
        }

        public void SetBadRequestWithMessage(string message)
        {
            ActionResult = new BadRequestObjectResult(message);
        }

        public void SetBadRequestObject(object obj)
        {
            ActionResult = new BadRequestObjectResult(obj);
        }

        public void SetUnauthorizedWithMessage(string message)
        {
            ActionResult = new UnauthorizedObjectResult(message);
        }
    }
}
