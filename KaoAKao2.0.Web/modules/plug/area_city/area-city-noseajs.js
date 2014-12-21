
/*
    --省市县插件--

    --引用
    var areaCity = require("area-city");

    --实例化
    var brandArea = areaCity.createArea({
        elementID: ""  //父块状元素ID
    });
    
    brandArea.getAreaCode();   获取选择地区编码
    brandArea.getAreaName();   获取选择地区名称 返回{ Province:'' ,City":'', County:''}
    brandArea.setValue(areaCode); 设置选中地区
*/


    var Option = "<option value=''>请选择</option>";
    var Default = {
        elementID: "areaCity",
        dataUrl: "/Base/GetChildAreaByCode"
    };
    var AreaCity = function (options,callback) {
        this.setting = [];
        this.init(options, callback);
    };
    //初始化
    AreaCity.prototype.init = function (options, callback) {
        var _self = this, _element;
        _self.setting = $.extend([], Default, options);

        _element = $("#" + _self.setting.elementID)

        _self.province = $("<select></select>", { id: _self.setting.elementID + "_province", "class": "areacity-select" }).append(Option);
        _self.city = $("<select></select>", { id: _self.setting.elementID + "_city", "class": "areacity-select" }).append(Option);
        _self.county = $("<select></select>", { id: _self.setting.elementID + "_county", "class": "areacity-select" }).append(Option);
        _element.append(_self.province).append(_self.city).append(_self.county);

        _self.getChildren(_self.province, "0086", callback);

        _self.bindEvent();
    }
    //获取下级区域列表
    AreaCity.prototype.getChildren = function (element, areaCode, callback) {
        var _self = this;
        Global.post(_self.setting.dataUrl, { areaCode: areaCode }, function (data) {
            var _length = data.items.length;
            for (var i = 0; i < _length; i++) {
                element.append("<option value=" + data.items[i].AreaCode + ">" + data.items[i].AreaName + "</option>");
            }
            if (!!callback && typeof callback === "function")
                callback();
        });
    }
    //绑定事件
    AreaCity.prototype.bindEvent = function () {
        var _self = this;
        _self.province.on("change", function () {
            _self.city.empty();
            _self.city.append(Option);
            _self.county.empty();
            _self.county.append(Option);
            if ($(this).val() != "") {
                _self.getChildren(_self.city, $(this).val());
            }
        });
        _self.city.on("change", function () {
            _self.county.empty();
            _self.county.append(Option);
            if ($(this).val() != "") {
                _self.getChildren(_self.county, $(this).val());
            }
        });
    }
    //获取选择地区编码
    AreaCity.prototype.getAreaCode = function () {
        var _self = this;
        if (_self.county.val() != "") {
            return _self.county.val();
        } else if (_self.city.val() != "") {
            return _self.city.val();
        } else if (_self.province.val() != "") {
            return _self.province.val();
        } else {
            return "";
        }
    }
    //获取地区名称
    AreaCity.prototype.getAreaName = function () {
        var _self = this,
            _province = _self.province.find("option:selected").text(),
            _city = _self.city.find("option:selected").text(),
            _county = _self.county.find("option:selected").text();
        return {
            Province: _province == "请选择" ? "" : _province,
            City: _city == "请选择" ? "" : _city,
            County: _county == "请选择" ? "" : _county
        }
    }
    //获取地区名称
    AreaCity.prototype.setValue = function (areaCode) {
        if (areaCode.length != 6) {
            return;
        }
        var _self = this, _province = areaCode.substr(0, 2), _city = areaCode.substr(2, 2), _county = areaCode.substr(4, 2);

        var province = _self.province.find("option[value^='" + _province + "']");
        province.prop("selected", "selected");
        if (_city == "00") {
            _self.province.change();
            return;
        }
        _self.getChildren(_self.city, province.val(), function () {
            var city = _self.city.find("option[value*='" + _city + "']");
            city.prop("selected", "selected");
            if (_county == "00") {
                _self.city.change();
                return;
            }
            _self.getChildren(_self.county, city.val(), function () {
                _self.county.find("option[value='" + areaCode + "']").prop("selected", "selected");
            });
        });
    }
