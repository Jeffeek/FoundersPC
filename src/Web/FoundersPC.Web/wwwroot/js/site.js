"use strict";
const randomInt = (min, max) =>
{
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
};
const randomString = (length = 6) => (Math.random().toString(36).slice(2) + Math.random().toString(36).slice(2)).substring(0, length);