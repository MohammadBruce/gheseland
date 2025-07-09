var datas = {
    loadSync: function (url, method, data, dataType, callback, beforesend, complete, contentType, processData) {
        $.ajax({
            url: url,
            type: method,
            data: data,
            dataType: dataType,
            contentType: contentType,
            async: false,
            processData: processData,
            beforeSend: beforesend,
            complete: complete,
            success: function (response) {
                callback(response);
            }
        });
    },
    loadAsync: function (url, method, data, dataType, callback, beforesend, complete, contentType, processData) {
        $.ajax({
            url: url,
            type: method,
            data: data,
            dataType: dataType,
            contentType: contentType,
            processData: processData,
            beforeSend: beforesend,
            complete: complete,
            timeout: 3000000,
            success: function (response) {
                callback(JSON.parse(response) );
            }
        });
    },
    loadAsyncPromise: function (url, method, data, beforesend, complete) {
        var result = $.deferred();
        $.ajax({
            url: url,
            type: method,
            data: data,
            dataType: "json",
            beforeSend: beforesend,
            complete: complete,
            success: function (response) {
                result.resolve(response);
            },
            error: function () {
                result.reject();
            }
        });
        return result;
    },
    call: function (params) {

        if (typeof (params.method) == "undefined") {
            params.method = methods.GET;
        }
        if (typeof (params.dataType) == "undefined") {
            params.dataType = "json"
        }
        if (typeof (params.failure) == "undefined") {
            params.failure = function () {
                console.log('failure');
            }
        }
        if (typeof params.async == "undefined") {
            params.async = true;
        }
        //if (typeof (params.haveGlobalLoader) == "undefined") {
        //    params.haveGlobalLoader = false
        //}

        //if (params.haveGlobalLoader) {
        //    params.beforeSend = function () {
        //        loader().show();
        //    }

        //    params.complete = function () {
        //        loader().hide();
        //    }

        //}
        



        if (params.async) {
            this.loadAsync(params.url, params.method, params.data, params.dataType, params.callback, params.beforeSend, params.complete, params.contenttype, params.processData, params.failure, params.headers)

        } else {
            this.loadSync(params.url, params.method, params.data, params.dataType, params.callback, params.beforeSend, params.complete, params.contenttype, params.processData, params.failure, params.headers)

        }
    }
}
var methods = {
    GET: "GET",
    POST: "POST"
};

var dataTypes = {
    JSON: "json",
    APPLICATION_JSON: "application/json"
};
