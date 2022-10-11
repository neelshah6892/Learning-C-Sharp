const prerender = require('aspnet-prerendering');
module.exports = prerender.createServerRenderer(
    function (params) {
        var result = "Prerendering Result" + params.data.x * params.data.y;

        return new Promise(function (resolve, reject) {
            resolve({html: result });
        })
    }
)