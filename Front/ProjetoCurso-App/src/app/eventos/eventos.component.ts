import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {

  public eventos: any = [];
  public eventosFiltrados: any = [];

  larguraImg:number = 150;
  margemImg:number = 2;
  mostrarImg:boolean = true;


  private _filtroLista:string = "";
  public get filtroLista() : string {
    return this._filtroLista;
  }
  public set filtroLista(v : string) {
    this._filtroLista = v;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  filtrarEventos(filtrarPor:string): any{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: { tema: string; local: string;}) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }


  //Para utilizar conexão com o banco de dados vamos fazer uma referencia ao HTTP Client injetado pelo construtor
  //Para isso temos que lembrar de infromar no app-modules a importação do HTTP Client.

  constructor(private http:HttpClient) {}

  ngOnInit() {
    this.getEventos();
  }

  verImagem():void {
    this.mostrarImg = !this.mostrarImg;
  }
  public getEventos(): void {

    //Forma automática de buscar os dados do Banco de Dados por meio da API
    this.http.get('https://localhost:5001/api/eventos').subscribe({ //esse subscrive se comporta como um CallBack retornando a resposta do consumo da api
      //Utilizamos 2 propriedades principais
      next: (v) => {
        this.eventos = v;
        this.eventosFiltrados = this.eventos;
      },
      error: (e) => console.error(e),
      complete: () => console.info('complete')
      //response => this.eventos = response,
      //error => console.log(error),
    });

  }
}
