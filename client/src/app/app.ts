import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal, Signal } from '@angular/core';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  private http = inject(HttpClient);
  protected readonly title = 'Dating App';
  protected members = signal<any[]>([]);
  ngOnInit(): void {
    this.http.get<any[]>('https://localhost:5001/api/members').subscribe({
      next: response  => this.members.set(response),
      error: err => console.error(err),
      complete: () => console.log('Request complete')
    });
  }
}
