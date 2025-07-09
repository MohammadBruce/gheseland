function Hash() {
    return {
        getUrlHash: function () {
            var n = location.hash;
            return String(n)
        },
        getUrlParameter: function (n) {
            for (var u = decodeURIComponent(window.location.hash), r = u.split("/"), t, i = 0; i < r.length; i++)
                if (t = r[i].split("="), t[0] === n) return t[1] === undefined ? "" : t[1];
            return ""
        },
        setUrlParameter: function (n) {
            var o = decodeURIComponent(window.location.hash),
                i = o.split("/"),
                f, t, u = 0,
                e = "",
                r;
            for ($.each(n, function (n, r) {
                u = 0;
                var e = r.sParam,
                    o = r.sParamValue;
                for (t = 0; t < i.length; t++) f = i[t].split("="), f[0] === e && (i[t] = e + "=" + o, u++);
                u === 0 && i.push(e + "=" + o)
            }), t = 0; t < i.length; t++) r = i[t].split("="), typeof r[1] != "undefined" && r[1] != "" && (e += "/" + r[0] + "=" + r[1]);
            window.location.hash = e
        },
        addUrlParameter: function (n, t, i, r) {
            for (var c = decodeURIComponent(window.location.hash), f = c.split("/"), s, o = 0, h = "", e, o = 0, u = 0; u < f.length; u++) s = f[u].split("="), s[0] === n && (f[u] = n + "=" + t, o++);
            for (o === 0 && f.push(n + "=" + t), u = 0; u < f.length; u++) e = f[u].split("="), typeof e[1] != "undefined" && e[1] != "" && (h += "/" + e[0] + "=" + e[1], typeof i != "undefined" && typeof r != "undefined" && Hash().addTag(n, t, i, r));
            window.location.hash = h
        }
    }
}

function QueryString() {
    return {
        getUrlQueryStrings: function () {
            var fullUrl = window.location.href;
            return String(fullUrl.substring(fullUrl.indexOf('?') + 1, fullUrl.length));
        },
        getUrlParameter: function (sParam) {
            var sPageUrl = this.getUrlQueryStrings();
            var sUrlVariables = sPageUrl.split('&');
            var sParameterName;
            var i;

            for (i = 0; i < sUrlVariables.length; i++) {
                sParameterName = sUrlVariables[i].split('=');

                if (sParameterName[0] === sParam) {
                    return sParameterName[1] === undefined ? "" : sParameterName[1];
                }
            }
            return "";
        }
    }
}
