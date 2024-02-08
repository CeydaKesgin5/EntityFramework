// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

#region Default Convention
/* 2 entity arasındaki ilişkiyi navigation propertyler üzerinden çoğul olarak kurmalıyız.
 * (ICollection, List)
 * 
 */
/* Default Convention'da cross table'ı manuel oluşturmak zorunda değiliz. EF Core tasarıma uygun bir şekilde
 * cross table'ı kendisi otomatik basacak ve generate edecektir.
 * 
 */
/* Ve oluşturulan cross tabl^2ın içerisinde composite primary key'i de otomatik oluşturmuş olacaktır.
 */
//class Kitap
//{
//    public int Id { get; set; }
//    public string KitapAdi { get; set; }  
//    public ICollection<Yazar>Yazarlar { get; set; }
//}

//class Yazar
//{
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }

//    public ICollection<Kitap> Kitaplar { get; set; }
//}
//class EKitapDbContext : DbContext
//{
//    public DbSet<Kitap> Kitaplar { get; set; }
//    public DbSet<Yazar> Yazarlar { get; set; }
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ETicaretDB;User ID=sa;Password=123456");
//    }
  
//}
#endregion
#region Data Annotations
//Cross table manuel olarak oluşturulmalıdır.
//Entity'lerde oluşturduğumuz cross table entitysi ile bire çok bir ilişki kurulmalı.
//Cross table'da composite primary key'i data annotations(attribute) lar ile manuel kuramıyoruz. Bunun için de Fluent Apı'de çalışma yapmamız gerekiyor.
/*//Cross table'a karşılık bir entity modeli oluşturuyorsak eğer bunu context sınıfı içerisinde
 * DbSet property'si olarak bildirmek mecburiyetinde değiliz.
 */
//class Kitap
//{
//    public int Id { get; set; }
//    public string KitapAdi { get; set; }
//    public ICollection<KitapYazar> Yazarlar { get; set; }
//}
//class KitapYazar//CROSS TABLE
//{
//    public int KitapId { get; set; }
//    public string YazarId { get; set; }
//    public Kitap Kitap {  get; set; }
//    public Yazar Yazar { get; set; }
//}
//class Yazar
//{
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }

//    public ICollection<KitapYazar> Kitaplar {  get; set; }

//}
//class EKitapDbContext : DbContext
//{
//    public DbSet<Kitap> Kitaplar { get; set; }
//    public DbSet<Yazar> Yazarlar { get; set; }
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ETicaretDB;User ID=sa;Password=123456");
//    }
//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {//composite primary key
//        modelBuilder.Entity<KitapYazar>()
//            .HasKey(ky => new { ky.KitapId, ky.YazarId });
//    }
//}
#endregion

#region Fluent API
//Cross table manuel olarak oluşturulmalıdır.
//DbSet olarak oluşturulmasına gerek yok
//Composite PK HasKey metodu ile kurulmalı
class Kitap
{
    public int Id { get; set; }
    public string KitapAdi { get; set; }
    public ICollection<KitapYazar> Yazarlar { get; set; }
}
class KitapYazar//CROSS TABLE
{
    public int KitapId { get; set; }
    public string YazarId { get; set; }
    public Kitap Kitap { get; set; }
    public Yazar Yazar { get; set; }
}
class Yazar
{
    public int Id { get; set; }
    public string YazarAdi { get; set; }

    public ICollection<KitapYazar> Kitaplar { get; set; }

}
class EKitapDbContext : DbContext
{
    public DbSet<Kitap> Kitaplar { get; set; }
    public DbSet<Yazar> Yazarlar { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ETicaretDB;User ID=sa;Password=123456");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KitapYazar>()
            .HasKey(ky => new {ky.KitapId,ky.YazarId});
       
        modelBuilder.Entity<KitapYazar>().
            HasOne(ky => ky.Kitap)
            .WithMany(k => k.Yazarlar)
            .HasForeignKey(ky=>ky.KitapId);
        
        modelBuilder.Entity<KitapYazar>()
            .HasOne(ky => ky.Yazar).
            WithMany(y => y.Kitaplar)
            .HasForeignKey(ky => ky.YazarId);
        

    }
}
#endregion