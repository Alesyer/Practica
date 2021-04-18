import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Usuario } from '../interfacesUsuario';

@Component({
  selector: 'app-usuarios-listado',
  templateUrl: './usuarios.listado.html',
})
export class UsuariosListadoComponent {
  public usuarios: Usuario[];

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit() {
    this.http.get<Usuario[]>(this.baseUrl + 'api/Usuarios').subscribe(result => {
      this.usuarios = result;
    }, error => console.error(error));
  }

}




