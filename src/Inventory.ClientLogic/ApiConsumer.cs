using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ClientLogic
{
    public class ApiConsumer(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        
    }
}
