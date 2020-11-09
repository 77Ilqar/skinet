import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IProduct } from './models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'SkiNet';
  products: IProduct[];
  constructor(private http: HttpClient)
  {}
  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/product').subscribe((Response: IProduct[]) => {
      this.products = Response;
    }, error => {
      console.log(console.error);
    }
    );
  }
}
