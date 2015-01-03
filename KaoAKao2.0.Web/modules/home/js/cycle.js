function DY_scroll(wraper,prev2,next2,img,speed,or)
 { 
  var wraper = $(wraper);
  var prev2 = $(prev2);
  var next2 = $(next2);
  var img = $(img).find('ul');
  var w = img.find('li').outerWidth(true);
  var s = speed;
  next2.click(function()
       {
        img.animate({'margin-left':-w},function()
                  {
                   img.find('li').eq(0).appendTo(img);
                   img.css({'margin-left':0});
                   });
        });
  prev2.click(function()
       {
        img.find('li:last').prependTo(img);
        img.css({'margin-left':-w});
        img.animate({'margin-left':0});
        });
  if (or == true)
  {
   ad = setInterval(function() { next2.click();},s*1000);
   wraper.hover(function(){clearInterval(ad);},function(){ad = setInterval(function() { next2.click();},s*1000);});
  }
 }
 DY_scroll('.img-scroll','.prev2','.next2','.img-list',3,true);// true为自动播放，不加此参数或false就默认不自动