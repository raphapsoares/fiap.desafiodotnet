using fiap.testedotnet.ui.Models.Turma;
using fiap.testedotnet.ui.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using fiap.testedotnet.ui.Models.Aluno;
using fiap.testedotnet.ui.Models.Relacionamento;
using fiap.testedotnet.ui.Configurations;
using Microsoft.Extensions.Options;

namespace fiap.testedotnet.ui.Pages.Relacionamento
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
        public List<AlunoViewModel> Alunos { get; set; }

        [BindProperty]
        public List<TurmaViewModel> Turmas { get; set; }
        
        [BindProperty]
        public CriarRelacionamentoRequestModel Request { get ; set; }
        private List<string> MensagensDeErro { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var httpClient = _clientFactory.CreateClient();

            await RetornarComboAlunos(httpClient);
            await RetornarComboTurmas(httpClient);

            TempData["MensagensDeErro"] = MensagensDeErro;

            if (Alunos == null || Turmas == null)
                return RedirectToPage("/Error");

            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            var httpClient = _clientFactory.CreateClient();

            var jsonBody = JsonConvert.SerializeObject(Request);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{_appSettings.ApiBaseUrl}/Relacionamento", content);

            if (response.IsSuccessStatusCode)
                return RedirectToPage("./Index");

            if (!response.IsSuccessStatusCode)
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
        
        private async Task RetornarComboAlunos(HttpClient httpClient)
        {
            var response = await httpClient.GetAsync($"{_appSettings.ApiBaseUrl}/Aluno");

            if (response.StatusCode == System.Net.HttpStatusCode.OK ||
               response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
            {
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResponseApi<List<AlunoViewModel>>>(content);

                if (resultado != null && resultado.Dados != null)
                    Alunos = resultado.Dados;
                else
                    Alunos = new List<AlunoViewModel>();

                if (resultado != null
                    && !resultado.Sucesso
                    && resultado?.Mensagens != null
                    && resultado?.Mensagens.Count > 0)
                {
                    if (MensagensDeErro == null)
                        MensagensDeErro = new List<string>();

                    MensagensDeErro.AddRange(resultado.Mensagens);
                }
            }
        }
        
        private async Task RetornarComboTurmas(HttpClient httpClient)
        {
            var response = await httpClient.GetAsync($"{_appSettings.ApiBaseUrl}/Turma");

            if (response.StatusCode == System.Net.HttpStatusCode.OK ||
               response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
            {
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResponseApi<List<TurmaViewModel>>>(content);

                if (resultado != null && resultado.Dados != null)
                    Turmas = resultado.Dados;
                else
                    Turmas = new List<TurmaViewModel>();

                if (resultado != null
                    && !resultado.Sucesso
                    && resultado?.Mensagens != null
                    && resultado?.Mensagens.Count > 0)
                {
                    if (MensagensDeErro == null)
                        MensagensDeErro = new List<string>();

                    MensagensDeErro.AddRange(resultado.Mensagens);
                }
            }
        }
    }
}
