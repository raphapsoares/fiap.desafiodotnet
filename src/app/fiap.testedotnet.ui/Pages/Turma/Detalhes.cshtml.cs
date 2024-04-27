using fiap.testedotnet.ui.Configurations;
using fiap.testedotnet.ui.Models;
using fiap.testedotnet.ui.Models.Turma;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace fiap.testedotnet.ui.Pages.Turma
{
    public class DetalhesModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly AppSettings _appSettings;

        public DetalhesModel(
            IHttpClientFactory clientFactory,
            IOptions<AppSettings> appSettings)
        {
            _clientFactory = clientFactory;
            _appSettings = appSettings.Value;
        }
        public TurmaViewModel TurmaViewModel { get; set; }
        
        private List<string> MensagensDeErro { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync($"{_appSettings.ApiBaseUrl}/Turma/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK ||
               response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
            {
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResponseApi<TurmaViewModel>>(content);

                if (resultado != null && resultado.Dados != null)
                    TurmaViewModel = resultado.Dados;
                else
                    TurmaViewModel = new TurmaViewModel();

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

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                return RedirectToPage("/Error");

            return Page();

        }
    }
}
