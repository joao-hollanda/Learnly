using Learnly.Domain.Entities.Simulados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learnly.Repository.Config
{
    public class AlternativaConfig : IEntityTypeConfiguration<Alternativa>
    {
        public void Configure(EntityTypeBuilder<Alternativa> builder)
        {
            builder.ToTable("Alternativas");

            builder.HasKey(a => a.AlternativaId);

            builder.Property(a => a.Letra)
                   .IsRequired()
                   .HasMaxLength(1);

            builder.Property(a => a.Texto)
                   .IsRequired()
                   .HasColumnType("TEXT");

            builder.Property(a => a.Correta)
                   .IsRequired();

            builder.HasOne(a => a.Questao)
                   .WithMany(q => q.Alternativas)
                   .HasForeignKey(a => a.QuestaoId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
