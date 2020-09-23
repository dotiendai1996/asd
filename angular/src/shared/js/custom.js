// $('body').delegate('input[type="number"]:not(.allow-negative)', 'paste', function(evt) {

//     clipboardData = evt.originalEvent.clipboardData || window.clipboardData;
//     var pastedData = clipboardData.getData('Text');

//     if (!pastedData.match(/^[0-9]*$/)) {
//         evt.preventDefault();
//     }
// });
// $('body').delegate('input[type="number"]:not(.allow-negative)', 'keypress', function(evt) {
//     if (evt.which != 8 && evt.which != 0 && evt.which < 48 || evt.which > 57) {
//         evt.preventDefault();
//     }
// });
// $('body').delegate('input[type="text"]', 'keypress', function(evt) {
//     var text = $(this).val();
//     //console.log(text.match(/[^ ](.*|\n|\r|\r\n)*/));
//     if (evt.keyCode == 32 && evt.target.selectionStart === 0) {
//         evt.preventDefault();
//     }
// });
// $('body').delegate('input[type="email"]', 'keydown', function(evt) {
//     var text = $(this).val();
//     //console.log(text.match(/[^ ](.*|\n|\r|\r\n)*/));
//     if (evt.keyCode == 32 && text == '') {
//         evt.preventDefault();
//     }
// });


