// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;



#region Veri Nasıl Eklenir?
/*
//Verisel işlemleri yapabilmek için context nesnesinden bir tane instnce olşturmamız gerekiyor.
ETicaretContext context = new();
//instance
Urun urun = new()//Bu Entity sınıfından (Urun) bir nesne oluşturduk.Yani veritabanından bir veriye karşılık gelecek nesne oluşturuş olduk
{
    UrunAdi = "A Ürünü",
    Fiyat = 1000
};

#region context.DbSet.AddAsync()
//Yapmamız gereken: Context nesnesi üzerinden Entity instance'sinin veritabanına gidip eklenmesi gerektiğinin talimatı verilmeli.
await context.AddAsync(urun);
//await context.Urunler.AddAsync(urun);
#endregion
//EF Core'a yaptığımız bu işlemi veritabanına execute etmesini istememiz gerekiyor.
await context.SaveChangesAsync();//SaveChanges:insert,update ve delete sorgularını oluşturup bir transaction eşliğinde veritabanına gönderip execute eden bir fonksiyondur.
                                 //Eğer ki oluşturulan sorgulardan biri başarısız olursa tüm işlemleri geri alır.(rollback)
*/
#endregion
#region EF Core açısından bir verinin eklenmesi gerektiği nasıl anlaşılıyor?
/*
ETicaretContext context = new();
Urun urun = new()
{
    UrunAdi = "B ürünü",
    Fiyat = 2000
};

Console.WriteLine(context.Entry(urun).State);

await context.AddAsync(urun);

Console.WriteLine(context.Entry(urun).State);

await context.SaveChangesAsync();

Console.WriteLine(context.Entry(urun).State);
*/
#endregion

#region Birden fazla veri eklerken nelere dikkat 
//SaveChanges fonksiyonu her tetiklendiğinde bir transaction oluşturacağından dolaya EF Core ile yapılan
//her bir isleme özel kullanmaktan kaçınmalayaz Çünkü her işleme özel transaction veritabanı açısından
//ekstradan maliyet demektir. O yüzden nümkün mertebe tün işlemlerinizi tek bir transaction eşliğinde veritabanına gönderebilmek
//için savechanges'ı aşağadaki gibi tek seferde kullanmak hem saliyet hem de yönetilebilirlik açısından katkıda bulunmuş olacaktır.

//ETicaretContext context = new();
//Urun urun1 = new()
//{
//    UrunAdi = "C ürünü",
//    Fiyat = 2000
//};
//Urun urun2 = new()
//{
//    UrunAdi = "D ürünü",
//    Fiyat = 2000
//}; Urun urun3 = new()
//{
//    UrunAdi = "E ürünü",
//    Fiyat = 2000
//};

//await context.AddAsync(urun1);

//await context.AddAsync(urun2);

//await context.AddAsync(urun3);
//await context.SaveChangesAsync();
#region SaveChange'ı verimli kullanalım!

#endregion
#region AddRange

//ETicaretContext context = new();
//Urun urun1 = new()
//{
//    UrunAdi = "C ürünü",
//    Fiyat = 2000
//};
//Urun urun2 = new()
//{
//    UrunAdi = "D ürünü",
//    Fiyat = 2000
//}; Urun urun3 = new()
//{
//    UrunAdi = "E ürünü",
//    Fiyat = 2000
//};

//await context.Urunler.AddRangeAsync(urun1, urun2, urun3);
//context.SaveChanges();

#endregion


#endregion

#region Eklenen Verinin Generate edilen Id'sini Elde Etme 
ETicaretContext context = new();
Urun urun = new(){
   UrunAdi = "O ürünü",
    Fiyat = 2000
};

await context.AddAsync(urun);
await context.SaveChangesAsync();
Console.WriteLine(urun.Id);


#endregion
public class ETicaretContext: DbContext//Context nesnesi veritabanına karşılık geliyor.
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

#region OnConfiguring ile konfigürasyon ayarlarını gerçekleştirmek
//EF Core tool'unu yapılandırmak için kullandığımız bir metottur.
//Context nesnesinde override edilerek kullanılmaktadır.
//EF Core,her tablonun default olarak bir primary key kolunu olması gerektiğini kabul eder.
//Haliyle, bu kolunu temsil eden bir property tanımlamadığınız taktirde hata verecektir.

#endregion
