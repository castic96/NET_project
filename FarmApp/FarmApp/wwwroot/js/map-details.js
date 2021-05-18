let map;
let markers = [];

function initMap() {
    let latitude = Number(document.getElementById('latitude').value);
    let longitude = Number(document.getElementById('longitude').value);
    let position = { lat: latitude, lng: longitude };


    map = new google.maps.Map(document.getElementById("map-details"), {
        zoom: 12,
        center: position
    });

    new google.maps.Marker({
        position: position,
        map: map,
    });

}