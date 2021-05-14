// Note: This example requires that you consent to location sharing when
// prompted by your browser. If you see the error "The Geolocation service
// failed.", it means you probably did not give permission for the browser to
// locate you.
let map, infoWindowMyLocation, infoWindowDescription;

function initMap() {
    map = new google.maps.Map(document.getElementById("map"), {
        center: { lat: 49.739, lng: 13.372 },
        zoom: 12,
    });

    const script = document.createElement("script");
    script.src =
        "/api/map";

    document.getElementsByTagName("head")[0].appendChild(script);

    infoWindowMyLocation = new google.maps.InfoWindow();
    infoWindowDescription = new google.maps.InfoWindow();

    const locationButton = document.createElement("button");
    locationButton.textContent = "Center to My Location";
    locationButton.classList.add("custom-map-control-button");
    map.controls[google.maps.ControlPosition.TOP_CENTER].push(locationButton);
    locationButton.addEventListener("click", () => {
        // Try HTML5 geolocation.
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                (position) => {
                    const pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude,
                    };
                    infoWindowMyLocation.setPosition(pos);
                    infoWindowMyLocation.setContent("My Location");
                    infoWindowMyLocation.open(map);
                    map.setCenter(pos);
                },
                () => {
                    handleLocationError(true, infoWindowMyLocation, map.getCenter());
                }
            );
        } else {
            // Browser doesn't support Geolocation
            handleLocationError(false, infoWindowMyLocation, map.getCenter());
        }
    });

    locationButton.click();
}

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(
        browserHasGeolocation
            ? "Error: The Geolocation service failed."
            : "Error: Your browser doesn't support geolocation."
    );
    infoWindow.open(map);
}

const showshops_callback = function (results) {
    for (let i = 0; i < results.shops.length; i++) {
        const latLng = new google.maps.LatLng(results.shops[i].latitude, results.shops[i].longitude);
        new google.maps.Marker({
            position: latLng,
            map: map,
        });
    }
};