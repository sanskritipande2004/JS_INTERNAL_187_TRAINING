function fun(n)
{
    return n*2;
}
let ch=fun(72);
console.log(ch);

let arr=["banana","apple","mango","grapes","guava"];

for(let i of arr)
{
    console.log(i);
}
arr.push("papaya");
console.log(arr);
arr.unshift("kiwi");
console.log(arr);
let idx=arr.indexOf("banana");
console.log(idx);
var b=arr.includes("mango");
console.log(b);
let nums=[2,4,6,8];
let doubled=nums.map(n=>2*n);
let res=nums.filter(n=>n>20);
let a=[1,2,3,4,5];
let sum=a.reduce((acc,curr)=>{return acc+curr},0);
console.log(doubled);
console.log(res);
console.log(sum);
