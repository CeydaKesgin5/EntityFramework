// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
ApplicationDbContext context = new ApplicationDbContext();

#region 1 to 1 İlişkilsel senaryolarda veri güncelleme

#region 1. Durum | Esas tablodaki veriye bağımlı veriyi değiştirme
//Person? person = await context.Persons
//    .Include(p => p.Address)
//    .FirstOrDefaultAsync(p => p.Id == 1);

//context.Addresss.Remove(person.Address);
//person.Address = new()
//{
//    PersonAddress = "Yeni Adres"
//};
//await context.SaveChangesAsync();


#endregion

#region 2. Durum | Bağımlı verinin ilişkisel olduğu ana veriyi güncelleme
//Address? address = await context.Addresss.FindAsync(2);//adresi elde ettik
//context.Addresss.Remove(address);//sildik
//await context.SaveChangesAsync();

//address.Person = new()//yeni bir person oluşturduk
//{
//    Name = "Rıfkı"
//};
//await context.Addresss.AddAsync(address);
//await context.SaveChangesAsync();

#endregion
//class Person
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public Address Address { get; set; }
//}
//class Address
//{
//    public int Id { get; set; }
//    public string PersonAddress { get; set; }
//    public Person Person { get; set; }

//}
//class ApplicationDbContext : DbContext
//{
//    public DbSet<Person> Persons { get; set; }
//    public DbSet<Address> Addresss { get; set; }
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//    }
//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Address>()
//            .HasOne(a => a.Person)
//            .WithOne(p => p.Address)
//            .HasForeignKey<Address>(a => a.Id);
//    }
//}
#endregion

#region 1 to Many İlişkilsel senaryolarda veri güncelleme
#region 1. Durum | Esas tablodaki veriye bağımlı veriyi değiştirme
//Blog? blog = await context.Blogs
//    .Include(b => b.Posts)
//    .FirstOrDefaultAsync(b => b.Id == 1);

//Post? silinecekPost=blog.Posts.FirstOrDefault(p => p.Id == 2);
//blog.Posts.Remove(silinecekPost);
//blog.Posts.Add(new() { Title = "4. Post" });
//blog.Posts.Add(new() { Title = "5. Post" });

//await context.SaveChangesAsync();
#endregion

#region 2. Durum | Bağımlı verinin ilişkisel olduğu ana veriyi güncelleme
////1.seçenek
////Post? post =await context.Posts.FirstAsync(4);
////post.Blog = new()
////{
////    Name = "2. Blog"
////};
////await context.SaveChangesAsync();

//2.seçenek
//Post? post =await context.Posts.FindAsync(4);
//Blog? blog = await context.Blogs.FindAsync(2);
//post.Blog = blog;
//await context.SaveChangesAsync();


//#endregion
//class Blog
//{
//    public Blog()
//    {
//        Posts = new HashSet<Post>();
//    }
//    public int Id { get; set; }
//    public string Name { get; set; }

//    public ICollection<Post> Posts { get; set; }
//}
//class Post
//{
//    public int Id { get; set; }
//    public int BlogId { get; set; }
//    public string Title { get; set; }
//    public Blog Blog { get; set; }

//}

//class ApplicationDbContext : DbContext
//{
//    public DbSet<Blog> Blogs { get; set; }
//    public DbSet<Post> Posts { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//    }
//}
#endregion
#region Many to Many İlişkilsel senaryolarda veri güncelleme
#region 1. örnek
//Book? book=await context.Books.FindAsync(1);
//Author? author = await context.Author.FindAsync(3);
//book.Authors.Add(author);

#endregion
#region 2. örnek
Author? author = await context.Authors
    .Include(a => a.Books)
    .FirstOrDefaultAsync(a => a.Id == 3);

foreach(var book in author.Books)
{
    if(book.Id == 1)
    {
        author.Books.Remove(book);
    }
}
await  context.SaveChangesAsync();
#endregion


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
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }


}
#endregion