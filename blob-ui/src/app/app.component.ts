import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { FileUploadModule } from 'primeng/fileupload';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { GalleriaModule } from 'primeng/galleria';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, FileUploadModule, HttpClientModule, GalleriaModule],
  templateUrl: './app.component.html',
  styles: [],
})
export class AppComponent {
  images!: string[];

  constructor(private http: HttpClient) { }

  upload(event: { files: File[] }) {
    const form = new FormData();
    event.files.forEach(f => form.append(f.name, f));

    this.http.post<string[]>('https://localhost:7263/image', form)
      .subscribe(x => this.images = x);
  }
}
