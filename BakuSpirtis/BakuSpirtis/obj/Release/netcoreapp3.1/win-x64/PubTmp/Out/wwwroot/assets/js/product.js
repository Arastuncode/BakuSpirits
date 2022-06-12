$(document).ready(function () {
  $(".content").slice(0, 1000).show();
  $("#loadMore").on("click", function (e) {
    e.preventDefault();
    $(".content:hidden").slice(0, 1000).slideDown();
    if ($(".content:hidden").length == 0) {
      $("#loadMore").text("Məzmun yoxdur").addClass("noContent");
    }
  });
});

$(document).on("click", ".categories", function (e) {
  e.preventDefault();
  $(this).next().next().slideToggle();
});

$(document).on("click", ".category li a", function (e) {
  e.preventDefault();
  let category = $(this).attr("data-id");
  let products = $(".product-item");

  products.each(function () {
    if (category == $(this).attr("data-id")) {
      $(this).parent().fadeIn();
    } else {
      $(this).parent().hide();
    }
  });
  if (category == "all") {
    products.parent().fadeIn();
  }
});


 