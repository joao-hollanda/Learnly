using Learnly.Domain.Entities.Simulados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learnly.Repository.Config
{
    public class SimuladoQuestaoConfig : IEntityTypeConfiguration<SimuladoQuestao>
    {
        public void Configure(EntityTypeBuilder<SimuladoQuestao> builder)
        {
            builder.ToTable("SimuladoQuestoes");

            builder.HasKey(sq => sq.SimuladoQuestaoId);

            builder.HasOne(sq => sq.Simulado)
                   .WithMany(s => s.Questoes)
                   .HasForeignKey(sq => sq.SimuladoId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sq => sq.Questao)
                   .WithMany(q => q.Simulados)
                   .HasForeignKey(sq => sq.QuestaoId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
