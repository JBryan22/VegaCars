import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { Vehicle } from '../../models/vehicle';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  vehicles: Vehicle[];
  makes: any;
  filter: any = {};

  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    this.vehicleService.getMakes().subscribe(data => {
      this.makes = data;
    })
    this.populateVehicles();
  }

  private populateVehicles() {
    this.vehicleService.getVehicles(this.filter).subscribe(data => {
      this.vehicles = data;
    });
  }

  onFilterChange() {

    this.populateVehicles();

    //This is a way to do filtering on the client side, which makes sense for small data sets. 
    // var vehicles = this.allVehicles;

    // if(this.filter.makeId) {
    //   vehicles = vehicles.filter(v => v.make.id == this.filter.makeId);
    // }

    // if (this.filter.modelId) {
    //   vehicles = vehicles.filter(v => v.model.id == this.filter.modelId);
    // }

    // this.vehicles = vehicles;
  }
  
  resetFilter() {
    this.filter = {};
    this.onFilterChange();
  }
}
