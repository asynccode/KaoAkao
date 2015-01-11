onload = function() {
	var e, i = 0;
	while (e = document.getElementById('gallery').getElementsByTagName ('DIV') [i++]) {
		if (e.className == 'now' || e.className == 'old') {
		e.onclick = function () {
			var getEls = document.getElementsByTagName('DIV');
				for (var z=0; z<getEls.length; z++) {
				getEls[z].className=getEls[z].className.replace('reveal', 'conceal');
				getEls[z].className=getEls[z].className.replace('now', 'old');
				}
			this.className = 'now';
			var max = this.getAttribute('title');
			document.getElementById(max).className = "reveal";
			}
		}
	}
}