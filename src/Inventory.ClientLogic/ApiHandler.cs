using Inventory.ClientLogic.Exceptions;
using Inventory.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ClientLogic
{
    public class ApiHandler : HttpClientHandler
    {

        private const string TYPE_HEADER = "exception-type";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var res = await base.SendAsync(request, cancellationToken);
            if (res.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var headerErrors = res.Headers.GetValues(TYPE_HEADER);

                if (headerErrors.Count() == 1)
                {
                    var errorHeader = headerErrors.SingleOrDefault();

                    Exception? ex = null;

                    if (request.Content is not null)
                    {
                        var content = request.Content;

                        switch (errorHeader)
                        {
                            case nameof(SingleErrorException):
                                var errorStr = await content.ReadAsStringAsync(cancellationToken);
                                ex = new SingleErrorException(errorStr);
                                break;
                            case nameof(MultiErrorsException):
                                var messages = await content.ReadFromJsonAsync<IEnumerable<string>>(cancellationToken);
                                ex = new MultiErrorsException(messages ?? []);
                                break;
                        }

                        if (ex is not null) throw ex;
                    }
                    
                }

                throw new UnknownBadRequestException();
            }
            return res;
        }

    }
}
