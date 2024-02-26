// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");
ApplicationDbContext context = new();
#region Shadow Properties-Gölge özellikler
/*
 Entity sınıfarında fiziksel olarak tanımlanmayan/ modellenmeyen ancak EF Core tarafından ilgili entity için var olan/ var olduğu kabul edilen property’lerdir.
Genellikle tabloda gösterilmesini istemedğimiz  entity instance’i üzerinde işlem yapmayacağımızkolonlar için shadow propertyler kullanılabilir.
Shadow properyler değerleri ve stateleri change tracker tarafından kontrol edilir. 

 */

#endregion

#region Foreign Key -Shadow Properties
/*İlişkisel senaryolarda foreign key property'sini tanımladığımız halde
 * EF Core tarafından dependent entity'e eklenmektedir. İşte bu shadow property'dr.
 
 */

//var blogs = await context.Blogs.Include(b => b.Posts)
//    .ToListAsync();
//Console.WriteLine();
#endregion
#region Shadow property oluşturma
//Bir entity üzerinde shadow property oluşturmak istiyorsanız eğer Fluent API'ı kullanmamız gerekmektedir.

//shadow property
//modelBuilder.Entity<Blog>()
//    .Property<DateTime>("CreatedDate");
//Bana DateTime türünde CreatedDate adında bir property tanımla.
#endregion
#region Shadow property' erişim sağlama
#region Change Tracker ile erişim sağlama
//Shadow Property2 erişim sağlayabilmk için Chane Tracker'dan istifade edilebilir.

//var blog = await context.Blogs.FindAsync();
//var createdData = context.Entry(blog).Property("CreatedData");

//Console.WriteLine(createdData.CurrentValue);
//Console.WriteLine(createdData.OriginalValue);

//createdData.CurrentValue = DateTime.Now;
//await context.SaveChangesAsync();
#endregion
#region EF Property ile erişim sağlama
/* Özellikle linq sorgulamalarında Shadow property'lerine erişim için EF Property static yapılanmasını
 * kullanabiliriz.
 */

var blogs = context.Blogs.OrderBy(b=>EF.Property<DateTime>(b,"CreatedData")).ToListAsync();
var blogs2 = context.Blogs.Where(b => EF.Property<DateTime>(b, "CreatedData").Year > 2020).ToListAsync();
#endregion
#endregion
class Blog
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Post> Posts { get; set; }
}
class Post
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Blog Blog;

}

class ApplicationDbContext: DbContext
{
    public DbSet<Blog> Blogs { get; set; }  
    public DbSet<Post> Posts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //shadow property
        modelBuilder.Entity<Blog>()
            .Property<DateTime>("CreatedDate");
        //Bana DateTime türünde CreatedDate adında bir property tanımla.


    }
}