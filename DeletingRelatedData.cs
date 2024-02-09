// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
ApplicationDbContext context = new();

#region 1 to 1
//Person? person = await context.Persons
//    .Include(p => p.Address)
//    .FirstOrDefaultAsync(p => p.Id == 1);

//if (person != null)
//    context.Addresses.Remove(person.Address);
//await context.SaveChangesAsync();

#endregion

#region 1 to Many
//Blog? blog=await context.Blogs
//    .Include(b=>b.Posts)
//    .FirstOrDefaultAsync(b=>b.Id==1);

//Post? post= blog.Posts.FirstOrDefault(p => p.Id == 2);
//context.Posts.Remove(post);
//await context.SaveChangesAsync();

#endregion

#region many to many
//Book? book =await context.Books
//    .Include(b=>b.Authors)
//    .FirstOrDefaultAsync(b=>b.Id==1);

//Author? author=book.Authors.FirstOrDefault(p=>p.Id==2);
////context.Authors.Remove(author);//yazarı siler
//book.Authors.Remove(author);//cross table bağını siler
//await context.SaveChangesAsync();

#endregion

#region Cascade Delete
//Bu davranış modelleri Fluent API ile konfigüre edilir.
#region Cascade
//Principal tablodan silinen veriyle karşı/bağımlı tabloda bulunan ilişkili verilerin silinmesini sağlar.
#endregion
#region SetNull
//Principal tablodan silinen veriyle karşı/bağımlı tabloda bulunan ilişkili verilere null değerini atar.
//Foreign key ve primary key aynı ise kullanamayız
#endregion
#region Restrict
//Principal tablodan herhangi bir veri silinmeye çalışıldığında o veriye karşılıkk dependent
//table'da ilişkisel veriler varsa bu sefer bu silme işlmeini engellemesini sağlar.
#endregion
#endregion
class Person
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Address Address { get; set; }
}
class Address
{
    public int Id { get; set; }
    public string PersonAddress { get; set; }

    public Person Person { get; set; }
}
class Blog
{
    public Blog()
    {
        Posts = new HashSet<Post>();
    }
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Post> Posts { get; set; }
}
class Post
{
    public int Id { get; set; }
    public int? BlogId { get; set; }
    public string Title { get; set; }

    public Blog Blog { get; set; }
}
class Book
{
    public Book()
    {
        Authors = new HashSet<Author>();
    }
    public int Id { get; set; }
    public string BookName { get; set; }

    public ICollection<Author> Authors { get; set; }
}
class Author
{
    public Author()
    {
        Books = new HashSet<Book>();
    }
    public int Id { get; set; }
    public string AuthorName { get; set; }

    public ICollection<Book> Books { get; set; }
}


class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ETicaretDB;User ID=sa;Password=123456");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>()
            .HasOne(a => a.Person)
            .WithOne(p => p.Address)
            .HasForeignKey<Address>(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Blog)
            .WithMany(b => b.Posts)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books);
    }
}