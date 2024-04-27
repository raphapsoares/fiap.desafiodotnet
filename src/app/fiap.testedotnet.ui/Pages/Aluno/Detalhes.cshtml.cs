using fiap.testedotnet.ui.Configurations;
using fiap.testedotnet.ui.Models;
using fiap.testedotnet.ui.Models.Aluno;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace fiap.testedotnet.ui.Pages.Aluno
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
        public AlunoViewModel Aluno { get; set; }
        private List<string> MensagensDeErro { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync($"{_appSettings.ApiBaseUrl}/Aluno/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK ||
               response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
            {
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResponseApi<AlunoViewModel>>(content);

                if (resultado != null && resultado.Dados != null)
                    Aluno = resultado.Dados;
                else
                    Aluno = new AlunoViewModel();

                if (resultado != null
                    && !resultado.Sucesso
                    && resultado?.Mensagens != null
                    && resultado?.Mensagens.Count > 0)
                {
                    MensagensDeErro = new List<string>();
                    MensagensDeErro.AddRange(resultado.Mensagens);
                }
                // Armazena as mensagens de erro no TempData
                TempData["MensagensDeErro"] = MensagensDeErro;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                return RedirectToPage("/Error");

            return Page();

        }
    }
}
