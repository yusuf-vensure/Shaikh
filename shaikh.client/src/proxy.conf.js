const PROXY_CONFIG = [
  {
    context: [
      "/api"
    ],
    target: "http://127.0.0.1:5218",
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
];

module.exports = PROXY_CONFIG;
