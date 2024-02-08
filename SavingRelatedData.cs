// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;


#region One to One İlişkisel Senaryolarda Veri Ekleme 
//ApplicationDbContext context = new();
#region 1. Yöntem->Principal Entity Üzerinden Dependent Entity Verisi Ekleme
//Person person = new Person();
//person.Name = "Ceyda";
//person.Address = new() { PersonAddress="Eskişehir"};

//await context.AddAsync();
//await context.SaveChangesAsync();
#endregion
/* Eğer ki Principal Entity Üzerinden ekleme gerçekleştiriliyorsa dependent entity nesnesi
 * verilmek zorunda değildir! Ama, dependent entity üzerinden ekeleme işlemei gerçekleştiriliyorsa 
 * eğer burada Principal Entitynin nesnesine ihtiyacımız zorunludur.
 */
#region 2. Yöntem->Dependent Entity Üzerinden Principal Entity Verisi Ekleme
//Address address = new Address() { 
//    PersonAddress="Eskişehir",
//    Person = new() { Name="Ceyda"}

//};


//await context.AddAsync(address);
//await context.SaveChangesAsync();


#endregion
//class Person
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public Address Address { get; set; }
//}
//class Address {
//    public int Id { get; set; }
//    public string PersonAddress { get; set; }
//    public Person Person { get; set; }

//}
//class ApplicationDbContext : DbContext
//{
//    public DbSet<Person> Persons { get; set;}
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

//    internal Task AddAsync()
//    {
//        throw new NotImplementedException();
//    }
//}

#endregion


#region One to Many İlişkisel Senaryolarda Veri Ekleme 
//ApplicationDbContext context = new ApplicationDbContext();
#region 1. Yöntem->Principal Entity Üzerinden Dependent Entity Verisi Ekleme
#region Nesne Referansı Üzerinden Ekleme
//Blog blog = new Blog() { Name="ceyda.com Blog"};
//blog.Posts.Add(new() { Title = "Post 1" });
//blog.Posts.Add(new() { Title = "Post 2" });
//blog.Posts.Add(new() { Title = "Post 3" });

//await context.AddAsync(blog);
//await context.SaveChangesAsync();

#endregion
#region Object Initilizer Üzerinden Ekleme
//Blog blog1=new Blog()
//{
//    Name="A Blog",
//    Posts = new HashSet<Post>()
//    {
//        new(){ Title="Post 4"},
//        new(){ Title="Post 5"},

//    }
//};

//await context.AddAsync(blog1);
//await context.SaveChangesAsync();
#endregion

#endregion

#region 2. Yöntem->Dependent Entity Üzerinden Principal Entity Verisi Ekleme
//Post post = new Post()
//{
//    Title= "Post 6",
//    Blog = new() { Name="B Blog"}
//};
//await context.AddAsync(post);
//await context.SaveChangesAsync();
#endregion

#region Foreign Key Kolonu Üzerinden Veri Ekleme
/* 1. ve 2. yöntemler hiç olmayan verilerin ilişkisel olarak eklenmesini sağlarken
 * 3. yöntem önceden eklenmiş olan ir principal entity verisiyle yeni dependent entitylerin 
 * ilişkisel olarak eşleştirilmesini sağlar.
 */


//Post post = new Post()
//{
//    BlogId = 1,
//    Title="Post 7"
//};
#endregion//class Blog
//{
//    public Blog()
//    {
//        Posts = new HashSet<Post>();
//    }
//    public int Id {  get; set; }
//    public string Name { get; set; }

//    public ICollection<Post> Posts { get; set;}
//}
//class Post { 
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


#region Many to Many İlişkisel Senaryolarda Veri Ekleme 
//ApplicationDbContext context = new ApplicationDbContext();

//#region 1.Yöntem
////n to n ilişkisi eğer ki default convention üzerinden tasarlanmışsa kullanılan bir yöntemdir.
//Book book = new Book()
//{
//    BookName="A Kitabı",
//    Authors=new HashSet<Author>() {
//        new Author(){ AuthorName="hilmi"},
//        new Author(){ AuthorName="hilmi"},
//        new Author(){ AuthorName="hilmi"},

//    }
//};
//await context.Books.AddAsync(book);
//await context.SaveChangesAsync();
//class Book
//{
//    public Book()
//    {
//        Authors = new HashSet<Author>();
//    }
//    public int Id { get; set; }
//    public string BookName { get; set; }
//    public ICollection<Author> Authors { get; set; }
//}


//class Author
//{
//    public Author()
//    {
//        Books = new HashSet<Book>();

//    }
//    public int Id { get; set; }
//    public string AuthorName { get; set; }
//    public ICollection<Book> Books { get; set; }
//}
//class ApplicationDbContext : DbContext
//{
//    public DbSet<Book> Books { get; set; }
//    public DbSet<Author> Author { get; set; }
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//    }




//}
#endregion
#region 2. Yöntem
//n to n ilişkisi eğer ki fluent api üzerinden tasarlanmışsa kullanılan bir yöntemdir.
ApplicationDbContext context = new ApplicationDbContext();

Author author =new Author()
{
    AuthorName="Mustafa",
    Books=new HashSet<BookAuthor>()
    {
        new(){ BookId=1 },
        new(){ Book=new(){BookName="B Kitap" } }

    }
};
await context.AddAsync(author);
await context.SaveChangesAsync();


#endregion
class Book
{
    public Book()
    {
        Authors = new HashSet<BookAuthor>();
    }
    public int Id { get; set; }
    public string BookName { get; set; }
    public ICollection<BookAuthor> Authors { get; set; }
}
class BookAuthor
{
    public int BookId { get; set; }
    public int AuthorId { get; set; }

    public Book Book { get; set; }
    public Author Author { get; set; }


}

class Author
{
    public Author()
    {
        Books = new HashSet<BookAuthor>();

    }
    public int Id { get; set; }
    public string AuthorName { get; set; }
    public ICollection<BookAuthor> Books { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Author { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.AuthorId, ba.BookId });

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Book)
            .WithMany(b => b.Authors)
            .HasForeignKey(ba => ba.BookId);

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Author)
            .WithMany(b => b.Books)
            .HasForeignKey(ba => ba.AuthorId);
    }



}
#endregion