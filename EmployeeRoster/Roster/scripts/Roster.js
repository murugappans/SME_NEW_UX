! function (a) {
    jQuery.sessionTimeout = function (b) {
        function c(b) {
            switch (b) {
                case "start":
                    e = setTimeout(function () {
                        a("#sessionTimeout-dialog").dialog("open"), d("start")
                    }, h.warnAfter);
                    break;
                case "stop":
                    clearTimeout(e)
            }
        }

        function d(a) {
            switch (a) {
                case "start":
                    f = setTimeout(function () {
                        window.location = h.redirUrl
                    }, h.redirAfter - h.warnAfter);
                    break;
                case "stop":
                    clearTimeout(f)
            }
        }
        var e, f, g = {
            message: "Your session is about to expire.",
            keepAliveUrl: "/keep-alive",
            redirUrl: "/timed-out",
            logoutUrl: "/log-out",
            warnAfter: 9e5,
            redirAfter: 12e5
        },
            h = g;
        b && (h = a.extend(g, b)), a("body").append('<div title="Session Timeout" id="sessionTimeout-dialog">' + h.message + "</div>"), a("#sessionTimeout-dialog").dialog({
            autoOpen: !1,
            width: 400,
            modal: !0,
            closeOnEscape: !1,
            open: function () {
                a(".ui-dialog-titlebar-close").hide()
            },
            buttons: {
                "Log Out Now": function () {
                    window.location = h.logoutUrl
                },
                "Stay Connected": function () {
                    a(this).dialog("close"), a.ajax({
                        type: "POST",
                        url: h.keepAliveUrl
                    }), d("stop"), c("start")
                }
            }
        }), c("start")
    }
}(jQuery);

$.sessionTimeout({
    warnAfter: 3000,
    redirAfter: 300000
});