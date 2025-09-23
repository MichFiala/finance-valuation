module.exports = function override(config, env) {
  return config;
};

module.exports.devServer = function overrideDevServer(configFunction) {
  return function (proxy, allowedHost) {
    const config = configFunction(proxy, allowedHost);
    if (config.https) {
      config.server = { type: 'https' };  // use new server option
      delete config.https;
    }
    return config;
  };
};
