import { Component, Inject } from '@angular/core';
import { HttpClient } from "@angular/common/http";


@Component({
  selector: 'file-upload',
  templateUrl: "file-upload.component.html",
  styleUrls: ["file-upload.component.scss"]
})
export class FileUploadComponent {

  //need to test if it is a csv before running it through this.
  
  fileName = '';

  constructor(private http: HttpClient) { }

  onFileSelected(event) {

    const file: File = event.target.files[0];

    if (file) {

      this.fileName = file.name;

      const formData = new FormData();

      formData.append("thumbnail", file);

      const upload$ = this.http.post("/Clients/AddClients", processFile(formData));

      upload$.subscribe();
    }
  }

}

var processFile = (files: FileList) => {
  if (files && files.length > 0) {
    let file: File = files.item(0);
    let reader: FileReader = new FileReader();
    reader.readAsText(file);
    reader.onload = (e) => {
      return reader.result as string;
    }
  }
}
