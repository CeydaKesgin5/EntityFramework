
// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;

#region Veri Nasıl Silinir?


//ETicaretContext context = new ETicaretContext();
//Urun urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 5);
//context.Urunler.Remove(urun);
//await context.SaveChangesAsync();
#endregion

#region Silme İşleminde ChangeTacker'in Rolü Nedir?

#endregion
public class ETicaretContext : DbContext//Context nesnesi veritabanına karşılık geliyor.
{
    public DbSet<Urun> Urunler { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Provider
        //Hnagi sunucuda olacak ConnectonString
        //LazyLoading
        //tool'a dair bir davranış modellemesi çizmemizi sağlayan yapılandırmaları bu fonksiyon üzerinden gerçekleştiriyor olacağız.
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ETicaretDB;User ID=sa;Password=123456");
    }
}
//Entity
public class Urun//tablo modeli
{
    //sütun
    public int Id { get; set; }//ıd yi gördüğü zaman default olarak pimary key olarak tanımlar.
    public string UrunAdi { get; set; }
    public float Fiyat { get; set; }
}
