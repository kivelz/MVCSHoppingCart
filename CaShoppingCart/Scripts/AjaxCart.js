    $(function () {

        /*
        * Increment product
        */

        $("a.incrproduct").click(function (e) {
            e.preventDefault();

            var productId = $(this).data("id");
            var url = "/cart/IncrementProduct";

            $.getJSON(url, { productId: productId }, function (data) {

                $(".qty" + productId).html(data.qty);

                var price = data.qty * data.price;
                var priceHtml = "$" + price.toFixed(2);

                $("td.total" + productId).html(priceHtml);

                var gt = parseFloat($("td.grandtotal span").text());
                var grandtotal = (gt + data.price).toFixed(2);

                $("td.grandtotal span").text(grandtotal);
            }).done(function (data) {
            });
        });

    // Decrement product


    $("a.decrproduct").click(function (e) {
        e.preventDefault();

    var $this = $(this);
    var productId = $(this).data("id");
    var url = "/cart/DecrementProduct";

                $.getJSON(url, {productId: productId }, function (data) {
                    if (data.qty == 0) {
        $this.parent().parent().fadeOut("fast", function () {
            location.reload();
        });
    }
                    else {
        $(".qty" + productId).html(data.qty);


    var price = data.qty * data.price;
    var priceHtml = "$" + price.toFixed(2);

    $("td.total" + productId).html(priceHtml);

    var gt = parseFloat($("td.grandtotal span").text());
    var grandtotal = (gt - data.price).toFixed(2);

    $("td.grandtotal span").text(grandtotal);
}

                }).done(function (data) {

    });
});

// Remove product


            $("a.removeproduct").click(function (e) {
        e.preventDefault();

    var $this = $(this);
    var productId = $(this).data("id");
    var url = "/cart/RemoveProduct";

                $.get(url, {productId: productId }, function (data) {
        location.reload();
    });
 });

//CheckOut

 $("a.placeorder").click(function (e) {
 e.preventDefault();
    var $this = $(this);
    var url = "/cart/CheckOut";
    $(".ajaxbg").show();
     $.post(url, {}, function (data) {
                      alert('Thank you for your purchase');
                    window.location = "/product/index";
                });
});

});

