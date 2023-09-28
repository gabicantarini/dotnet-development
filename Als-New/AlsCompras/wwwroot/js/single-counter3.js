/*Evemt Single Page Counter JS Code*/
const second = 1000,
      minute = second * 60,
      hour = minute * 60,
      day = hour * 24;
let countDown = new Date('Dec 31, 2021 00:00:00').getTime(),
    x = setInterval(function() {
		let now = new Date().getTime(),
			distance = countDown - now;
		document.getElementById("days").innerText = Math.floor(distance / (day)),
		document.getElementById("days2").innerText = Math.floor(distance / (day)),
		document.getElementById("days3").innerText = Math.floor(distance / (day)),
		document.getElementById("hours").innerText = Math.floor((distance % (day)) / (hour)),
		document.getElementById("hours2").innerText = Math.floor((distance % (day)) / (hour)),
		document.getElementById("hours3").innerText = Math.floor((distance % (day)) / (hour)),
		document.getElementById("minutes").innerText = Math.floor((distance % (hour)) / (minute)),
		document.getElementById("minutes2").innerText = Math.floor((distance % (hour)) / (minute)),
		document.getElementById("minutes3").innerText = Math.floor((distance % (hour)) / (minute)),
		document.getElementById("seconds").innerText = Math.floor((distance % (minute)) / second),
		document.getElementById("seconds2").innerText = Math.floor((distance % (minute)) / second),
		document.getElementById("seconds3").innerText = Math.floor((distance % (minute)) / second);
}, second)