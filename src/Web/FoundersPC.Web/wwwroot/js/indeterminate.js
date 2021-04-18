"use strict";
$(document).ready(function ()
{
    indeterminate();

    function indeterminate()
    {
        const checkBoxes = $('input[indeterminate="True"][type="checkbox"]');
        for (let i = 0; i < checkBoxes.length; i++)
        {
            $(checkBoxes[i]).prop("indeterminate", true);
        }
    }
});