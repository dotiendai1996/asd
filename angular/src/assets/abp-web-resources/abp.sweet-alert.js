var abp = abp || {};
(function () {
    var showMessage = function (type, message, title, opts) {

        if (!title) {
            title = message;
            message = undefined;
        }

        opts = opts || {};
        opts.title = title;
        opts.type = type;
        opts.confirmButtonText = opts.confirmButtonText || abp.localization.abpWeb('Ok');

        if (opts.isHtml) {
            opts.html = message;
        } else {
            opts.text = message;
        }

        return Swal.fire(opts);
    };

    abp.message.info = function (message, title, opts) {
        return showMessage('info', message, title, opts);
    };

    abp.message.success = function (message, title, opts) {
        return showMessage('success', message, title, opts);
    };

    abp.message.warn = function (message, title, opts) {
        return showMessage('warning', message, title, opts);
    };

    abp.message.error = function (message, title, opts) {
        return showMessage('error', message, title, opts);
    };

    abp.message.confirm = function (message, titleOrCallback, callback, opts) {

        var title = undefined;

        if (typeof titleOrCallback === "function") {
            callback = titleOrCallback;
        }
        else if (titleOrCallback) {
            title = titleOrCallback;
        };

        opts = opts || {};
        opts.title = title ? title : abp.localization.abpWeb('AreYouSure');
        opts.type = 'warning';

        // opts.confirmButtonText = opts.confirmButtonText || abp.localization.abpWeb('Yes');
        // opts.cancelButtonText = opts.cancelButtonText || abp.localization.abpWeb('Cancel');
        opts.confirmButtonText = "Có";
        opts.cancelButtonText = "Không";

        opts.showCancelButton = true;

        if (opts.isHtml) {
            opts.html = message;
        } else {
            opts.text = message;
        }

        return Swal.fire(opts).then(function(result) {
            callback && callback(result.value);
        });
    };
})();
