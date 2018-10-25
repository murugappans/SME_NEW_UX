
function AnotherFunction(msg,type)
    {
       toastr.options = {
  "closeButton": true,
   "debug": false,
  "positionClass": "toast-top-full-width",
  "onclick": null,
  "showDuration": "300",
  "hideDuration": "500",
  "timeOut": "5000",
  "extendedTimeOut": "1000",
  "showEasing": "swing",
  "hideEasing": "linear",
  "showMethod": "fadeIn",
  "hideMethod": "fadeOut"
}
//alert(msg+type);

switch(type)
{
case "I":
  toastr.info('<i>'+msg+'</i>','<b><i>info</i></b>');
  break;
case "W":
 toastr.warning('<i>'+msg+'</i>','<b><i>warning</i></b>');
  break;
  case "S":
  toastr.success('<i>'+msg+'</i>','<b><i>Success</i></b>');
  break;
  case "E":
  toastr.error('<i>'+msg+'</i>','<b><i>Error</i></b>');
  break;
default:
  toastr.info('<i>'+msg+'</i>','<b><i>info</i></b>');
}

    };