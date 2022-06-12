$(document).ready(function () {
    $(document).on("click", ".set-default", function () {
        let ProductId = parseInt($(".product-id").val())
        let ImageId = parseInt($(this).attr("data-id"))
        $.ajax({
            url: "/AdminArea/Product/SetDefaultImage",
            data: {
                productId: ProductId,
                imageId: ImageId
            },
            type: "Post",
            success: function (res) {
                Swal.fire({
                    position: "bottom-end",
                    icon: "success",
                    title: "Image successfully changed",
                    showConfirmButton: false,
                    timer: 1500
                }).then(function () {
                    window.location.reload();
                })
            }
        })
    })
})