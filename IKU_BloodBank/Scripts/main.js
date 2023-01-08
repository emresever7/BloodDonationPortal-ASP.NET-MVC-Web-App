const navToggle = document.querySelector(".mobile-nav");
const primaryNav = document.querySelector(".nav");

navToggle.addEventListener("click", ()=> {
    primaryNav.hasAttribute("data-visible") ? navToggle.setAttribute("aria-expanded", false) : navToggle.setAttribute("aria-expanded", true);
    primaryNav.toggleAttribute("data-visible");
});
  
/* Search Bar */

function myFunction() {
    var input, filter, ul, li, a, i, txtValue;
    input = document.getElementById("search");
    filter = input.value.toUpperCase();
    ul = document.getElementById("map-list");
    li = ul.getElementsByTagName("table");
    for (i = 0; i < li.length; i++) {
        a = li[i].getElementsByTagName("tr")[0];
        txtValue = a.textContent || a.innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }
}

//var teamNames = document.getElementsByClassName("teamName");
//var neighbourhoods = document.getElementsByClassName("city");
//var phoneNumbers = document.getElementsByClassName("phone-number");
//var longitudes = document.getElementsByClassName("longitude");
//var latitudes = document.getElementsByClassName("latitude");

/* Map Settings */

var map = L.map('map').setView([40.99130848477239, 28.83212363020195], 13);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
}).addTo(map);


L.Control.geocoder().addTo(map);

//for (i = 0; i<TeamName.length; i++)
//{
//    var marker = L.marker([valueof(Longitude[i]), valueof(Latitude[i])]).addTo(map);

//    marker.bindPopup(valueof(TeamName[i]) + "</b><br/>" + valueof(Neighbourhood[i]) + "<br/>" + valueof(PhoneNumber[i])).openPopup();
//}


fetch('/BloodTeams/GetTeamsForMap')
    .then(response => response.json())
    .then(data => {
        // 1. KONTROL: Veri gerçekten geldi mi? Konsola yazdırıp bakalım.
        console.log("Sunucudan gelen toplam veri sayısı: ", data.length);

        if (data.length > 0) {
            // İlk verinin yapısını konsola yazdır, böylece isimlerin nasıl geldiğini (büyük/küçük) görürüz
            console.log("Örnek Veri Yapısı: ", data[0]);
        }

        data.forEach(team => {
            // 2. ÇÖZÜM: Hem küçük harf hem büyük harf ihtimalini destekle (Hangisi doluysa onu alır)
            var lat = team.latitude || team.Latitude;
            var lng = team.longitude || team.Longitude;
            var name = team.teamName || team.TeamName;
            var hood = team.neighbourhood || team.Neighbourhood;
            var phone = team.phoneNumber || team.PhoneNumber;

            // 3. Virgüllü metin gelme ihtimaline karşı zorla sayıya çevir
            var finalLat = parseFloat(lat?.toString().replace(',', '.'));
            var finalLng = parseFloat(lng?.toString().replace(',', '.'));

            // 4. Koordinatlar geçerli bir sayı ise haritaya bas
            if (!isNaN(finalLat) && !isNaN(finalLng) && finalLat !== 0 && finalLng !== 0) {
                var marker = L.marker([finalLat, finalLng]).addTo(map);

                var popupContent = `
                    <b>${name || 'Bilinmeyen Takım'}</b><br/>
                    ${hood || ''}<br/>
                    Tel: ${phone || '-'}
                `;

                marker.bindPopup(popupContent);
            } else {
                // Eğer bir veride koordinat yoksa veya bozuksa bizi uyar
                console.warn(`Hatalı Koordinat! Takım: ${name}, Lat: ${lat}, Lng: ${lng}`);
            }
        });
    })
    .catch(error => {
        console.error("Harita verileri çekilirken bir hata oluştu:", error);
    });


