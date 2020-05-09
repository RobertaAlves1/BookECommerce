class Shopping {
    clickIncremento(button) {
        let data = this.getData(button);
        data.Amount++;
        this.postQuantidade(data);
    }

    clickDecremento(button) {
        let data = this.getData(button);
        data.Amount--;
        this.postQuantidade(data);
    }

    updateAmount(input) {
        let data = this.getData(input);
        this.postQuantidade(data);
    }

    getData(elemento) {
        var linhaDoItem = $(elemento).parents('[item-id]');
        var itemId = $(linhaDoItem).attr('item-id');
        var novaQuantidade = $(linhaDoItem).find('input').val();

        return {
            ID: itemId,
            Amount: novaQuantidade
        };
    }

    postQuantidade(data) {

        let token = $('[name=__RequestVerificationToken]').val();

        let headers = {};
        headers['RequestVerificationToken'] = token;

        $.ajax({
            url: '/request/updateamount',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            headers: headers
        }).done(function (response) {
            let itemPedido = response.requestItem;
            let linhaDoItem = $('[item-id=' + requestItem.ID + ']')
            linhaDoItem.find('input').val(requestItem.Amount);
            linhaDoItem.find('[subtotal]').html((itemPedido.subtotal).duasCasas());
            let carrinhoViewModel = response.shoppingViewModel;
            $('[numero-itens]').html('Total: ' + shoppingViewModel.itens.length + ' itens');
            $('[total]').html((carrinhoViewModel.total).duasCasas());

            if (requestItem.Amount == 0) {
                linhaDoItem.remove();
            }
        });
    }
}

var shopping = new Shopping();

Number.prototype.duasCasas = function () {
    return this.toFixed(2).replace('.', ',');
}

