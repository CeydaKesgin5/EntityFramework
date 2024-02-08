// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

ESirketDbContext context = new();
#region Default Convention
//Her 2 entity'de Navigation Property ile birbirlerini tekil olarak referans ederek fiziksel bir ilişkinin olacağı ifade edilir.
/*1 to 1 ilişki türünde Dependent entity'nin hangisi olduğunu default olarak
 * belirleyebilmek pek kolay değildir. Bu durumda fiziksel olarak bir foreign key'e karşılık
 * property/kolon tanımlayarak çözüm getirebiliyoruz.
 * Bçylece foreign key'e karşılık property tanımlayarak lüzumsuz bir kolon oluşturuyoruz.
 * 
 */
//class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }

//    public CalisanAdresi CalisanAdresi { get; set; }//Navigation Property
//}

//class CalisanAdresi
//{
//    public int Id { get; set; }
//    public int CalisanId { get; set; }
//    public string Adres { get; set; }
//    public Calisan Calisan { get; set; }//Navigation Property
//}
//}

#endregion

#region Data Annotations
//Navigation propertyler tanımlanmalıdır.
/* Foreignkey kolonunn ismi default convention'un dışında bir kolon olacaksa eğer
 * Foreignkey attribute ile bunu bildirebiliriz.
 */

// Foreign key kolonu oluşturulmak zorunda değil
/*1'e 1 ilişkide ekstradan foreign key kolonuna ihtiyaç olmayacğından dolayı depent entitydeki 
 * id kolonunun hem foreign key hem de primary key olarak kullanmayı tercih ediyoruz.
 */
//class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }

//    public CalisanAdresi CalisanAdresi { get; set; }//Navigation Property
//}

//class CalisanAdresi
//{
//    [Key, ForeignKey(nameof(Calisan))]//Id hem foreign key hem primary key 
//    public int Id { get; set; }
//    //[ForeignKey(nameof(Calisan))]//CalisanId foreignkey
//    // public int CalisanId {  get; set; }
//    public string Adres { get; set; }
//    public Calisan Calisan { get; set; }//Navigation Property
//}


#endregion
#region Fluent API
//Navigation propertyler tanımlanmalıdır.
/* Fluent API yönteminde entity'ler arasındaki ilişki context sınıfı içerisinde OnModelCreating
 * fonksiyonu override edilerek metotlar aracılığyla tasarlanması gerekmektedir. Yani tüm sorumluluk 
 * bu fonksiyon içerisiddeki çalışmalardadır.
 */
class Calisan
{
    public int Id { get; set; }
    public string Adi { get; set; }

    public CalisanAdresi CalisanAdresi {  get; set; }//Navigation Property
}

class CalisanAdresi
{
    public int Id { get; set; }
    public string Adres { get; set; }
    public Calisan Calisan { get; set; }//Navigation Property
}
#endregion
class ESirketDbContext : DbContext
{
    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<CalisanAdresi> CalisanAdresleri { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ETicaretDB;User ID=sa;Password=123456");
    }
    //Model'lern(entity) veritabanında generate edilecek yapıları bu fonksiyonda içerisinde konfigüre edilir.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalisanAdresi>()
            .HasKey(c=>c.Id);

        modelBuilder.Entity<Calisan>()
            .HasOne(c=>c.CalisanAdresi)//Calisan ile CalisanAdres arasında 1'e 1 ilişki var
            .WithOne(c=>c.Calisan)
            .HasForeignKey<CalisanAdresi>(c=>c.Id);
    }
}