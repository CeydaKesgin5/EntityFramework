// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Xml;
ETicaretContext context = new();

#region Change Tracking Nedir?
/*Context nesnesi üzerinden gelen tüm nesneler/veriler otomatik olarak bir takip mekanizması tarafından izlenirler.
  İşte bu takip mekanizmasına Change Tracker denir. Change Tracker ile nesneler üzerindeki değişiklikler/işlemler  
  takip edilecek netice itibariyle bu işlemlerin fıtratına uygun sql sorgucukları generate edilir.
  İşte bu işleme de Change Tracking denir.*/
#endregion

#region ChangeTracker Nedir?
//Takip edilen nesnelere erişebilmemizi sağlayan ve gerektiği taktirde işlemler gerçekleştirmemizi sağlayan  bir propertydir.
//Context sınıfının base class'ı olan DbContext sınıfının bir member'ıdır.

//var urunler=await context.Urunler.ToListAsync(); //tüm verileri çektik.
//urunler[6].Fiyat = 123;
//context.Urunler.Remove(urunler[7]);
//urunler[8].UrunAdi = "asdasd";

//var datas = context.ChangeTracker.Entries();

#endregion

#region DetectChanges
/*EF Core, comtext nesnesi tarafından izlenen tüm nesnelerdei değişiklikleri Change Tracker sayesinde takip
  edebilmekte ve nesnelerde olan verilsel değişiklikler yakalanarak bunların anlık görüntüleri(snapshor)'ini
  oluşturabilir.*/

/*Yapılan değişikliklerin veritabanına gönderilmeden önce algılandığından emin olmak gerekir. SaveChanges 
  fonksiyonu çağrıldığı anda nesneler EF Core tarafından otomatik kontrol edilebilirler.*/

/*Ancak yapılan operasyonlarda güncel tracking verilerinden emin olabilmek için değişikliklerin algılanmasını
  opsiyonel olarak gerçekleştirmek isteyebiliriz. İşte bunun için DetectChanges fonksiyonu kullanılabilir ve her ne kadar 
  EF Core değişiklikleri otomatik algılıyor olsa da siz yine de iradenizle kontrole zorlayabilirsiniz.*/

//SaveChanges ve Entries fonksiyonu içerisinde DetectChanges otomatik olarak tetiklenir.

//var urun = await context.Urunler.FirstOrDefaultAsync(u=>u.Id==3);
//urun.Fiyat = 123;

//context.ChangeTracker.DetectChanges();//zaten SaveChanges tarafından çağrırılıyor. Gereksiz bir maliyet
//await context.SaveChangesAsync();   
#endregion

#region AutoDetectChangeEnabled Property'si
/*İlgili metodlar(SaveChanges, Entries) tarafından DetectChanges metodunun otomatik olarak tetiklenmesinin konfigürasyonunu yapmamızı sağlayan propertydir.
  SaveChanges fonksiyonu tetiklendiğinde DetectChanges metodunu içerisinde default olarak çağırmaktadır. Bu durumda
  DetectChanges fonksioynunun kullanımı irademizle yönetmek ve maliyet/performans optimizasyonu yapmak
  istediğimiz durumlarda AutoDetectChangeEnabled özelliğini kapatabiliriz.*/
#endregion

#region Entries Metodu
//Context'teki Entry metodunun koleksiyonel versiyonudur.
/*Change Tracker mekanimzası tarafından izlenen her entity nesnensinin bilgisinin EntityEntry türünden elde etmemizi
  sağlar ve belirli işlemler yapabilmemize olanak sağlar.*/

/*Entries metodu, DetectChanges metodunu tetikler.Bu durumda tıpki SaveChanges'da olduğu gibi bir maliyettir.
  Buradaki maliyetten kaçınmak için AutoDetectChangeEnabled özelliğine false değeri verilebilir.*/

//var urunler=await context.Urunler.ToListAsync();
//urunler.FirstOrDefault(u=>u.Id==7).Fiyat = 123;//update
//context.Urunler.Remove(urunler.FirstOrDefault(u => u.Id == 8));//delete
//urunler.FirstOrDefault(u => u.Id == 9).UrunAdi = "asdsa";//update

//context.ChangeTracker.Entries().ToList().ForEach(e =>
//{
//    if (e.State == EntityState.Unchanged)
//    {
//        //...
//    }
//    else if (e.State == EntityState.Deleted)
//    {
//        //...
//    }
//});


#endregion

#region AcceptAllChanges Metodu

/*SaveChangesAsync() veya SaveChangesAsync(true) tetiklendiğinde EF Core herşeyin yolunda olduğunu varsayarak track ettiği verilerin takibini keser 
 * yeni değişikliklerin takip edilmesini bekler.Böyle bir durumda bekelnemyen bir durum
 * olası bir hata söz konusu olursa eğer EF Core takip ettiği nesneleri bırakaağı için bir düzeltme mevzu bahis
 * olamayacktır.
 */
/* Haliyle bu durumda devreye SaveChanges(false) ve AcceptAllChanges metotları girecektir.*/

/*SaveChanges(false), EF Core'a gerekli veritabanı komutlarını yürütmesini söyler ancak gerektiğinde yeniden
 * oynatılabilmesi için değişiklikleri beklemeye/nesneleri takip etmeye devam eder. Taa ki AcceptAllChanges
 * metodunu irademizle çağırana kadar!
 */

//SaveChanges(false) ile işlemin başarılı olduğundan emin olursanız AcceptAllChanges metodu ile nesnelerden takibi kesebilirsiniz.

//var urunler = await context.Urunler.ToListAsync();
//urunler.FirstOrDefault(u => u.Id == 7).Fiyat = 123;//update
//context.Urunler.Remove(urunler.FirstOrDefault(u => u.Id == 8));//delete
//urunler.FirstOrDefault(u => u.Id == 9).UrunAdi = "asdsa";//update

//await context.SaveChangesAsync();
//await context.SaveChangesAsync(true);//tracking mekanizmasında takip edilen nesneleri siler.

//await context.SaveChangesAsync(false);
//context.ChangeTracker.AcceptAllChanges();


#endregion

#region HasChanges

//Takip edilen nesneler arasından değişiklik yapılanların olup olmadığını bilgisini verir.
//Arkaplanda DetectChanges metodunu tetikler.

//var reult = context.ChangeTracker.HasChanges(); //redult boolen türünde
#endregion

#region Entity States
//Entity nesnelerinin durumlarını ifade eder.
#region Detached
//Nesnenin Change Tracker tarafından takip edilmediğini ifade eder.

//Urun urun = new();
//Console.WriteLine(context.Entry(urun).State);//ekrana detached yazılır.

#endregion

#region Added
//Veritabanına eklenecek nesneyi ifade eder.Added henüz veritabanına işlenmeyen veriyi ifa eder.
//SaveChanges fonksiyonu çağrıldığında insert sorgusu oluşturulacağı anlamı taşır.

//Urun urun = new() { Fiyat=123,UrunAdi="Urun 1001"};
//Console.WriteLine(context.Entry(urun).State);//detached
//await context.Urunler.AddAsync(urun);
//Console.WriteLine(context.Entry(urun).State);//added
//await context.SaveChangesAsync();
//urun.Fiyat = 300;
//Console.WriteLine(context.Entry(urun).State);//modified

#endregion

#region Unchanged
/*Veritabanından sorgulandığından beri nesne üzerinde herhangi bir değişiklik yapılmadığını ifade eder. Sorgu
neticesinde elde edilen tüm nesneler başlangıçta bu state değerindedir.*/

//var urun=await context.Urunler.ToListAsync();
//var data = context.ChangeTracker.Entries();
//Console.WriteLine(context.Entry(urun).State);//unchanged

#endregion

#region Modified
//Nesne üzerinde değişiklik yani güncelleme yapıldığını ifade eder.
//SaveChanges fonksiyonu çağrıldığında update sorgusu oluşturulacağı anlamına gelir.

//var urun = await context.Urunler.FirstOrDefaultAsync(u=>u.Id==3);
//Console.WriteLine(context.Entry(urun).State);//unchanged
//urun.Fiyat = 123;
//Console.WriteLine(context.Entry(urun).State);//modified
//await context.SaveChangesAsync(false);
//Console.WriteLine(context.Entry(urun).State);//modified

//yukarıda AcceptAllChanges tetiklenmeyi beklediği için takibi bırakmamış, o yüzden hala modified

//await context.SaveChangesAsync();
//Console.WriteLine(context.Entry(urun).State);//unchanged


#endregion

#region Deleted
//Nesnenin silidniğini ifade eder.
//SaveChanges fonksiyonu çağrıldığında delete sorgusu oluşturulacağı anlamına gelir.

//var urun = await context.Urunler.FirstOrDefaultAsync(u=>u.Id==3);
//context.Urunler.Remove(urun);
//Console.WriteLine(context.Entry(urun).State);//deleted
//context.SaveChanges();
//Console.WriteLine(context.Entry(urun).State);//unchanged



#endregion

#endregion

#region Context Nesnesi Üzerinden Change Tracker
//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 55);
//urun.Fiyat = 123;
//urun.UrunAdi = "SİLGİ";//Modified | Update

#region Entry Metodu
#region OriginValues Property'si
//yapılan değişiklikler veritabanına yansıtılmada önce veritabanındaki en güncel verileri elde etmek için kullanılır.

//var fiyat = context.Entry(urun).OriginalValues.GetValue<float>(nameof(Urun.Fiyat));
//var urunAdi=context.Entry(urun).OriginalValues.GetValue<string>(nameof(Urun.UrunAdi));
#endregion
#region CurrentValues Property'si
//İlgili nesnenin mevcut değerini getirdi.

//context.Entry(urun).CurrentValues.GetValue<string>(nameof(Urun.UrunAdi));

#endregion
#region GetDatabaseValues Metodu
//var _urun=await context.Entry(urun).GetDatabaseValuesAsync();
#endregion
#endregion


#endregion



public class ETicaretContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }
    public DbSet<Parca> Parcalar { get; set; }
    public DbSet<UrunParca> UrunParca { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Provider
        //Hnagi sunucuda olacak ConnectonString
        //LazyLoading
        //tool'a dair bir davranış modellemesi çizmemizi sağlayan yapılandırmaları bu fonksiyon üzerinden gerçekleştiriyor olacağız.
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ETicaretDB;User ID=sa;Password=123456");
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