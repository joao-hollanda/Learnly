using Learnly.Domain.Entities.Simulados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Learnly.Domain.Enums;
using System.Linq;

namespace Learnly.Repository.Config
{
       public class QuestaoConfig : IEntityTypeConfiguration<Questao>
       {
              public void Configure(EntityTypeBuilder<Questao> builder)
              {
                     builder.ToTable("Questoes");

                     builder.HasKey(q => q.QuestaoId);

                     builder.Property(q => q.Titulo)
                            .IsRequired()
                            .HasMaxLength(500);

                     builder.Property(q => q.Disciplina)
                            .IsRequired()
                            .HasMaxLength(100);

                     builder.Property(q => q.Lingua)
                            .IsRequired()
                            .HasMaxLength(50);

                     builder.Property(q => q.Contexto)
                            .HasColumnType("TEXT");

                     builder.Property(q => q.Arquivos)
                            .HasColumnType("TEXT");


                     builder.Property(q => q.AlternativaCorreta)
                            .IsRequired()
                            .HasMaxLength(1);


                     builder.HasMany(q => q.Alternativas)
                            .WithOne(a => a.Questao)
                            .HasForeignKey(a => a.QuestaoId)
                            .OnDelete(DeleteBehavior.Cascade);
              }
       }
}
