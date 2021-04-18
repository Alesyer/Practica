import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';

import { Usuario } from '../interfacesUsuario';

@Component({
  selector: 'app-usuarios-alta',
  templateUrl: './usuarios.alta.html',
  styleUrls: ['./usuarios.alta.css']
})
export class UsuariosAltaComponent {


  // the view title
  title: string;
  // the form model
  form: FormGroup;
  // the object to edit
  usuario: Usuario;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) {
  }
  ngOnInit() {
    this.form = new FormGroup({
      nombre: new FormControl(''),
      edad: new FormControl(''),
      email: new FormControl(''),
      password: new FormControl('')
    });

    this.title = "Alta de usuarios";
    this.initUsuario();
  }
  initUsuario() {
    this.usuario = {
      id: 0,
      nombre: '',
      edad: 0,
      email: '',
      password: ''
    }
  }
  onSubmit() {
    var usuario = this.usuario;
    usuario.nombre = this.form.get("nombre").value;
    usuario.edad = Number(this.form.get("edad").value);
    usuario.email = this.form.get("email").value;
    usuario.password = this.form.get("password").value;

    var url = this.baseUrl + "api/Usuarios";
    this.http
      .post<void>(url, usuario)
      .subscribe(result => {
        this.initUsuario(); this.form.reset();
      }, error => console.log(error));
  }
}








