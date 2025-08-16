using Auth.ApiConsumer;
using Inventory.ClientLogic;

namespace Inventory.Website.Services
{
    public class WebApiHandler(AuthService service) : ApiHandler(service)
    {

        //protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        //{
        //    var res = await base.SendAsync(request, cancellationToken);

        //    if ()

        //    return res;
        //}

    }
}
