using fiap.testedotnet.ui.Configurations;
using fiap.testedotnet.ui.Models;
using fiap.testedotnet.ui.Models.Turma;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace fiap.testedotnet.ui.Pages.Turma
{
    public class CriarModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly AppSettings _appSettings;

        public CriarModel(
            IHttpClientFactory clientFactory,
            IOptions<AppSettings> appSettings)
        {
            _clientFactory = clientFactory;
            _appSettings = appSettings.Value;
        }

        [BindProperty]
        public CriarTurmaRequestModel TurmaRequest { get; set; }
        
        private List<string> MensagensDeErro { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var httpClient = _clientFactory.CreateClient();

            var jsonTurma = JsonConvert.SerializeObject(TurmaRequest);
            var content = new StringContent(jsonTurma, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{_appSettings.ApiBaseUrl}/Turma", content);

            if (response.IsSuccessStatusCode)
                return RedirectToPage("./Index");

            if(!response.IsSuccessStatusCode)
            {
                var conteudo = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResponseApi<TurmaViewModel>>(conteudo);

                if (!resultado.Sucesso && resultado?.Mensagens != null && resultado?.Mensagens.Count > 0)
                {
                    MensagensDeErro = new List<string>();
                    MensagensDeErro.AddRange(resultado.Mensagens);
                }

                TempData["MensagensDeErro"] = MensagensDeErro;
            }

            return Page();
        }
    }
}
