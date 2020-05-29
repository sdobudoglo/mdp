function AddressViewModel(app, dataModel) {
    var self = this;

    self.myAddress = ko.observable("");

    Sammy(function () {
        this.get('#address', function () {
            // Make a call to the protected Web API by passing in a Bearer Authorization Header
            $.ajax({
                method: 'get',
                url: app.dataModel.userInfoUrl,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                success: function (data) {
                    self.myAddress('Ваш адрес: ' + data.address);
                }
            });
        });
        this.get('/', function () { this.app.runRoute('get', '#address'); });
    });

    return self;
}

app.addViewModel({
    name: "Address",
    bindingMemberName: "address",
    factory: AddressViewModel
});
