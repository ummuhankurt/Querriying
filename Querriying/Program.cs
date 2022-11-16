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
            // Nerede eder? Çalıştırıldığında/execute edildiği noktada tetiklenir. İşte bu duruma ertelenmiş çalışma denir.
            //int urunId = 5;
            //var urunler = from u in context.Urunler where u.UrunId > urunId select u;
            //urunId = 200;
            //foreach (Urun urun in urunler)
            //{
            //    Console.WriteLine(urun.UrunAdi); // urunId ' si 200 den büyük olanları getirir.
            //}
            #endregion
        }
    }
}
