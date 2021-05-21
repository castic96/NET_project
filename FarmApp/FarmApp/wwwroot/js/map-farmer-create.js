/** Map. */
let map;

/** List of markers. */
let markers = [];

/**
 *  Initializes the map.
 */
function initMap() {
    map = new google.maps.Map(document.getElementById("map-farmer"), {
        zoom: 12,
        center: { lat: 49.739, lng: 13.372 },
    });

    document.getElementById("submit").addEventListener("click", () => {
        geocodeAddress(map);
    });
}

/**
 *  Geocodes the address.
 */
function geocodeAddress(resultsMap) {    
    let geocoder = new google.maps.Geocoder();
    const address = document.getElementById("address").value;

    deleteMarkers();

    geocoder.geocode({ address: address }, (results, status) => {
        if (status === "OK") {
            resultsMap.setCenter(results[0].geometry.location);
            addMarker(results[0].geometry.location);
            fillAddress(results[0]);
        } else {
            alert("Geocode was not successful for the following reason: " + status);
        }
    });
}

/**
 *  Fills address and coordinates into fields on page.
 */
function fillAddress(address) {
    document.getElementById("address-input").value = address.formatted_address;
    document.getElementById("lat-input").value = address.geometry.location.lat();
    document.getElementById("lng-input").value = address.geometry.location.lng();
}

/**
 *  Adds marker to the map.
 */
function addMarker(location) {
    const marker = new google.maps.Marker({
        position: location,
        map: map,
    });
    markers.push(marker);
}

/**
 *  Sets all markers to the map.
 */
function setMapOnAll(map) {
    for (let i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
}

/**
 *  Removes markers from the map.
 */
function clearMarkers() {
    setMapOnAll(null);
}

/**
 *  Deletes markers from the list of markers.
 */
function deleteMarkers() {
    clearMarkers();
    markers = [];
}