/*
    会员、教师管理
*/
define(function (require, exports, module) {
    var $ = require("jquery"),
        Verify = require('verify'), VerifyObject,
        Global = require("global"),
        doT = require("dot");
    require("paginate");
    var Params={
        index: 1,
        keywords: "",
        type:0
    }
    var UserLevel = {

        //列表页JS
        init: function (type) {
            Params.type = type;
            this.bindEvent();
            this.getUsersList(1);
        },
        //绑定事件
        bindEvent: function () {
            var _self = this;
            $(".search-ico").click(function () {
                Params.keywords = $("#keywords").val();
                _self.getUsersList(1);
            });
            $("#keywords").keypress(function (event) {
                if (event.keyCode == 13) {
                    Params.keywords = $("#keywords").val();
                    _self.getUsersList(1);
                }
            })
        },
        //获取列表
        getUsersList: function () {
            var _self = this;
            $(".tr-header").nextAll().remove();
            Global.post("/Manage/Customer/GetUsers", Params, function (data) {
                doT.exec("/modules/manage/template/users-list.html?1", function (templateFun) {
                    var innerText = templateFun(data.Items);
                    innerText = $(innerText);
                    $(".tr-header").after(innerText);
                    //绑定事件
                    innerText.find(".delete").click(function () {
                        var uid = $(this).data("id");
                        if (confirm("确认删除吗？")) {
                            Global.post("/Manage/Customer/DeleteUser", {
                                userid: uid
                            }, function (data) {
                                if (data.Status) {
                                    _self.getUsersList(1);
                                }
                            })
                        }
                    });
                });
                $("#pager").paginate({
                    total_count: data.Total,
                    count: data.Pages,
                    start: Params.index,
                    display: 10,
                    border: true,
                    border_color: '#fff',
                    text_color: '#333',
                    background_color: '#fff',
                    border_hover_color: '#ccc',
                    text_hover_color: '#333',
                    background_hover_color: '#ee',
                    rotate: true,
                    images: false,
                    mouse: 'slide',
                    onChange: function (page) {
                        Params.index = page;
                        _self.getUsersList();
                    }
                });
            });
        },

        //新建页JS
        initCreate: function () {
            this.bindCreateEvent();
        },
        //绑定事件
        bindCreateEvent: function () {
            var _self = this;
            VerifyObject = Verify.createVerify({
                element: ".verify",
                emptyAttr: "data-empty",
                verifyReg: "data-type",
                regText: "data-text"
            });

            $("#btnSaveUserLevel").click(function () {
                if (!VerifyObject.isPass())
                    return;
                _self.savaUserLevel();
            })
        },
        //保存
        savaUserLevel: function () {
            var _self = this;
            var UserLevel = {
                ID: _self.ID,
                Level: $("#level").val().trim(),
                Name: $("#name").val().trim(),
                Type: $("#type").val(),
                MinExp: $("#minExp").val(),
                Discount:1,
                ImgPath: $("#imgPath").attr("src"),
                Description: $("#description").val()
            };
            Global.post("/Manage/Customer/SaveUserLevel", { userLevel: JSON.stringify(UserLevel) }, function (data) {
                if (data.ID == "1") {
                    location.href = "/Manage/Customer/UserLevel";
                } else {
                    alert(data.ID);
                }
            });
        },
        //详情页JS
        initDetail: function (id, callBack) {
            this.UserID = id;
            this.bindCreateEvent();
            this.bindUser(id, callBack);
        },
        //绑定页面信息
        bindUser: function (id, callBack) {
            var _self = this;
            Global.post("/Manage/Customer/GetUserByUserID", { userid: id }, function (data) {
                var model = data.Item;

                $("#name").val(model.Name);
                $("#mobile").val(model.MobileTele);
                $("#imgPath").attr("src", model.PhotoPath);
                $("#email").val(model.Email);
                $("#keywords").val(model.KeyWords);
                $("#description").val(model.Description);
                !!callBack && callBack();
            });
        }
    }
    module.exports = UserLevel;
})