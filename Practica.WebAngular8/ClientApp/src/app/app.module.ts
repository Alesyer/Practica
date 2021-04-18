import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';


import { UsuariosAltaComponent } from './usuarios-alta/usuarios.alta';
import { UsuariosAutentificacionComponent } from './usuarios-autentificacion/usuarios.autentificacion';
import { UsuariosListadoComponent } from './usuarios-listado/usuarios.listado';




@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    UsuariosAltaComponent,
    UsuariosAutentificacionComponent,
    UsuariosListadoComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: UsuariosAltaComponent, pathMatch: 'full' },
      { path: 'usuarios-autentificacion', component: UsuariosAutentificacionComponent },
      { path: 'usuarios-listado', component: UsuariosListadoComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
