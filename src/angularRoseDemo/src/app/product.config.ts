import { ProductConfig } from './models/product';
import { EnvServiceFactory } from './env.service.provider';

export const ProductConfigFactory = () => {

  const environment = EnvServiceFactory();
  const config: ProductConfig = new ProductConfig();

  config.issuer = environment.issuer;
  config.loginurl = environment.loginurl;
  config.logouturl = environment.logouturl;
  config.clientId = environment.clientId;
  config.scope = environment.scope;
  config.redirectUri = environment.redirectUri;
  config.appUri = environment.appUri;

  return config;
};

export const ProductConfigProvider = {
  provide: ProductConfig,
  useFactory: ProductConfigFactory,
  deps: [],
};

export const productConfig: ProductConfig = ProductConfigFactory();
