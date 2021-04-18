import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';


import { Usuario } from '../interfacesUsuario';

@Component({
  selector: 'app-usuarios-autentificacion',
  templateUrl: './usuarios.autentificacion.html'
})
export class UsuariosAutentificacionComponent {


  // the view title
  title: string;
  // the form model
  form: FormGroup;
  // the object to edit
  usuario: Usuario;

  autentificado: boolean;
  submit: boolean;

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

    this.title = "Login";
    this.autentificado = false;
    this.submit = false;
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
    if (this.form.invalid) {
      return;
    }
    var usuario = this.usuario;
    usuario.email = this.form.get("email").value;
    usuario.password = this.form.get("password").value;
    this.submit = true;

    var url = this.baseUrl + "api/Usuarios/getAuthentication";
    this.http.post<boolean>(url, this.usuario).subscribe(result => {
      this.autentificado = result;
      if (this.autentificado)
        this.router.navigate(['/usuarios-listado']);
      return;
    }, error => console.error(error));
  }
}









