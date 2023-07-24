import { Component, OnInit, Input } from '@angular/core';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { LoaderService } from './loader.service';
import { LoaderState } from './loader.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-loadingspinner',
  templateUrl: './loadingspinner.component.html',
  styleUrls: ['./loadingspinner.component.scss']
})
export class LoadingspinnerComponent implements OnInit {
  showSpinner: boolean = false;
  private subscription: Subscription | undefined;
  constructor(private loadingSpinner: ProgressSpinnerModule, private loaderService: LoaderService) { }

  show() {
    this.showSpinner = true;

  }

  hide() {
    this.showSpinner = false;

  }

  ngOnInit() {
    this.subscription = this.loaderService.loaderState
      .subscribe((state: LoaderState) => {
        this.showSpinner = state.show;
      });
  }
  ngOnDestroy() {
    this.subscription!.unsubscribe();
  }

}
