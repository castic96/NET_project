// Note: This example requires that you consent to location sharing when
// prompted by your browser. If you see the error "The Geolocation service
// failed.", it means you probably did not give permission for the browser to
// locate you.
let map, infoWindowMyLocation;

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
    let infoWindowDescription = new google.maps.InfoWindow();

    for (let i = 0; i < results.shops.length; i++) {
        let currentShop = results.shops[i];

        const latLng = new google.maps.LatLng(currentShop.latitude, currentShop.longitude);

        let shopId = currentShop.id;
        let shopName = currentShop.name;
        let shopStreet = currentShop.street;
        let shopCity = currentShop.city;
        let shopPostalCode = currentShop.postalcode;

        let infoWindowContent = document.createElement('div');
        let header = document.createElement('strong');
        header.textContent = shopName;
        infoWindowContent.appendChild(header);
        infoWindowContent.appendChild(document.createElement('br'));

        let content = document.createElement('text');
        content.textContent = shopStreet + ", " + shopPostalCode + " " + shopCity;
        infoWindowContent.appendChild(content);
        infoWindowContent.appendChild(document.createElement('br'));
        infoWindowContent.appendChild(document.createElement('br'));

        let detailsButton = document.createElement("button");
        detailsButton.textContent = "Details";
        infoWindowContent.appendChild(detailsButton);

        detailsButton.addEventListener("click", () => {
            window.location.href = '/Authorized/Customer/Find/Details/' + shopId;
        });

        let marker = new google.maps.Marker({
            position: latLng,
            map: map
        });

        marker.addListener("click", function () {
            infoWindowDescription.setContent(infoWindowContent);
            infoWindowDescription.open(map, marker);
        });
    }
};