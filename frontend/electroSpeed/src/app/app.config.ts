import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { provideNgxStripe } from 'ngx-stripe';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(routes),
    provideNgxStripe('pk_test_51QJzl7Ahkg3lZ5a851ZMgE7zjPYyUFdwlyqTx63FFvJwzJEk1ELgtrYenTNa3xeElOh0sFicUcN3xMFoHIPDJ35u00EiLtR02y'),
    provideHttpClient()]
};

