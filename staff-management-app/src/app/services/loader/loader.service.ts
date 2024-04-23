import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  private _loading = new BehaviorSubject<boolean>(false);
  public readonly loading$ = this._loading.asObservable();

  constructor() {
  }

  show() {
    Swal.fire({
      animation: false,
      title: "Please wait",
      didOpen: () => {
        Swal.showLoading();
        const swalOverlay = document.querySelector('.swal2-container') as HTMLElement;
        swalOverlay.style.zIndex = '9999';
      },

      allowOutsideClick: false

    })

    this._loading.next(true);
  }

  hide() {
    Swal.close()
    this._loading.next(false);

  }
}
