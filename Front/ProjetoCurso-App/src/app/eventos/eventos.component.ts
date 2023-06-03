import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {

  public eventos: any;

  //Para utilizar conexão com o banco de dados vamos fazer uma referencia ao HTTP Client injetado pelo construtor
  //Para isso temos que lembrar de infromar no app-modules a importação do HTTP Client.

  constructor(private http:HttpClient) {}

  ngOnInit() {
    this.getEventos();
  }

  public getEventos(): void {

    //Forma Manual de mandar os dados:
    /*
    this.eventos = [
      {
        Tema: 'Angular',
        Local: 'Campo Grande',
      },
      {
        Tema: 'Angular e Suas Novidades',
        Local: 'São Paulo',
      },
      {
        Tema: 'SQL Server ao vivo',
        Local: 'Curitiba',
      },
    ];
    */

    //Forma automática de buscar os dados do Banco de Dados por meio da API
    this.http.get('https://localhost:5001/api/eventos').subscribe( //esse subscrive se comporta como um CallBack retornando a resposta do consumo da api
      //Utilizamos 2 propriedades principais
      response => this.eventos = response,
      error => console.log(error),
    );

    

  }
}
