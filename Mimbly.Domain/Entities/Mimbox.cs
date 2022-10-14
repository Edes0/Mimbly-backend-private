namespace Mimbly.Domain.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table("Mimbox")]
public class Mimbox
{
    [Key] //TODO: Check if needed
    [Column("Id", TypeName = "uniqueidentifier", Order = 1)]
    public Guid Id { get; set; }

    [Column("Water", TypeName = "float")]
    public float Water { get; set; } = 0;

    [Column("Co2", TypeName = "float")]
    public float Co2 { get; set; } = 0;

    [Column("Plastic", TypeName = "float")]
    public float Plastic { get; set; } = 0;

    [Column("Economy", TypeName = "float")]
    public float Economy { get; set; } = 0;

    [Column("Mimbox_Status_Id", TypeName = "uniqueidentifier")]
    public Guid StatusId { get; set; }

    [Column("Mimbox_Model_Id", TypeName = "uniqueidentifier")]
    public Guid ModelId { get; set; }

    [Column("Mimbox_Location_Id", TypeName = "uniqueidentifier")]
    public Guid? LocationId { get; set; }

    [Column("Company_Id", TypeName = "uniqueidentifier")]
    public Guid? CompanyId { get; set; }

    public ICollection<MimboxLog>? MimboxLogList { get; } = new List<MimboxLog>();

    public MimboxStatus Status { get; set; }

    public MimboxModel Model { get; set; }

    public Location Location { get; set; }

    public Company Company { get; set; }


    public Mimbox()
    {
        Id = Guid.NewGuid();
        MimboxLog log = new("Mimbox created");
        MimboxLogList.Add(log);
    }

    public static void Configure(ModelBuilder modelBuilder)
    {
        //var mimbox = modelBuilder.Entity<Mimbox>();

        //mimbox.HasOne(x => x.Model).WithMany(c => c.Mimboxes);
        //mimbox.HasOne(x => x.Status).WithMany(c => c.Mimboxes);
        //mimbox.HasOne(x => x.Location).WithMany(c => c.Mimboxes);
        //mimbox.HasOne(x => x.Company).WithMany(c => c.MimboxList);
        //mimbox.HasMany(x => x.MimboxLogList).WithOne(c => c.Mimbox);
        //mimbox.HasF(x => x.)
        //.OnDelete(DeleteBehavior.SetNull);
    }
}