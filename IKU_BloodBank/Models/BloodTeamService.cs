using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace IKU_BloodBank.Models
{
    public class BloodTeamService
    {
        public void ImportBloodTeams(string xmlData)
        {
            // 1. Veriyi sanal bir root etiketi (<Root> ... </Root>) içine alıyoruz.
            // İstersen <Root> yerine <BloodTeams> gibi daha anlamlı bir isim de verebilirsin.
            
            try
            {
                // Sanal root ekliyoruz (Bir önceki adımdan)
                string validXmlData = $"<Root>{xmlData}</Root>";
                XDocument xDoc = XDocument.Parse(xmlData);

                // DİKKAT: .Descendants("BloodTeamDTO") YERİNE LocalName kullanıyoruz!
                List<BloodTeam> bloodTeams = xDoc.Descendants()
                    .Where(x => x.Name.LocalName == "BloodTeamDTO") // Namespace ne olursa olsun adı bu olanları bul
                    .Select(x => new BloodTeam
                    {
                        // İçerideki elemanları okurken de x.Element() YERİNE LocalName ile arıyoruz
                        TeamName = (string)x.Elements().FirstOrDefault(e => e.Name.LocalName == "TeamName"),
                        Address = (string)x.Elements().FirstOrDefault(e => e.Name.LocalName == "Address"),
                        CityId = (string)x.Elements().FirstOrDefault(e => e.Name.LocalName == "CityId"),
                        CityIdArea = (string)x.Elements().FirstOrDefault(e => e.Name.LocalName == "CityIdArea"),
                        Town = (string)x.Elements().FirstOrDefault(e => e.Name.LocalName == "Town"),
                        Neighbourhood = (string)x.Elements().FirstOrDefault(e => e.Name.LocalName == "Neighbourhood"),

                        // XML'de <Phone>, modelde PhoneNumber
                        PhoneNumber = (string)x.Elements().FirstOrDefault(e => e.Name.LocalName == "Phone"),

                        // Sayısal değerler (Virgül/nokta hatalarını önlemek için)
                        Latitude = (double?)x.Elements().FirstOrDefault(e => e.Name.LocalName == "Latitude") ?? 0.0,
                        Longitude = (double?)x.Elements().FirstOrDefault(e => e.Name.LocalName == "Longitude") ?? 0.0,
                        Platelets = (bool?)x.Elements().FirstOrDefault(e => e.Name.LocalName == "Platelets") ?? false

                    }).ToList();

                Console.WriteLine($"Bulunan Kayıt Sayısı: {bloodTeams.Count}");

                // 4. Veritabanına kaydetme işlemi
                //context.BloodTeams.AddRange(bloodTeams);
                //context.SaveChanges();
                using (var db = new BloodTeamDbContext())
                {
                    db.BloodTeams.AddRange(bloodTeams);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Eğer XML formatında başka bir bozukluk varsa buraya düşer
                Console.WriteLine($"XML dönüştürme hatası: {ex.Message}");
            }


        }
        //public void ImportBloodTeams(string xmlData)
        //{
        //    // 1. XML verisini parse et (Eğer dosyadan okuyacaksan XDocument.Load("dosyayolu.xml") kullanabilirsin)
        //    // Eğer veride Root elemanı yoksa, XDocument.Parse işleminin çalışması için 
        //    // veriyi sanal bir <Root> etiketi içine almak gerekebilir.
        //    XDocument xDoc = XDocument.Parse(xmlData);

        //    // 2. XML'deki "BloodTeamDTO" düğümlerini bul ve BloodTeam sınıfına eşleştir
        //    List<BloodTeam> bloodTeams = xDoc.Descendants("BloodTeamDTO").Select(x => new BloodTeam
        //    {
        //        // Metinsel değerler doğrudan string'e cast edilebilir
        //        TeamName = (string)x.Element("TeamName"),
        //        Address = (string)x.Element("Address"),
        //        CityId = (string)x.Element("CityId"),
        //        CityIdArea = (string)x.Element("CityIdArea"),
        //        Town = (string)x.Element("Town"),
        //        Neighbourhood = (string)x.Element("Neighbourhood"),

        //        // Dikkat: XML'de etiket <Phone>, fakat sınıfında property PhoneNumber
        //        PhoneNumber = (string)x.Element("Phone"),

        //        // Sayısal ve boolean değerleri (nullable) cast ederek almak hata almanı önler
        //        // Eğer veri boş gelirse ?? operatörü ile varsayılan (0.0 veya false) atıyoruz.
        //        // XElement double cast işlemi noktayı (.) evrensel ondalık ayırıcı olarak otomatik algılar.
        //        Latitude = (double?)x.Element("Latitude") ?? 0.0,
        //        Longitude = (double?)x.Element("Longitude") ?? 0.0,
        //        Platelets = (bool?)x.Element("Platelets") ?? false

        //    }).ToList();

        //    // 3. Elde edilen listeyi Entity Framework ile veritabanına kaydet
        //    ///*

        //    using (var db = new BloodTeamDbContext())
        //    {
        //        db.BloodTeams.AddRange(bloodTeams);
        //        db.SaveChanges();
        //    }


        //    //*/
        //}
    }
}