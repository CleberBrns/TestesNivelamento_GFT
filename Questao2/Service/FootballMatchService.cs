using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Questao2.Models.Result;

namespace Questao2.Service
{
    public class FootballMatchService
    {
        public static async Task<List<FootballMatchResult>> GetFootballMatches(string team, int teamNumber, int year)
        {
            var rootballMatches = new List<FootballMatchResult>();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://jsonmock.hackerrank.com");            

            int paginaRequest = 1;
            //É preciso contar o Looping com um número a menos que o total de páginas, para poder passar em todas as páginas
            int contadorLooping = 0;
            int ultimaPagina = 0;
            bool sair = false;

            do
            {
                await httpClient.GetAsync(GetNextUrl(year, teamNumber, team, paginaRequest))
                    .ContinueWith(async (jobSearchTask) =>
                    {
                        var response = await jobSearchTask;
                        if (response.IsSuccessStatusCode)
                        {
                            string jsonString = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<PageResult>(jsonString);
                            if (result != null)
                            {
                                if (result.data.Any())
                                    rootballMatches.AddRange(result.data.ToList());
                                                                
                                if (ultimaPagina == 0)
                                {
                                    ultimaPagina = result.total_pages;
                                }

                                paginaRequest = Convert.ToInt32(result.page) + 1;
                                contadorLooping++;
                            }
                        }
                        else
                        {
                            sair = true;
                        }
                    });

            } while (sair || contadorLooping != ultimaPagina);

            return rootballMatches;
        }

        private static string GetNextUrl(int year, int teamNumber, string team, int proximaPagina)
        {
            return $"/api/football_matches?year={year}&team{teamNumber}={team?.Trim()}&page={proximaPagina}";
        }
    }
}
