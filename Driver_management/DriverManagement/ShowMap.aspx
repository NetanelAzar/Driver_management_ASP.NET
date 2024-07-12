<%@ Page Title="" Language="C#" MasterPageFile="~/DriverManagement/DriverMaster.Master" AutoEventWireup="true" CodeBehind="ShowMap.aspx.cs" Inherits="Driver_management.DriverManagement.ShowMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Show Map</title>
    <link rel="stylesheet" href="https://unpkg.com/leaflet/dist/leaflet.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/limonte-sweetalert2/11.0.18/sweetalert2.min.css" />
    <link rel="stylesheet" href="https://unpkg.com/leaflet-routing-machine/dist/leaflet-routing-machine.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/leaflet.markercluster/1.4.1/MarkerCluster.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/leaflet.markercluster/1.4.1/MarkerCluster.Default.css" />
    <link rel="stylesheet" href="https://unpkg.com/leaflet-control-geocoder/dist/Control.Geocoder.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/leaflet-locatecontrol/0.72.0/L.Control.Locate.min.css" />

    <script src="https://unpkg.com/leaflet/dist/leaflet.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/limonte-sweetalert2/11.0.18/sweetalert2.all.min.js"></script>
    <script src="https://unpkg.com/leaflet-routing-machine/dist/leaflet-routing-machine.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/leaflet.markercluster/1.4.1/leaflet.markercluster.js"></script>
    <script src="https://unpkg.com/leaflet-control-geocoder/dist/Control.Geocoder.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/leaflet-locatecontrol/0.72.0/L.Control.Locate.min.js"></script>


    <style>
        #map {
            width: 100%;
            height: 500px;
        }
        .leaflet-popup-content-wrapper, .leaflet-popup-tip {
            background: rgba(255, 255, 255, 0.9);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">

    <div>
        <h2>Display Addresses on Map</h2>
        <br />
        <div id="map"></div>
        <br>
        <button id="downloadExcel">Download Addresses as Excel</button>
    </div>

<script>
    var map = L.map('map').setView([31.0461, 34.8516], 8);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors'
    }).addTo(map);

    var markers = L.markerClusterGroup();

    function loadMap(userId) {
        $.ajax({
            url: 'ShowMap.aspx/GetAddresses',
            data: JSON.stringify({ userId: userId }),
            method: 'POST',
            contentType: 'application/json',
            success: function (data) {
                console.log("Received addresses: ", data);

                if (data && data.d) {
                    var addresses = data.d;
                    var waypoints = [];

                    addresses.forEach(function (address) {
                        var marker = L.marker([parseFloat(address.Latitude), parseFloat(address.Longitude)]).addTo(markers);

                        var popupContent = '<b>Address:</b><br>' + address.Address + ', ' + address.City;
                        popupContent += '<br><a href="#" onclick="window.calculateRoute(' + address.Latitude + ',' + address.Longitude + ');">Get Directions</a>';
                        marker.bindPopup(popupContent);

                        waypoints.push(L.latLng(parseFloat(address.Latitude), parseFloat(address.Longitude)));
                    });

                    map.addLayer(markers);

                    // Remove existing routing control if any
                    if (window.routingControl) {
                        map.removeControl(window.routingControl);
                    }

                    window.routingControl = L.Routing.control({
                        waypoints: waypoints,
                        createMarker: function () { return null; },
                        router: L.Routing.osrmv1({ serviceUrl: 'https://router.project-osrm.org/route/v1' })
                    }).addTo(map);
                } else {
                    console.error("No valid data received.");
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'No valid data received from the server!',
                    });
                }
            },
            error: function (xhr, status, error) {
                console.error("Failed to fetch addresses: ", error);
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Failed to fetch addresses from the server!',
                });
            }
        });
    }

    function calculateRoute(latitude, longitude) {
        var origin = map.getCenter();
        var destination = latitude + ',' + longitude;
        var url = 'https://www.google.com/maps/dir/' + origin.lat + ',' + origin.lng + '/' + destination;
        window.open(url, '_blank');
    }

    L.Control.geocoder().addTo(map);

    var lc = L.control.locate({
        position: 'topleft',
        strings: {
            title: "Show me where I am"
        },
        locateOptions: {
            enableHighAccuracy: true
        }
    }).addTo(map);

    $(document).ready(function () {
        var userId = '<%= Session["Login"] %>';
        loadMap(userId);

        $('#downloadExcel').click(function () {
            $.ajax({
                url: 'ShowMap.aspx/DownloadAddresses',
                method: 'POST',
                contentType: 'application/json',
                success: function (data) {
                    var base64 = data.d;
                    var link = document.createElement('a');
                    link.href = 'data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,' + base64;
                    link.download = 'Addresses.xlsx';
                    link.click();
                },
                error: function () {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Failed to download addresses from the server!',
                    });
                }
            });
        });
    });
</script>

</asp:Content>
