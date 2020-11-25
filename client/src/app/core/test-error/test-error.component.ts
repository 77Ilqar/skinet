import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent implements OnInit {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }
  get404Error(): void {
    this.http.get(this.baseUrl + 'product/42').subscribe(Response => { console.log(Response); },
    error => { console.log(error); });
  }
  get500Error(): void {
    this.http.get(this.baseUrl + 'buggy/servererror').subscribe(Response => { console.log(Response); },
    error => { console.log(error); });
  }
  get400Error(): void {
    this.http.get(this.baseUrl + 'buggy/badrequest').subscribe(Response => { console.log(Response); },
    error => { console.log(error); });
  }
  get404ValidationError(): void {
    this.http.get(this.baseUrl + 'product/fortytwo').subscribe(Response => { console.log(Response); },
    error => { console.log(error); });
  }


}
