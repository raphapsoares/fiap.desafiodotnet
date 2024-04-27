using fiap.testedotnet.ui.Configurations;
using fiap.testedotnet.ui.Models;
using fiap.testedotnet.ui.Models.Turma;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace fiap.testedotnet.ui.Pages.Turma
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
        public List<TurmaViewModel> Turmas { get; set; }
        List<string> MensagensDeErro { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync($"{_appSettings.ApiBaseUrl}/Turma");


            if (response.StatusCode == System.Net.HttpStatusCode.OK || 
                response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
            {
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResponseApi<List<TurmaViewModel>>>(content);

                if (resultado?.Dados != null)
                    Turmas = resultado?.Dados ?? new List<TurmaViewModel>();
                else
                    Turmas = new List<TurmaViewModel>();

                if(!resultado.Sucesso && resultado?.Mensagens != null && resultado?.Mensagens.Count > 0)
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

        public async Task<IActionResult> OnPostDelete(int id)
        {

            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.DeleteAsync($"{_appSettings.ApiBaseUrl}/Turma/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Turma deletada com sucesso, você pode redirecionar para a página de listagem novamente ou fazer outra ação
                return RedirectToPage("./Index");
            }
            else
            {
                // Se houver um erro ao deletar a turma, você pode lidar com ele aqui
                // Por exemplo, redirecionar para uma página de erro
                return RedirectToPage("/Error");
            }
        }

    }
}
