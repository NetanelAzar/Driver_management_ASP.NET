var stripe = Stripe('pk_test_51PdnnDCGGvtn75QC5OTYc7ykFTdwq9feK1PSFQpe4Gl16Pxi0RHtHO3AP7KZyKEWMVS7lQ7hWJDMqX2CjZTtICKh005aryEm8x'); // Public key
var elements = stripe.elements();
var cardElement = elements.create('card');
cardElement.mount('#card-element');

function payNow() {
    stripe.createToken(cardElement).then(function (result) {
        if (result.error) {
            alert(result.error.message);
        } else {
            var token = result.token.id;
            var numberOfPackages = '<%= Request.QueryString["NumberOfPackages"] %>';
            var totalAmount = '<%= Request.QueryString["TotalAmount"] %>';
            var destinationAddress = '<%= Request.QueryString["DestinationAddress"] %>';
            var orderDate = '<%= Request.QueryString["OrderDate"] %>';
            var destinationCity = '<%= Request.QueryString["DestinationCity"] %>';
            var payment = '<%= Request.QueryString["Payment"] %>';

            var form = document.createElement('form');
            form.method = 'post';
            form.action = 'PayOrder.aspx';

            var hiddenFields = [
                { name: 'stripeToken', value: token },
                { name: 'numberOfPackages', value: numberOfPackages },
                { name: 'totalAmount', value: totalAmount },
                { name: 'destinationAddress', value: destinationAddress },
                { name: 'orderDate', value: orderDate },
                { name: 'destinationCity', value: destinationCity },
                { name: 'Payment', value: payment }
            ];

            hiddenFields.forEach(function (field) {
                var input = document.createElement('input');
                input.type = 'hidden';
                input.name = field.name;
                input.value = field.value;
                form.appendChild(input);
            });

            document.body.appendChild(form);
            form.submit();
        }
    });
}