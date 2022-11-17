using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Querriying
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ETicaretContext context = new();

            #region En temel basit sorgulama nasıl yapılır?
            #region Method Syntax
            //Sorgulama sürecinde mantığı metodlar ile kuruyorsak, bu metod syntaxtir.
            //var urunler = await context.Urunler.ToListAsync();
            #endregion
            #region Query Syntax 
            // Ürünler tablosundaki her bir ürünü çek.Linq sorgusu.
            //var urunler =  await (from u in context.Urunler select u).ToListAsync();  
            #endregion
            #endregion

            #region IQueryable ve IEnumerable Nedir?
            // var urunler = from u in context.Urunler select u; // Quaryable
            // var urunler = await (from u in context.Urunler select u).ToListAsync(); // IEnumerable
            #region IQueryable;
            //Sorguya karşılık gelir.
            //Ef core üzerinden yapılmış olan sorgunun execute edilmemiş halini ifade eder.
            #endregion
            #region IEnumerable;
            //Sorgunun çalışıtırılıp/execute edilip verilein in memory'e yüklenmiş halini ifade eder.
            #endregion
            #endregion

            #region Sorguyu Execute Etmek İçin Ne Yapmamız Gerekir?

            //Foreach ile; execute edilmesi gerektiğini anlar.

            //var urunler = from u in context.Urunler select u;
            //foreach (Urun urun in urunler)
            //{
            //    Console.WriteLine(urun.UrunAdi);
            //}
            #endregion

            #region Deffered Execution(Ertelenmiş Çalışma)
            // IQueryable çalışmalarında ilgili kod yazıldığı noktada tetiklenmez/çalıştırılmaz. Yani ilgili kod yazıldığı noktada sorguyu generate etmez.
            //Nerede eder? Çalıştırıldığında / execute edildiği noktada tetiklenir.İşte bu duruma ertelenmiş çalışma denir.
            //int urunId = 5;
            //var urunler = from u in context.Urunler where u.UrunId > urunId select u;
            //urunId = 200;
            //foreach (Urun urun in urunler)
            //{
            //    Console.WriteLine(urun.UrunAdi); // urunId ' si 200 den büyük olanları getirir.
            //}
            //await urunler.ToListAsync(); // Bu durum sadece foreach'te geçerli değil. ToListAsync() ile de aynı çalışma sergilenir.
            #endregion

            #region Çoğul Veri Getiren Sorgulama Fonksiyonları

            #region ToListAsync
            //Üretilen sorguyu execute ettirmemizi sağlayan bir fonksiyoundur. IQueryable > IEnumerable
            //var urunler = context.Urunler.ToListAsync();
            //var urunler = (from u in context.Urunler select u).ToListAsync();
            #endregion

            #region Where
            //Oluşturulan sorguya where şartı eklememizi sağlayan bir fonksiyondur.
            //var urunler = context.Urunler.Where(u => u.UrunId >= 500).ToListAsync();
            //var urunler = context.Urunler.Where(u => u.UrunAdi.StartsWith("A")).ToListAsync(); // A ile başlayan ürünler. Arka planda Like 'a%' sorgusunu çalıştırır.
            //var urunler = from u in context.Urunler where u.UrunId > 500 && u.UrunAdi.EndsWith("7") select u;
            //var data = await urunler.ToListAsync();
            #endregion

            #region OrderBy 
            //Sorgu üzerinde sıralama yapmamızı sağlayan bir fonksiyondur.Default olarak asc (ascending sıralama yapar).
            //var urunler = context.Urunler.Where(u => u.UrunId > 500 || u.UrunAdi.EndsWith("2")).OrderBy(u => u.UrunAdi); // Metod syntax
            //var urunler = from u in context.Urunler where u.UrunId > 500 || u.UrunAdi.StartsWith("2") orderby u.UrunAdi select u; // Query syntax
            #endregion

            #region ThenBy
            //OrderBy üzerinde yapılan sıralama işlemini farklı kolonlarda uygulamamızı sağlayan fonksiyondur.
            //Örneğin ürün adı aynı olan iki veri var, bunları da kendi aralarında ThenBy ile sıralayabiliyoruz.
            //var urunler = context.Urunler.Where(u => u.UrunId > 500 || u.UrunAdi.EndsWith("2")).OrderBy(u => u.UrunAdi).ThenBy(u => u.Parcalar);
            #endregion

            #region OrderByDescending 
            //Büyükten küçüğe. Azalan sırada sıralar.
            #endregion

            #region ThenByDescending
            //Büyükten küçüğe. Azalan sırada sıralar.
            #endregion

            #endregion

            #region Tekil Veri Getiren Sorgulama Fonksiyonları
            //Yapılan sorguda sade ve sadece tek bir verinin gelmesi amaçlanıyorsa Single ya da SingleOrDefault fonksiyonları kullanılabilir.
            //Yapılan çalışmanın mantığında birden fazla verinin geldiği durumda bir patlamayla yazılımı uyarmak istiyorsan burada kullanman gereken temel sorgulayıcı fonk. Single ,SingleOrDefault.
            #region SingleAsync
            //Eğer ki, sorgu neticesinde birden fazla veri geliyorsa ya da hiç gelmiyorsa her iki durumda da exception fırlatır.
            //var urun = await  context.Urunler.SingleAsync(u => u.UrunAdi == "ssd");
            //Console.WriteLine();
            #endregion

            #region SingleOrDefaultAsync
            //Sorgu sonucunda birden fazla değer geliyorsa exception fırlatır,hiç veri gelmiyorsa null döner.
            //var urun2 = await context.Urunler.SingleOrDefaultAsync(p => p.UrunAdi == "Kazak");

            #endregion

            #region FirstAsync
            //Sorgu neticesinde tek bir veriye odaklanıyosam,(tekrar eden verilerden de bir tanesi olabilir) örneğin veritabanındaki Ahmet isimli kullanıcılardan ilkini getirir.
            //Eğer ki hiç veri gelmiyorsa hata fırlatır. Elde edilen verilerden ilkini getirir.
            //var urun = await context.Urunler.FirstAsync(p => p.UrunAdi == "Pantolon");
            //Console.WriteLine(urun.UrunAdi);
            #endregion

            #region FirstOrDefaultAsync
            //Sorgu neticesinde elde edilen verilerlen ilkini getirir. Eğer ki hiç veri gelmiyorsa null değerini döndürür.
            //var urun = await context.Urunler.FirstOrDefaultAsync(p => p.UrunAdi == "asdsd");
            //Console.WriteLine(urun);
            #endregion

            #region Find
            //Filtreleme işlemini primary key'e göre yapacaksan, Find()' i kullanabilirsin.Hızlıdır.
            //Verilen Id değeri veri tabanında herhangi bir veriye karşılık gelmiyorsa hata vermez.
            //Urun urun = await context.Urunler.FindAsync(35); // Lambda expression yok, id değeri direkt olarak verilir.
            //Console.WriteLine();
            #endregion

            #region LastAsync
            //Last ve LastOrDefault ile First ve FirstOrDefault davranışları tamamen aynıdır. Sadece gelen verilerden sonuncusunu alır.
            //Sorgu neticesinde hiç veri gelmezse hata fırlatır.
            //Last veya LasrOrDefault kullanırken OrderBy yapmamız lazım.
            //Urun urun = await context.Urunler.OrderBy(u => u.Fiyat).LastAsync(u => u.UrunAdi.StartsWith("Pantolon"));
            //Console.WriteLine();
            #endregion
            #region LastOrDefaultAsync
            // Sorgu neticesinde veri gelmezse hata fırlatmaz.null döner.
            #endregion
            #endregion

            #region CountAsync
            // Oluşturulan sorgunun execute edilmesi neticesinde kaç adet satırın elde edileceğini sayısal olarak(int) bizlere bildiren fonksiyondur.
            //var urunler = (await context.Urunler.ToListAsync()).Count();
            //var urunler = await context.Urunler.CountAsync(); // ToListAsync() çalıştırmana gerek yok. CountAsync tetiklendiği anda sorguya count fonksiyonunu ekleyecek ve execute edecek.
            //Yani sonucu belleğe çekip saymana gerek yok, direkt sonucu alabiliriz.
            //Console.WriteLine(urunler);
            #endregion

            #region LongCountAsync: Count int döner. Veri 5 milyar tanedir. Bunu int karşılamaz. LongCountAsync kullanılır.
            // Şartlı verileri de sayabilirsin.
            //var urunler = await context.Urunler.LongCountAsync(p => p.Fiyat > 200);
            //Console.WriteLine();
            #endregion

            #region AnyAsync
            //Sorgu neticesinde verinin gelip gelmediğini bool türünde dönen fonksiyondur. Şu sorgu neticesinde veri geliyor mu? gelmiyor mu?
            //var urunler = await context.Urunler.AnyAsync(u => u.Fiyat == 20000000);
            Console.WriteLine();
            #endregion

            #region MaxAsync
            //var fiyat = await context.Urunler.MaxAsync(u => u.Fiyat);
            //Console.WriteLine(fiyat);
            #endregion

            #region MinAsync
            //var fiyat = await context.Urunler.MinAsync(u => u.Fiyat);
            //Console.WriteLine(fiyat);
            #endregion

            #region Distinct
            //Sorguda mükerrer kayıtlar varsa bunları tekilleştiren bir işleve sahip fonksiyondur.
            //Distinct() dediğin anda hala IQueryable'dır. Çalışması için execute etmen lazım. ToListAsync().
            //var urunler = await context.Urunler.Distinct().ToListAsync();
            //foreach (var item in urunler)
            //{
            //    Console.WriteLine(item.UrunAdi);
            //}
            #endregion

            #region AllAsync
            //Bir sorgu neticesinde gelen verilerin,verilen şarta uyup uymadığını kontrol etmektedir. Eğer ki tüm veriler şarta uyuyorsa true, uymuyorsa false döndürecektir.
            //var m = await context.Urunler.AllAsync(u => u.Fiyat >= 0);
            //Console.WriteLine(m);
            #endregion

            #region SumAsync
            //Toplam fonksiyonu.
            //var fiyatToplam = await context.Urunler.SumAsync(u => u.Fiyat);
            //Console.WriteLine(fiyatToplam);
            #endregion

            #region AverageAsync
            //Vermiş olduğumuz sayısal propertyin aritmetik ortalamasını alır.
            //var aritmetikOr = await context.Urunler.AverageAsync(u => u.Fiyat);
            //Console.WriteLine(aritmetikOr);
            #endregion

            #region ContainAsync
            //Like sorgusu oluşturmamızı sağlar. Where ile beraber kullanılır.
            //var urunler = await context.Urunler.Where(u => u.UrunAdi.Contains("a")).ToListAsync();
            //foreach (var item in urunler)
            //{
            //    Console.WriteLine(item.UrunAdi);
            //}
            #endregion

            #region StartsWith
            //var urunler = await context.Urunler.Where(u => u.UrunAdi.StartsWith("a")).ToListAsync();

            //foreach (var item in urunler)
            //{
            //    Console.WriteLine(item.UrunAdi);
            //}
            #endregion

            #region EndsWith
            // var urunler = await context.Urunler.Where(u => u.UrunAdi.EndsWith("a")).ToListAsync();

            //foreach (var item in urunler)
            //{
            //    Console.WriteLine(item.UrunAdi);
            //}
            #endregion

            #region Sorgu Sonucu Dönüşüm Fonksiyonları
            //Bu fonksiyonlar ile sorgu neticesinde elde edilen verileri isteğimiz doğrultusunda farklı türlerde projecsiyon edebiliyoruz.

            #region ToDictionaryAsync
            //Sorgu neticesinde gelecek olan veriyi bir dictionary olarak elde etmek/karşılamak istiyorsak kullanılır.{Key,value}
            //ToList ile aynı amaca hizmet vermektedir. Yani,oluşturulan sorguyu execute edip neticesini alırlar.ToList -> List<TEntity>.ToDictionary -> {key,value}
            //var urun = await context.Urunler.ToDictionaryAsync(u => u.UrunAdi, u => u.Fiyat);
            //Console.WriteLine();
            #endregion

            #region ToArrayAsync   
            //Oluşturulan sorguyu dizi olarak elde eder. ToList ile muadil amaca hizmet eder.Yani, sorguyu execute eder, gelen sonucu entity dizisi olarak elde eder.
            //var urunler = await context.Urunler.ToArrayAsync();
            //for (int i = 0; i < urunler.Length; i++)
            //{
            //    Console.WriteLine(urunler[i].UrunAdi);
            //}
            #endregion

            #region Select
            //İşlevsel olarak birden fazla davranışı söz konusudur.
            //1) Select fonksiyonu,generate edilecek sorgunun çekilecek kolonlarını ayarlamamızı sağlamaktadır.
            //var urunler = await context.Urunler.Select(u => new Urun
            //{
            //    UrunId = u.UrunId,
            //    Fiyat = u.Fiyat
            //}).ToListAsync();

            //Console.WriteLine();

            //2) Select fonksiyonu, gelen verileri farklı türlerde karşılamamızı sağlar.T, anonim.

            //var urunler = await context.Urunler.Select(u => new
            //{
            //    UrunId = u.UrunId,
            //    Fiyat = u.Fiyat
            //}).ToListAsync();

            //Console.WriteLine();

            #region C#'ta anonim tip
            //Tipsiz
            var d = new
            {
                A = "Ahmet"
            };
            #endregion
            #endregion

            #region SelectMany
            //Select ile aynı amaca hizmet eder.Lakin, ilişkisel tablolar neticesinde gelen koleksiyonel verileri de tekilleştirip projeksiyon etmemizi sağlar.
            //Join yapılarda kullanılır.
            //var urunler = await context.Urunler.Include(u => u.Parcalar).SelectMany(u => u.Parcalar, (u, p) => new
            //{
            //    u.UrunId,
            //    u.Fiyat,
            //    p.ParcaAdi
            //}).ToListAsync();
            //Console.WriteLine();
            #endregion

            #region GroupBy Fonksiyonu
            //Gruplama yapmamızı sağlayan fonk.
            //Method Syntax
            //var datas = await context.Urunler.GroupBy(u => u.Fiyat).Select(group => new
            //{
            //    miktar = group.Count(),
            //    fiyat = group.Key
            //}).ToListAsync();
            //foreach (var item in datas)
            //{
            //    Console.WriteLine("Fiyat : " + item.fiyat + " Miktar : " + item.miktar);
            //}
            //Query Syntax
            var datas = from u in context.Urunler
                        group u by u.Fiyat into g
                        select new
                        {
                            miktar = g.Count(),
                            fiyat = g.Key //Hangi kolonun değerini gruplayıp saydırdıysan key değeri o kolondur.
                        };
            foreach (var item in datas)
            {
                Console.WriteLine("Fiyat : " + item.fiyat + " Miktar : " + item.miktar);
                //}

                #endregion
                #region Foreach Fonksiyonu
                //Foreach döngüsünün metod halidir.
                await datas.ForEachAsync(x =>
                {
                });
                
                #endregion
                #endregion
            }
        }
    }
}
