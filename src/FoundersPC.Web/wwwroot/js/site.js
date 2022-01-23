"use strict";
const randomInt = (min, max) =>
{
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
};
const randomString = (length = 6) =>
{
    let result = "";
    while (result.length < length)
    {
        result += Math.random().toString(36).slice(2);
    }
    return result;
};