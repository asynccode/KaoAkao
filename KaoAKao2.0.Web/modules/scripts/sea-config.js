
/*基础配置*/
seajs.config({
    base: "/modules/",
    alias: {
        "jquery": "scripts/jquery-1.11.1.js",
        "jquery172": "scripts/jquery-1.7.2.js",
        "jqueryui": "scripts/jquery-ui-1.8.20.js",
        "global": "scripts/global.js"        
    }
});

seajs.config({
    alias: {
        //弹出层插件
        "easydialog": "plug/easydialog/easydialog.js",
        //验证插件
        "verify": "plug/verify.js",
        //分页插件
        "paginate": "plug/datapager/paginate.js",
        //doT
        "dot": "plug/doT.js"
    }
});

seajs.config({
    alias: {
        "easydialog-css": "plug/easydialog/easydialog.css"
    }
});

