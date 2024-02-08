// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

#region Default Convention
/*Default Convention yöntemnde 1'e çok ilişkiyi kurarken foreign key kolonuna karşılık gelen
 * bir property tanımlamak mecburiyetinde değildir. Eğer tanılamazsak EF Core bunu kendisi
 * tamamlayacak yok eğer tanımlarsak, tanımladığımızı baz alacaktır.
 */
//class Calisan//dependent entity
//{
//    public int Id { get; set; }
//    public int DepartmanId {  get; set; }//foreign key olduğunu algılar 
//    public string Adi { get; set; }

//    public Departman Departman { get; set; }//Navigation Property
//}

//class Departman
//{
//    public int Id { get; set; }
//    public Calisan DepatmanAdi { get; set; }

//    public ICollection<Calisan>Calisanlar {  get; set; }


//}
#endregion

#region Data Annotations
/*Default convention yönteminde foreign kolonuna karşılık gelen property'i tanımladığımızda
 * bu property ismi temel geleneksel entity tanımalama kurallarına uymuyorsa eğer Dta 
 * Annotations'lar ile müdahalede bulunabiliriz.
 */
//class Calisan//dependent entity
//{
//    public int Id { get; set; }
//    [ForeignKey(nameof(Departman))]
//    public int DId {  get; set; }//foreign key olduğunu algılayamaz
//    public string Adi { get; set; }

//    public Departman Departman { get; set; }//Navigation Property
//}

//class Departman
//{
//    public int Id { get; set; }
//    public Calisan DepatmanAdi { get; set; }

//    public ICollection<Calisan> Calisanlar { get; set; }


//}
#endregion
#region Fluent Apı
class Calisan//dependent entity
{
    public int Id { get; set; }
    public int DId {  get; set; }
    public string Adi { get; set; }

    public Departman Departman { get; set; }//Navigation Property
}

class Departman
{
    public int Id { get; set; }
    public Calisan DepatmanAdi { get; set; }

    public ICollection<Calisan> Calisanlar { get; set; }


}
#endregion
class ESirketDbContext : DbContext
{
    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<Departman> Departman { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ETicaretDB;User ID=sa;Password=123456");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calisan>()
            .HasOne(c=>c.Departman)
            .WithMany(d=>d.Calisanlar)
            .HasForeignKey(c=>c.DId);
    
    
    }
}