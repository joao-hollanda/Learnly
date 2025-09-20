using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Learnly.Domain.Entities;

namespace Learnly.Repository.Config
{
       public class PlanoEstudoConfig : IEntityTypeConfiguration<PlanoEstudo>
       {
              public void Configure(EntityTypeBuilder<PlanoEstudo> builder)
              {
                     builder.ToTable("PlanosEstudo");

                     builder.HasKey(p => p.PlanoId);

                     builder.Property(p => p.Objetivo)
                            .IsRequired()
                            .HasMaxLength(200);

                     builder.Property(p => p.DataInicio)
                            .IsRequired();

                     builder.Property(p => p.DataFim)
                            .IsRequired();

                     builder.Property(p => p.HorasPorSemana)
                            .IsRequired();

                     builder.Property(p => p.StatusPlano)
                            .IsRequired();

              }
       }
}
