using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Aurora.Models;
using System.Net.Http;

namespace Aurora.Pages.Bears
{
    public class IndexModel : PageModel
    {
        private readonly Aurora.Models.AuroraContext _context;
        private readonly IHttpClientFactory _clientFactory;
        public IndexModel(Aurora.Models.AuroraContext context, IHttpClientFactory clientFactory)
        {
            _context = context;
            _clientFactory = clientFactory;
        }

        public IList<PolarBear> PolarBear { get;set; }

        public async Task OnGetAsync()
        {
            PolarBear = await _context.PolarBear.ToListAsync();

            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://api.github.com/repos/aspnet/docs/branches");
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            //Create client
            var client = _clientFactory.CreateClient();

            //Send request, and get response
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {

            }
            else
            {

            }
        }
    }
}
