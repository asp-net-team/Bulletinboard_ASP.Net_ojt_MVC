/**
 * Show preview image
 * @param {file} input
 */
function ShowpImagePreview(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#previewImage').attr('src', e.target.result);
            $("#file-fake-name").text(input.files[0].name);
            $("#previewImage").css("display", "block");
        }
        reader.readAsDataURL(input.files[0]);
    }
}
