using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using consumindoIA.Domain;
using Learnly.Services.Interfaces;
using Newtonsoft.Json;

namespace Learnly.Services.IAService
{
    public class IAService : IIAService
    {
        private readonly HttpClient _httpClient;
        public IAService(string apiKey)
        {
            _httpClient = new();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        }
        public async Task<string> GerarFeedbackAsync(Simulado simulado)
        {
            try
            {
                var jsonSimulado = JsonConvert.SerializeObject(simulado, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                var mensagens = new List<Message>{
                    new Message
                    {
                        role = "system",
                        content = "Você é um avaliador educacional que gera feedbacks curtos e objetivos sobre simulados. Foque em fornecer um panorama geral do desempenho do usuário, destacando pontos fortes e pontos a melhorar, sem entrar em detalhes de cada questão."
                    },
                    new Message
                    {
                        role = "user",
                        content = $"\n {jsonSimulado} Gere um feedback simples em formato de panorama geral, com no máximo 3 parágrafos."
                    },
                };

                var request = new ChatRequest
                {
                    messages = mensagens,
                    model = "moonshotai/kimi-k2-instruct",
                    temperature = 0.6,
                };

                var json = JsonConvert.SerializeObject(request);
                var corpo = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync("https://api.groq.com/openai/v1/chat/completions", corpo);

                if (!response.IsSuccessStatusCode) return "Houve um erro ao fazer a requisição, tente novamente mais tarde";

                var respostaJson = await response.Content.ReadAsStringAsync();
                var resposta = JsonConvert.DeserializeObject<ChatResponse>(respostaJson);

                return resposta.choices[0].message.content;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}