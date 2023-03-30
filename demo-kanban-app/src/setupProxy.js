const { createProxyMiddleware } = require("http-proxy-middleware");

module.exports = function (app) {
  app.use(
    "/api",
    createProxyMiddleware({
      target: "https://localhost:7108/",
      secure: false,
      logLevel: "debug",
      changeOrigin: true,
    })
  );
};

/*
{
  "/api/*": {
    "target": "https://localhost:7108/",
    "secure": false,
    "logLevel": "debug",
    "changeOrigin": true
  }
}
*/
