
//封装StringBuilder
function StringBuilder() { this._string_ = new Array(); }
StringBuilder.prototype.Append = function (str) { this._string_.push(str); }
StringBuilder.prototype.toString = function () { return this._string_.join(""); }
//封装String的trim(),isNullOrEmpty()
String.prototype.Trim = function (str) { if (!str) str = '\\s'; else { if (str == '\\') str = '\\\\'; else if (str == ',' || str == '|' || str == ';') str = '\\' + str; else str = '\\s'; } eval('var reg=/(^' + str + '+)|(' + str + '+$)/g;'); return this.replace(reg, ''); };
String.prototype.trim = function (str) { return this.Trim(str); };
String.prototype.isNull = function () { return this == null || this.trim().length == 0; }
String.prototype.countLength = function () { var strLength = 0; for (var i = 0; i < this.length; i++) { if (this.charAt(i) > '~') strLength += 2; else strLength += 1; } return strLength; }
String.prototype.cutString = function (cutLength) { if (!cutLength) cutLength = this.countLength(); var strLength = 0; var cutStr = ""; if (cutLength > this.countLength()) cutStr = this; else { for (var i = 0; i < this.length; i++) { if (this.charAt(i) > '~') strLength += 2; else strLength += 1; if (strLength >= cutLength) { cutStr = this.substring(0, i + 1); break; } } } return cutStr; }
String.prototype.ulength = function () { var c, b = 0, l = this.length; while (l) { c = this.charCodeAt(--l); b += (c < 128) ? 1 : 2 }; return b; };
//字符串截取，后面加入...
String.prototype.interceptString = function (len) {
    if (this.length > len) {
        return this.substring(0, len - 1) + "...";
    }
    else {
        return this;
    }
}

var LoadDiv = function () { return "<div id=\"LoadDiv\" class=\"TxtCenter\"><img align=\"absmiddle\" src=\" /Content/images/ajax-loader.gif\"/> 数据加载中，请稍候...</div>"; };
var NullDiv = function () { return "<div class=\"TxtCenter mTop10\">暂无数据</div>"; };


//为异步请求提供公用方法
//requestUrl 请求连接，如：GetAjaxValue.aspx
//requestType 请求类型，如：GET、POST、JSON
//requestData 请求传递数据，如：name=mytest&psd=meihua
//callbackFunction 返回处理函数，如：function SelectedItem(data){}
//loadingElementId 请求时呈现图片元素ID，如：#city 可以不填充
function AjaxRequest(requestUrl, requestType, requestData, callbackFunction, loadingElementId) {
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

////获取URL里的参数，返回一个参数数组
////调用方法如下
//var Request = GetRequest();
//var 参数1,参数2,参数N;
//参数1 = Request['参数1'];
//参数2 = Request['参数2'];
//参数N = Request['参数N'];  
function GetRequest() {
    var url = location.href;  //获取url中"?"符后的字串  
    var theRequest = new Object();
    if (url.indexOf("?") != -1) {
        var str = url.substring(url.indexOf("?") + 1);
        str = str.replace(/#/g, "");
        if (url.indexOf("&") == -1) {
            theRequest[str.substring(0, str.indexOf("="))] = str.substring(str.indexOf("=") + 1);
        }
        else {
            var strs = str.split("&");
            for (var i = 0; i < strs.length; i++) {
                theRequest[strs[i].substring(0, strs[i].indexOf("="))] = strs[i].substring(strs[i].indexOf("=") + 1);
            }
        }

    } else if (url.indexOf("&") != -1) {
        var str = url.substring(url.indexOf("&") + 1);
        str = str.replace(/#/g, "");
        if (url.indexOf("&") == -1) {
            theRequest[str.substring(0, str.indexOf("="))] = str.substring(str.indexOf("=") + 1);
        }
        else {
            var strs = str.split("&");
            for (var i = 0; i < strs.length; i++) {
                theRequest[strs[i].substring(0, strs[i].indexOf("="))] = strs[i].substring(strs[i].indexOf("=") + 1);
            }
        }
    }
    return theRequest;
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

function IsURL(str_url) {
    var strRegex = '^((https|http|ftp|rtsp|mms)?://)'
    //+ '?(([0-9a-z_!~*\'().&=+$%-]+: )?[0-9a-z_!~*\'().&=+$%-]+@)?' //ftp的user@ 
    //+ '(([0-9]{1,3}.){3}[0-9]{1,3}' // IP形式的URL- 199.194.52.184 
    //+ '|' // 允许IP和DOMAIN（域名） 
    //+ '([0-9a-z_!~*\'()-]+.)*' // 域名- www. 
    //+ '([0-9a-z][0-9a-z-]{0,61})?[0-9a-z].' // 二级域名 
    //+ '[a-z]{2,6})' // first level domain- .com or .museum 
    //+ '(:[0-9]{1,4})?' // 端口- :80 
    //+ '((/?)|' // a slash isn't required if there is no file name 
    //+ '(/[0-9a-z_!~*\'().;?:@&=+$,%#-]+)+/?)$';
    var re = new RegExp(strRegex);
    if (re.test(str_url)) {
        return true;
    } else {
        return false;
    }
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

//是否为移动端
function isMoblieTerminal() {
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

//验证用户是否登录
function validateLogin()
{
    AjaxRequest("/Ajax/Validate", "get", {},
        function (data) {
            console.log(data);
            if (data.result = "1") {
                $(".clearfix li.button-min").hide();
                var html = "<li>" + data.userName + "    |    " + "登出" + "</li>";

            }
        }
    );

}