
alert("Привет, мир!");
console.log("Привет, мир!");

let a = prompt("Введите длину стороны a прямоугольника:", "5");
let b = prompt("Введите длину стороны b прямоугольника:", "10");
let sideA = Number(a);
let sideB = Number(b);
let S = sideA * sideB;
console.log("Площадь прямоугольника S = a * b =", S);

let ageInput = prompt("Введите ваш возраст:", "18");
let age = Number(ageInput);

if (age < 18) {
  alert("Доступ запрещён. Контент доступен только пользователям 18+.");
} else {
  alert("Доступ разрешён. Добро пожаловать на сайт!");
}

console.log("Таблица умножения числа 5:");
for (let i = 1; i <= 10; i++) {
  console.log("5 x " + i + " = " + 5 * i);
}

