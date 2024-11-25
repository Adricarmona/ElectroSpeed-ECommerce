import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { tap } from 'rxjs';
import { inject } from '@angular/core';
import { RedirectionService } from '../../service/redirection.service';

export const redirectionGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot, 
  state: RouterStateSnapshot) => {

    // Inyectamos servicios
    const redirectionService = inject(RedirectionService);
    const router = inject(Router);

    // Opción sin observable
    if (!redirectionService.isLogged) {
      // Navegamos al login indicando que después redireccione a donde queríamos ir en un principio
      router.navigate(['log'], { queryParams: { redirectTo: state.url }});
    }

    return redirectionService.isLogged;
};

