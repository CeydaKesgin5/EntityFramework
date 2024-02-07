// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;

#region Relationships Terimleri
#region Principal Entity(Asıl Entity)
//Kendi başına var olabilen tabloyu modelleyen entity'e denir.
//Departmanlar tablosunu modelleyen 'Departman' entitysidir.
#endregion

#region Dependent Entity(Bağınlı Entity) Nedir?
//Kendi başına var olamayan, bir başka tabloya bağmlı(ilişkisel olarak bağımlı)olan tabloyu modelleyen entity'ye denir.
//Calisanlar tablosunu modelleyen 'Calisan' entity'sidir.
#endregion

#region Foreign Key
//Principal Entity ile Dependent Entity arasındaki ilişkiyi sağlayan key'dir.
#endregion

#region Principal Key
//Principal Entity'deki id'nin kendisidir.
//Principal Entity'de kimliği olan kolonu ifade eden propertydir.
#endregion

#region Navigation Property Nedir?
//İlişkisel tablolar arasındaki fiziksel erişimi entity class'ları üzerinden sağlayan property'lerdir.
//Bir propertynin Navigation Property olabilmesi için kesinlikle entity türünden olması gerekiyor.
/*Navigation Property'ler entity'deki tanımlarına göre çoka çok ya da 1'e çok şeklinde ilişki türlerine
  ifade etmektedir. 
 */
#endregion

#region Entity Framework Core'da İlişki Yapılandırma Yöntemleri
#region Default Conventions
//Varsayılan enetity kurallarını kullanarak yapılan ilişki yapılandırma yöntemleridir.
//Navigation propertyleri kullanarak ilişki şablonlarını çıkarmaktadır.
#endregion

#region Data Annotations Attributes
//Entity'nin niteliklerine göre ince ayarlar yapmamızı sağlayan attributelerdir. 
//[Key], [ForeignKey]
#endregion

#region Fluent API
//Entity modellerindeki ilişkileri yapılandırırken daha detaylı çalışmanızı sağlayan yöntemdir.

#region HasOne
/*İlgili entity'nin ilişkisel entity'e 1'e 1 ya da 1'e çok olacak şekilde
 * ilişkisini yapılandırmaya başlayan metottur.
 */
#endregion

#region HasMany
/*İlgili entity'nin ilişkisel entity'e çoka bir ya da çoka çok olacak şekilde
 * ilişkisini yapılandırmaya başlayan metottur.
 */
#endregion


#region WithOne
/*HasOne ya da HasMany'den sonra 1'e 1 ya da çoka 1 olacak şekilde
 * ilişkisi yapılandırmasını tamamlayan metottur.
 */
#endregion

#region WithMany
/*HasOne ya da HasMany'den sonra 1'e çok ya da çoka çok olacak şekilde
 * ilişkisi yapılandırmasını tamamlayan metottur.
 */
#endregion

#endregion


#endregion

#endregion
class Calisan
{
    public int Id { get; set; }
    public string CalisanAdi { get; set; }
    public string DepartmanId { get; set; }

    public Departman Departman { get; set; }//Navigation Property
}
class Departman
{
    public int Id { get; set; }
    public string DepartmanAdi { get; set; }
    public ICollection<Calisan> Calisanlar { get; set; }//Navigation Property



}

