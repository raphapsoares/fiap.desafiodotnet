using fiap.testedotnet.ui.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using fiap.testedotnet.ui.Models.Relacionamento;
using fiap.testedotnet.ui.Configurations;
using Microsoft.Extensions.Options;

namespace fiap.testedotnet.ui.Pages.Relacionamento
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly AppSettings _appSettings;

        public IndexModel(
            IHttpClientFactory clientFactory,
            IOptions<AppSettings> appSettings)
        {
            _clientFactory = clientFactory;
            _appSettings = appSettings.Value;
        }
        public List<RelacionamentoViewModel> Relacionamentos { get; set; }
        private List<string> MensagensDeErro { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync($"{_appSettings.ApiBaseUrl}/Relacionamento");

            if (response.StatusCode == System.Net.HttpStatusCode.OK ||
                response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
            {
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResponseApi<List<RelacionamentoViewModel>>>(content);

                if (resultado?.Dados != null)
                    Relacionamentos = resultado?.Dados ?? new List<RelacionamentoViewModel>();
                else
                    Relacionamentos = new List<RelacionamentoViewModel>();

                if (!resultado.Sucesso && resultado?.Mensagens != null && resultado?.Mensagens.Count > 0)
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

        public async Task<IActionResult> OnPostDelete(int id1, int id2)
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.DeleteAsync($"{_appSettings.ApiBaseUrl}/Relacionamento/aluno/{id1}/turma/{id2}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
