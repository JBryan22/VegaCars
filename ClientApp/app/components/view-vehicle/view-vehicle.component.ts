import { PhotoService } from './../../services/photo.service';
import { Vehicle, KeyValuePair } from './../../models/vehicle';
import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router} from '@angular/router';
import * as _ from 'underscore';

@Component({
  selector: 'app-view-vehicle',
  templateUrl: './view-vehicle.component.html',
  styleUrls: ['./view-vehicle.component.css']
})
export class ViewVehicleComponent implements OnInit {
  @ViewChild('fileInput') fileInput: ElementRef;
  vehicleId: number;
  vehicle: Vehicle;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private vehicleService: VehicleService,
    private photoService: PhotoService) { 

      activatedRoute.params.subscribe(p => {
        this.vehicleId = +p['id'] || 0;
        if (isNaN(this.vehicleId) || this.vehicleId <= 0) {
          router.navigate(['vehicles']);
          return;
        }
      })
  }

  ngOnInit() {
    this.vehicleService.getVehicle(this.vehicleId).subscribe(
      v => this.vehicle = v,
      err => {
        if (err.status == 404) {
          this.router.navigate(['vehicles']);
          return;
        }
      }
    );
  }

  delete() {
    if (confirm(`Are you sure you want to delete this vehicle? 
                 This cannot be reversed.`)) {
        this.vehicleService.delete(this.vehicle.id)
            .subscribe(x => {
                this.router.navigate(['/home']);
            });
    }
  }

  uploadPhoto() {
    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;

    this.photoService.upload(this.vehicleId, nativeElement.files![0])
      .subscribe(x => console.log(x));
  }
}
