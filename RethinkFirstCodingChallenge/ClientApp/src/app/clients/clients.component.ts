import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './clients.component.html'
})
export class ClientsComponent {
  public clients: Client[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Client[]>(baseUrl + 'clients').subscribe(result => {
      this.clients = result;
    }, error => console.error(error));
  }
}

interface Client {
  firstName: string;
  lastName: number;
  birthdate: number;
  gender: string;
}
