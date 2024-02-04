// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;

ETicaretContext context = new();
#region En Temel Basit Bir Sorgulama Nasıl Yapılır?
#region Method Syntax
//var urunler = await context.Urunler.ToListAsync();
#endregion
#region Query Syntax
//var urunler2 = await (from urun in context.Urunler
//                      select urun).ToListAsync();
#endregion
#endregion

#region Sorguyu Execute Etmek İçin Ne Yapmamız Gerekiyor?
#region ToListAsync
//Sorguyu veritabanı sunucusuna gönderir.
//2ye ayrılır  Method Syntax ve Query Syntax -> yukarıdaki kodlar

#endregion
//int urunId = 5;
//string urunAdi = "2";
//var urunler = from urun in context.Urunler
//              where urun.Id > urunId &&urun.UrunAdi.Contains(urunAdi)//like
//              select urun;
//foreach (var urun in urunler)
//{
//    Console.WriteLine(urun.UrunAdi);
//}
#region Foreach
//IQuerable türünden yani execute edilmemiş sorgu yapılanmasını foreach'le çağırınca bunun execute edilmesi gerektiğini anlıyor 
//bu noktada execute edilip Enumerable oluyor.
//foreach (var urun in urunler)
//{
//    Console.WriteLine(urun.UrunAdi);
//}

//diğer yöntem
//await urunler.ToListAsync();

#endregion
#endregion

#region IQueryable ve IEnumerable Nedir?

//var urunler = from urun in context.Urunler
//             select urun;//sorgulama aşaması->IQuerable

//var urunler2 = await (from urun in context.Urunler
//                      select urun).ToListAsync();//memoryde->Enumerable

//IQueryable: Sorguya karşılık gelir. EF Core üzerinden yapılmış olan sorgunun execute edilmemiş halini ifade eder.
//Enumerable: Sorgunun çalıştırılıp/execute edlip verilerin in memory'ye yüklenmiş halini ifade eder.
#endregion

#region Deferred Execution(Ertelenmiş Çalışma)
//IQuerable çalışmalarında ilgili kod yazıldığı noktada tetiklenmez.çalıştırılmaz
//yani ilgili kod yazıldığı noktada generate etmez! Nerede eder? Çalıştırıldığı yani execute edildiği noktada teteklenir!
//İşte bu duruma da ertelenmiş çalışma adı verilir. 
#endregion

#region Çoğul Veri Getiren Veri Sorgulama Fonksiyonları
#region ToListAsync
//Üretilen sorguyu execute ettirmenizi sağlayan fonksiyondur.
//yani IQuerable'dan Enumerableye geçiş
//2 syntaxa sahiptir.

#region Method Syntax
//var urunler = context.Urunler.ToListAsync();
#endregion

#region Query Syntax
//var urunler2=(from urun in context.Urunler
//              select urun).ToListAsync();
#endregion

#endregion
#region Where
//Oluşturulan sorguya where şartı eklememizi sağlayan bir fonksiyondur.
#region Method Syntax
//var urunler = await context.Urunler.Where(u => u.Id > 500).ToListAsync();//başında bir şey arıyorsa startsWith,sonunda ise EndWith,ortasında ise Contains ->like için 
//var urunler = await context.Urunler.Where(u => u.UrunAdi.StartsWith("a")).ToListAsync();
#endregion
#region Query Syntax
//var urunler = from urun in context.Urunler
//              where urun.Id>500 || urun.UrunAdi.EndsWith("7")
//              select urun;

//var data=await urunler.ToListAsync();
#endregion
#endregion

#region Order By
//Sorgu üzerinde sıralama yapmamızı sağlayan bir fonksiyondur.(Ascending)
#region Method Syntax
//var urunler = context.Urunler.Where(u => u.Id > 500 || u.UrunAdi.EndsWith("2")).OrderBy(u => u.UrunAdi);
//await urunler.ToListAsync();
#endregion

#region Query Syntax
//var urunler2 = from urun in context.Urunler
//               where urun.Id > 500 || urun.UrunAdi.StartsWith("2")
//             orderby urun.UrunAdi
//             select urun;
//await urunler2.ToListAsync();

#endregion

#endregion

#region ThenBy
//Order By üzerinde yapılan sıralama işlemini farklı kolonlarda uygulamamızı sağlayan bir fonksiyondur.
//(Ascending)

//var urunler = context.Urunler.Where(u => u.Id > 500 || u.UrunAdi.EndsWith("2")).OrderBy(u => u.UrunAdi)
//    .ThenBy(u=>u.Fiyat).ThenBy(u=>u.Id);

//await urunler.ToListAsync();
#endregion

#region OrderByDescending
//Descending larak sıralama yapmamızı sağlayan bir fonksiyondur.

#region Method Syntax
//var urunler = context.Urunler.OrderByDescending(u => u.Fiyat);
//await urunler.ToListAsync();
#endregion

#region Query Syntax
//var urunler2 = from urun in context.Urunler
//               where urun.Id > 500 || urun.UrunAdi.StartsWith("2")
//               orderby urun.UrunAdi descending
//               select urun;
//await urunler2.ToListAsync();

#endregion

#endregion

#region ThenByDescending
//OrderByDescending By üzerinde yapılan sıralama işlemini farklı kolonlarda uygulamamızı sağlayan bir fonksiyondur.
//(Ascending)

//var urunler=context.Urunler.OrderByDescending(u => u.Id).ThenByDescending(u=>u.Fiyat)
//    .ThenBy(u=> u.UrunAdi).ToListAsync();


#endregion

#endregion

#region Tekil Veri Getiren Sorgulama Fonksiyonları
//Yapılan sorguda sade ve sadece tek bir verinin gelmesi amaçlanıyorsa Single ya da SingleOrDefault fonksiyonları kullnılabilir.
#region SingleAsync
//Eğer ki, sorgu neticesinde birden fazla veri geliyorsa ya da hiç gelmiyorsa her iki durumda da exception fırlatır.
#region Tek Kayıt Geldiğinde
//var urun=context.Urunler.SingleAsync(u=>u.Id==55);

#endregion
#region Hiç Kayıt Gelmediğinde
//var urun = context.Urunler.SingleAsync(u => u.Id == 5555);
//Idye sahip veriyi bulamadığında hata fırlatır
#endregion
#region Birden Fazla Kayıt Geldiğinde
//var urun = context.Urunler.SingleAsync(u => u.Id == 55);
//Birden fazla kayıt var ise de patlama olur
#endregion
#endregion

#region SingleOrDefaultAsync
//Eğer ki,sorgu neticesinde birden fazla veri geliyorsa exception fırlatır,hiç veri gelmiyorsa null döner. 
#region Tek Kayıt Geldiğinde
//var urun=context.Urunler.SingleOrDefaultAsync(u=>u.Id==55);

#endregion
#region Hiç Kayıt Gelmediğinde
//var urun = context.Urunler.SingleOrDefaultAsync(u => u.Id == 5555);
//Idye sahip veriyi bulamadığında hata fırlatır
#endregion
#region Birden Fazla Kayıt Geldiğinde
//var urun = context.Urunler.SingleOrDefaultAsync(u => u.Id == 55);
//Birden fazla kayıt var ise de patlama olur
#endregion
#endregion

//yapılan sorguda tek bir verinin gelmesi amaçlanıyorsa First ya da FirstOrDefault fonksiyonları kullanılabilir.
//İlgili sorguyu çalıştırıdığında elinde birden fazla veri varsa ve bunlardan birini kullanmak istiyorsak bu fonksiyonu kulanırız.

#region FirstAsync
//Sorgu neticesinde elde edilen verilerden ilkini getirir.Eğer ki hiç veri gelmiyorsa hata fırlatır.

#region Tek Kayıt Geldiğinde
//var urun=context.Urunler.FirstAsync(u=>u.Id==55);

#endregion
#region Hiç Kayıt Gelmediğinde
//var urun = context.Urunler.FirstAsync(u => u.Id == 5555);
//Idye sahip veriyi bulamadığında hata fırlatır
#endregion
#region Birden Fazla Kayıt Geldiğinde
//var urun = context.Urunler.FirstAsync(u => u.Id == 55);
#endregion
#endregion

#region FirstOrDefaultAsync
//Sorgu neticesinde elde edilen verilerden ilkini getirir.Eğer ki hiç veri gelmiyorsa null değerini döndürür.

#region Tek Kayıt Geldiğinde
//var urun=context.Urunler.FirstOrDefaultAsync(u=>u.Id==55);

#endregion
#region Hiç Kayıt Gelmediğinde
//var urun = context.Urunler.FirstOrDefaultAsync(u => u.Id == 5555);
#endregion
#region Birden Fazla Kayıt Geldiğinde
//var urun = context.Urunler.FirstOrDefaultAsync(u => u.Id == 55);
#endregion
#endregion

#region FindAsync
//Find fonksiyonu, primary key kolonuna özel hızlı bir şekilde sorgulama yapmamızı sağlar
//Urun urun = await context.Urunler.FindAsync(55);

#region Composite Primary key Durumu
//UrunParca u= await context.UrunParca.FindAsync(55);
#endregion
#endregion

#region LastAsync
//Sorgu neticesinde elde edilen verilerden en sonuncusunu getirir.Eğer ki hiç veri gelmiyorsa hata fırlatır.
//OrderBy kullanılması zorunludur.
//var urun=context.Urunler.OrderBy(u=>u.UrunAdi).LastAsync(u=>u.Id>55);

#endregion

#region LastOrDefaultAsync
//Sorgu neticesinde elde edilen verilerden en sonuncusunu getirir.Eğer ki hiç veri gelmiyorsa null döndürür.
//OrderBy kullanılması zorunludur.
//var urun=context.Urunler.OrderBy(u=>u.UrunAdi).LastOrDefaultAsync(u=>u.Id>55);


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
    public ICollection<Parca> Parcalar {  get; set; }
}
public class Parca
{
    public int Id { get; set; }
    public string ParcaAdi { get; set; }

      
}
public class UrunParca
{
    public string UrunId { get; set; }          
    public float ParcaId { get; set;}
    public Urun Urun { get; set; }
    public Parca Parca { get; set; }


}