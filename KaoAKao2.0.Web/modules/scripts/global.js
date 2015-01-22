define(function (require, exports, module) {
    var Global = {},
        jQuery = require("jquery");

    Global.post = function (url, params, callback, anync) {
        jQuery.ajax({
            type: "POST",
            url: url,
            data: params,
            dataType: "json",
            async: !anync,
            cache: false,
            success: function (data) {
                if (data.error) {
                    return;
                } else {
                    !!callback && callback(data);
                }
            }
        });
    }

    //格式化日期
    Date.prototype.toString = function (format) {
        var o = {
            "M+": this.getMonth() + 1,
            "d+": this.getDate(),
            "h+": this.getHours(),
            "m+": this.getMinutes(),
            "s+": this.getSeconds(),
            "q+": Math.floor((this.getMonth() + 3) / 3),
            "S": this.getMilliseconds()
        }

        if (/(y+)/.test(format)) {
            format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        }

        for (var k in o) {
            if (new RegExp("(" + k + ")").test(format)) {
                format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
            }
        }
        return format;
    };

    //日期字符串转换日期格式
    String.prototype.toDate = function (format) {
        var d = new Date();
        d.setTime(this.match(/\d+/)[0]);
        return (!!format) ? d.toString(format) : d;
    }

    //将json日期字符串转换日期格式
    String.prototype.createDateFormat = function () {
        var jsondate = this;
        if (jsondate == "刚刚")
            return jsondate;
        jsondate = jsondate.replace("/Date(", "").replace(")/", "");
        if (jsondate.indexOf("+") > 0) {
            jsondate = jsondate.substring(0, jsondate.indexOf("+"));
        }
        else if (jsondate.indexOf("-") > 0) {
            jsondate = jsondate.substring(0, jsondate.indexOf("-"));
        }

        var date = new Date(parseInt(jsondate, 10));
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();

        return date.getFullYear()
            + "-"
            + month
            + "-"
            + currentDate
            + "-"
            + " "
            + date.getHours()
            + ":"
            + date.getMinutes();
    }

    //截取字符串
    String.prototype.subString = function (len) {
        if (this.length > len) {
            return this.substr(0, len-1) + "...";
        }
        return this;
    }

    //为异步请求提供公用方法
    //requestUrl 请求连接，如：GetAjaxValue.aspx
    //requestType 请求类型，如：GET、POST、JSON
    //requestData 请求传递数据，如：name=mytest&psd=meihua
    //callbackFunction 返回处理函数，如：function SelectedItem(data){}
    //loadingElementId 请求时呈现图片元素ID，如：#city 可以不填充
    Global.AjaxRequest=function (requestUrl, requestType, requestData, callbackFunction, loadingElementId) {
        if (loadingElementId != null && loadingElementId != '')
            $(loadingElementId).html("<div class=\"TxtCenter\"><img src=\"images/ajax-loader.gif\"></div>");
        if (requestType != null && requestType != '' && requestType.toUpperCase() != 'JSON') {
            $.ajax({
                url: requestUrl,
                type: requestType,
                data: requestData,
                cache: false,
                success: function (data) {
                    if (loadingElementId != null && loadingElementId != '') $(loadingElementId).html('');
                    callbackFunction(data);
                }
            });
        }
        else {
            $.ajax({
                url: requestUrl,
                data: requestData,
                cache: false,
                success: function (data) {
                    if (loadingElementId != null && loadingElementId != '') $(loadingElementId).html('');
                    callbackFunction(data);
                }
            });
        }
    }

    //验证一个字符串是否包含特殊字符
    RegExp.isContainSpecial = function (str) {
        var containSpecial = RegExp(/[(\,)(\\)(\/)(\:)(\*)(\')(\?)(\\\)(\<)(\>)(\|)]+/);
        return (containSpecial.test(str));
    }
    //验证一个字符串时候是email
    RegExp.isEmail = function (str) {
        var emailReg = /^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)*\.[\w-]+$/i;
        return emailReg.test(str);
    }
    //验证一个字符串是否是
    RegExp.isUrl = function (str) {
        var patrn = /^http(s)?:\/\/[A-Za-z0-9\-]+\.[A-Za-z0-9\-]+[\/=\?%\-&_~`@[\]\:+!]*([^<>])*$/;
        return patrn.exec(str);
    }

    //验证一个字符串是否是电话或传真
    RegExp.isTel = function (str) {
        var pattern = /^[+]?((\d){3,4}([ ]|[-]))?((\d){7,8})(([ ]|[-])(\d){1,12})?$/;
        return pattern.exec(str);
    }
    //验证一个字符串是否是手机号码
    RegExp.isMobile = function (str) {
        var patrn = /^(1[3-8]{1})\d{9}$/;
        return patrn.exec(str);

    }
    //验证一个字符串是否是传真号
    RegExp.isFax = function (str) {
        var patrn = /^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$/;
        return patrn.exec(str);

    }
    //验证一个字符串是否为外国手机号码
    RegExp.isElseMobile = function (str) {
        var patrn = /^\d{5}\d*$/;
        return patrn.exec(str);
    }
    //验证一个字符串是否是汉字
    RegExp.isZHCN = function (str) {
        var p = /^[\u4e00-\u9fa5\w]+$/;
        return p.exec(str);
    }
    //验证一个字符串是否是数字
    RegExp.isNum = function (str) {
        var p = /^\d+$/;
        return p.exec(str);
    }
    //验证一个字符串是否是纯英文
    RegExp.isEnglish = function (str) {
        var p = /^[a-zA-Z., ]+$/;
        return p.exec(str);
    }

    //Cookies 操作
    //写入
    Global.setCookie= function (name, value) {
        var nextyear = new Date();
        nextyear.setFullYear(nextyear.getFullYear() + 10);
        var expireDate = nextyear.toGMTString();
        if (document.domain.indexOf('.mingdao.com') == -1) {
            document.cookie = name + "=" + escape(value) + ";expires=" + expireDate + ";path=/";
        } else {
            document.cookie = name + "=" + escape(value) + ";expires=" + expireDate + ";path=/;domain=.mingdao.com";
        }
    }
    //读取
    Global.getCookie = function getCookie(name) {
        var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
        if (arr != null) {
            return unescape(arr[2]);
        }
        return null;
    }
    //删除
    Global.delCookie = function (name) {
        var exp = new Date();
        exp.setTime(exp.getTime() - 10000);
        if (getCookie(name) == null) {
            return;
        }
        var cval = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"))[2];
        if (cval != null) {
            if (document.domain.indexOf('.mingdao.com') == -1) {
                document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString() + ";path=/";
            } else {
                document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString() + ";path=/;domain=.mingdao.com";
            }

        }
    }

    //是否为移动端
    Global.isMoblieTerminal=function () {
        var browser = {
            versions: function () {
                var u = navigator.userAgent, app = navigator.appVersion;
                return {
                    //移动终端浏览器版本信息  
                    trident: u.indexOf('Trident') > -1, //IE内核 
                    presto: u.indexOf('Presto') > -1, //opera内核 
                    webKit: u.indexOf('AppleWebKit') > -1, //苹果、谷歌内核 
                    gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1, //火狐内核 
                    mobile: u.match(/AppleWebKit.*Mobile.*/), //是否为移动终端 
                    ios: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), //ios终端 
                    android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android终端或者uc浏览器 
                    iPhone: u.indexOf('iPhone') > -1, //是否为iPhone或者QQHD浏览器 
                    iPad: u.indexOf('iPad') > -1, //是否iPad 
                    webApp: u.indexOf('Safari') == -1 //是否web应该程序，没有头部与底部 
                };
            }(),
            language: (navigator.browserLanguage || navigator.language).toLowerCase()
        }

        if (browser.versions.iPhone || browser.versions.android || browser.versions.mobile)
            return true;
        else
            return false;
    }

    //显示文本框验证内容
    Global.showIptMsg = function (obj, msg) {
        if ($(obj).parent().find(".hint").html())
        {
            $(obj).parent().find(".hint").show();
            $(obj).parent().find(".hint .h-t-nr").html(msg);
        }
        else
        {
            var html = '<div class="hint">';
            html += ' <div class="hint-tool">';
            html += '         <div class="h-t-nr">'+msg+'</div>';
            html += '         <span></span>';
            html += '       </div>';
            html += '     </div>';
            $(obj).parent().append(html);
            $(obj).parent().find(".hint").show();
        }

    }

    module.exports = Global;
});