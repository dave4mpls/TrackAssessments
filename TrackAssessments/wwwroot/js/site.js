// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function el(x) { return document.getElementById(x); }

function attachBarcodeReader() {
    // Seek the barcode fields and attach the quaggaJS
    // barcode reader to them.

}

function bodyOnLoad() {
    // The client side structure is that on page load,
    // we look for certain fields, and attach our fancy
    // add-on components to them.
    attachBarcodeReader();
}