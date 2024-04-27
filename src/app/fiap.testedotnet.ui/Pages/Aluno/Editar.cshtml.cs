using fiap.testedotnet.ui.Configurations;
using fiap.testedotnet.ui.Models;
using fiap.testedotnet.ui.Models.Aluno;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace fiap.testedotnet.ui.Pages.Aluno
{
    public class EditarModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly AppSettings _appSettings;

        public EditarModel(
            IHttpClientFactory clientFactory,
            IOptions<AppSettings> appSettings)
        {
            _clientFactory = clientFactory;
            _appSettings = appSettings.Value;
        }

        [BindProperty]
        public AtualizarAlunoRequestModel Request { get; set; }
        private List<string> MensagensDeErro { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var httpClient = _clientFactory.CreateClient();

            var jsonBody = JsonConvert.SerializeObject(Request);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync($"{_appSettings.ApiBaseUrl}/Aluno", content);

            if (response.IsSuccessStatusCode)
                return RedirectToPage("./Index");

            if (!response.IsSuccessStatusCode)
            {
                var conteudo = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResponseApi<AlunoViewModel>>(conteudo);

                if (resultado != null
                    && !resultado.Sucesso
                    && resultado?.Mensagens != null
                    && resultado?.Mensagens.Count > 0)
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
