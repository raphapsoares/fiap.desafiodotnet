using fiap.testedotnet.ui.Configurations;
using fiap.testedotnet.ui.Models;
using fiap.testedotnet.ui.Models.Aluno;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace fiap.testedotnet.ui.Pages.Aluno
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
        public List<AlunoViewModel> Alunos { get; set; }
        private List<string> MensagensDeErro { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync($"{_appSettings.ApiBaseUrl}/Aluno");


            if (response.StatusCode == System.Net.HttpStatusCode.OK ||
                response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
            {
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResponseApi<List<AlunoViewModel>>>(content);

                if (resultado?.Dados != null)
                    Alunos = resultado?.Dados ?? new List<AlunoViewModel>();
                else
                    Alunos = new List<AlunoViewModel>();

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
        public async Task<IActionResult> OnPostDelete(int id)
        {

            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.DeleteAsync($"{_appSettings.ApiBaseUrl}/Aluno/{id}");

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
