// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;

#region Veri Nasıl Güncellenir?
//ETicaretContext context= new();
////hedef veriyi context üzerinde çekme
//Urun urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 3);//Vermiş olduğumuz şarta uyan ilk veriyi getiren fonksiyon
//                                                                      //Kod çalıştırıldığında yazmış olduğumuz kod veritabanında bir select sorgusu olarak çalıştırılacak ve şarta uygun ilk veriyi getirecek 
//                                                                      //EF core yapılanması bu yapıyı Urun entitysinde bir nesneye dönüştürecek.->urun
//urun.UrunAdi = "H ürünü";
//urun.Fiyat = 999;

//await context.SaveChangesAsync();//güncellendi
#endregion

#region ChangeTracker Nedir?
//Context üzerinden gelen verilerin, nesnelerin takibinden sorumlu olan bir mekanizmadır.Bu takip mekanizması sayesinde context üzerinden gelen verilerle ilgili işlemler neticesinde
//update yahut delete sorgularının oluşturulacağı anlaşılır.

//EF Core'un contexte'ten gelen nesneler üzerinde nasıl bir davranış sergileyeceğini
//yani SaceChanges'i çağırdığımızda hangi sorguyu oluşturacağını anlayabiilmesi için ChangeTracker mekanzaması üzeriden bir çalışma gerçekleştirilir.
#endregion

#region Takip Edilmeyen Nesneler Nasıl Güncellenir?

//ETicaretContext context = new();

////context üzerinden elde edilmemiş nesne
//Urun urun = new()
//{
//    Id = 3,
//    UrunAdi = "Yeni Ürün",
//    Fiyat = 123
//};

//context.Urunler.Update(urun);//ChangeTracker mekanizmasıyla takip edilmeyen bu nesneyi ben güncellemek istiyorum. Takibi olmadığı için direkt SaveChangesi çağıramam.
//                             //Bu yüzden update fonksiyonu ile EF Core u bildiriyorum. Daha sonra SC çağırıyorum.
//                             //Update fonksiyonunu kullanabilmek için kesinlikle ilgili nesnenein ID değeri verilmelidir.
//await context.SaveChangesAsync();

#endregion
#region EntityState Nedir?
//Bir entity instance'ının durumunu ifade eden bir referanstır. 
//ETicaretContext context = new();
//Urun u = new();
//Console.WriteLine(context.Entry(u).State);//durumu verir

#endregion
#region EF Core açısından bir verinin güncellenmesi gerektiği nasıl anlaşılır?

//ETicaretContext context = new();
//Urun urun = await context.Urunler.FirstOrDefaultAsync(u=>u.Id==3);
//Console.WriteLine(context.Entry(urun).State);//Unchanged

//urun.UrunAdi = "Hilmi";
//Console.WriteLine(context.Entry(urun).State);//Modified

//await context.SaveChangesAsync();
//Console.WriteLine(context.Entry(urun).State);//Unchanged


#endregion
#region Birden Fazla Veri Güncellenirken Nelere Dikkat Edilmelidir?
ETicaretContext context = new();

var urunler=await context.Urunler.ToListAsync();//ürünler, instance olarak elde ettik
foreach (var urun in urunler)
{
    urun.UrunAdi += "*";

}
await context.SaveChangesAsync();
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