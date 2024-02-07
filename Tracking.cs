// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;

ETicaretContext context = new();

//ChangeTracker mekanizmasının davranışlarını yönetmemizi sağlayan fonksiyonlar.

#region AsNoTracking Metodu
//Context üzerinden gelen tüm datalar Change Tracker mekanizması tarafından kontrol edilmektedir.
/*Change Tracker takip ettiği nesnelerin sayısıyla doğru orantılı olacak şekilde maliyete sahiptir.
  Bu yüzden üzerinde işlem yapılmayacak verilerin takip edilmesi bizlere gereksiz yere bir maliyet çıkaracaktır.
 */

/*AsNoTracking metodu, context üzerinden sorgu neticesinde gelecek olan verileri Change Tracker
  tarafından takip edilmesini engeller.
 */

/*AsNoTracking metodu ile Change Tracker'ın ihtiyaç olmayan verilerdeki maliyeti törpülemiş 
  oluruz.
 */

/*AsNoTracking fonksiyonu ile yapılan sorgulamalarda; verleri elde edebilir, bu verileri
  istenilen noktalarda kullanabilir lakin veriler üzerinde herhangi bir değişiklik/update 
  işlemi yapamayız. 
 */

//var kullanicilar=await context.Kullanicilar.AsNoTracking().ToListAsync();//kullanici verilerini listele ama takip etme, neticesinde yapılan herhangi bir değişiklik veritabanına yansımaz


//foreach (var kullanici in kullanicilar)
//{
//    Console.WriteLine(kullanici.Adi);
//    kullanici.Adi = $"yeni-{kullanici.Adi}";//değişiklik olmaz
//    context.Kullanicilar.Update(kullanici);//değişir.
//}
//await context.SaveChangesAsync();
#endregion

#region AsNoTrackingWithIdentityResolution
/* CT(Change Tracker) mekanizması yinelenen verileri tekil instance olarak getirir.
 * Buradan ekstradan bir performans kazancı söz konusudur.
 */

/*Bizler yaptığımız sorgularda takip mekanizmasının AsNoTracking metodu ile maliyetini kırmak
 * isterken bazen maliyete sebebiyet verebiliriz.(Özellikle ilişkisel tabloları sorgularken 
 * bu duruma dikkat etmemiz gerekiyor.)
 */
/*AsNoTracking ile elde edilen veriler takip edilmeyeceğinden dolayı yinelenen verilerin 
 * ayrı instancelerde olmasına sebebiyet veriyoruz. Çünkü CT mekanizması takip ettiği nesneden 
 * bellekte varsa eğer aynı nesneden bir daha oluşturma gereği duymaksızın o nesneyi ayrı noktalardaki 
 * ihtiyacı aynı instance üzerinden gidermektedir.
 */

/*Böyle bir durumda hem takip mekanizmasının maliyetini ortadan kaldırmak hem de yinelenen 
 * dataları tek bir instance üzerinde karşılamak için AsNoTrackingWithIdentityResolution
 * fonksiyonunu kullanabiliriz.
 */

var kitaplar = await context.Kitaplar.Include(k => k.Yazarlar).AsNoTrackingWithIdentityResolution().ToListAsync();
/*output
Kitap nesnesi oluşturuldu
Yazar nesnesi oluşturuldu
Kitap nesnesi oluşturuldu
Yazar nesnesi oluşturuldu
Kitap nesnesi oluşturuldu
Kitap nesnesi oluşturuldu
*/

var kitaplar2 = await context.Kitaplar.Include(k => k.Yazarlar).AsNoTracking().ToListAsync();
/*output
Kitap nesnesi oluşturuldu
Yazar nesnesi oluşturuldu
Kitap nesnesi oluşturuldu
Yazar nesnesi oluşturuldu
Kitap nesnesi oluşturuldu
Yazar nesnesi oluşturuldu
Kitap nesnesi oluşturuldu
Yazar nesnesi oluşturuldu
*/

/*AsNoTrackingWithIdentityResolution fonksiyonu AsNoTracking fonksiyonuna göre daha yavaştır/maliyetlidir.
 * fakat CT'a nazaran daha performanslı ve az maliyetlidir.
 */
#endregion

#region AsTracking
/*Context üzerinden gelen dataların CT tarafından takip edilmesini iradeli bir şekilde ifade
etmenizi sağlayan fonksiyondur.*/
/*Niye kullanalım?
 * Bir sonraki inceleyeceğimiz UseQueryTrackingBehavior metodunun davranışı gereği uygulama s
 * seviyesinde CT'ın default olarak devrede olup olmamasını ayarlıyor olacağız. Eğer ki
 * default olarak pasif hale getirilirse böyle durumda takip mekanizmasının ihtiyaç olduğu
 * sorgularda AsTracking fonksiyonunu kullanabilir ve böylece takip mekanizmasını iradeli bir
 * şekilde devreye sokmuş oluruz.
 */
var kitaplar3=await context.Kitaplar.AsTracking().ToListAsync();
#endregion

#region UseQueryTrackingBehavior
/*EF Core uygulama seviyesinde ilgili contexten gelen verilerin üzerinde CT mekanizmasının davranışı temel seviyede
 * belilememizi sağlyan fonksiyondur. Ynai konfigürasyon fonksiyonudur.
 */
#endregion

public class ETicaretContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }
    public DbSet<Parca> Parcalar { get; set; }
    public DbSet<UrunParca> UrunParca { get; set; }
    public DbSet<Kullanici> Kullanicilar { get; set; }

    public DbSet<Rol> Roller { get; set; }

    public DbSet<Yazar> Yazarlar { get; set; }

    public DbSet<Kitap> Kitaplar { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ETicaretDB;User ID=sa;Password=123456");

        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UrunParca>().HasKey(up => new { up.UrunId, up.ParcaId });
    }
}
public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public float Fiyat { get; set; }
    public ICollection<Parca> Parcalar { get; set; }
}
public class Parca
{
    public int Id { get; set; }
    public string ParcaAdi { get; set; }


}
public class UrunParca
{
    public string UrunId { get; set; }
    public float ParcaId { get; set; }
    public Urun Urun { get; set; }
    public Parca Parca { get; set; }


}

public class UrunDetay
{
    public int Id { get; set; }
    public float Fiyat { get; set; }
}

public class Kullanici
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public ICollection<Rol> Roller { get; set; }
}

public class Rol
{
    public int Id { get; set; }
    public string RolAdi { get; set; }
    public ICollection<Kullanici> Kullanicilar { get; set; }

}
public class Kitap
{
    public Kitap() => Console.WriteLine("Kitap nesnesi oluşturuldu");
    public int Id { get; set; }
    public string KitapAdi { get; set; }
    public int SayfaSayisi { get; set; }

    public ICollection<Yazar> Yazarlar { get; set; }
}
public class Yazar
{
    public Yazar() => Console.WriteLine("Yazar nesnesi oluşturuldu");
    public int Id { get; set; }
    public string YazarAdi { get; set; }
  

    public ICollection<Kitap> Kitaplar { get; set; }

}