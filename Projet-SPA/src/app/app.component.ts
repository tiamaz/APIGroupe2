import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = '4IIR2';
  nom = 'EMSI';


  Afficher(){
    this.title = "MIAGE";
  }


}
