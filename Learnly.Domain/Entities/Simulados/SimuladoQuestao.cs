namespace Learnly.Domain.Entities.Simulados
{
    public class SimuladoQuestao
    {
        public int SimuladoQuestaoId { get; set; }
        public int SimuladoId { get; set; }
        public Simulado Simulado { get; set; }

        public int QuestaoId { get; set; }
        public Questao Questao { get; set; }

    }
}
