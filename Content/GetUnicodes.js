/*
******************************************************************************************
**************************** GO TO THIS LINK AND RUN THIS JS *****************************
***************** https://en.wikipedia.org/wiki/Playing_cards_in_Unicode *****************
******************************************************************************************
*/


let table = document.querySelector('table.wikitable.unicode');
let rows = table.querySelectorAll('tr');
let cardNames = [];
let unicodes = [];
for (let i = 0; i < rows.length; i += 4) {
	let cols = rows[i].querySelectorAll('th');
	cols.forEach(c => unicodes.push(c.innerText));
}
for (let i = 2; i < rows.length; i += 4) {
	let cols = rows[i].querySelectorAll('td');
	cols.forEach(c => cardNames.push(c.innerText));
}
let output = [];
for (let i = 0; i < unicodes.length; i++) {
	output.push({
		name: cardNames[i],
		unicode: unicodes[i]
	});
}

let downloadFile = () => {
	var fileContent = JSON.stringify(output, 0, 2);
	var bb = new Blob([fileContent], { type: 'text/plain' });
	var a = document.createElement('a');
	a.download = 'download.json';
	a.href = window.URL.createObjectURL(bb);
	a.click();
}

downloadFile();
