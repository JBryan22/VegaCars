import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { Vehicle } from '../../models/vehicle';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  private readonly PAGE_SIZE = 3;
  queryResult: any = {};
  vehicles: Vehicle[];
  makes: any;
  query: any = {
    pageSize: this.PAGE_SIZE
  };
  columns = [
    { title: 'ID' },
    { title: 'Make', key: 'make', isSortable: true },
    { title: 'Model', key: 'model', isSortable: true },
    { title: 'Contact Name', key: 'contactName', isSortable: true },
    { title: 'Details' },
  ]

  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    this.vehicleService.getMakes().subscribe(data => {
      this.makes = data;
    })
    this.populateVehicles();
  }

  private populateVehicles() {
    this.vehicleService.getVehicles(this.query).subscribe(result => {
      this.queryResult = result;
    });
  }

  onFilterChange() {
    this.query.page = 1;
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
    this.query = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };
    this.populateVehicles();
  }

  sortBy(columnName: string) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateVehicles();
  }

  onPageChange(page: number) {
    this.query.page = page;
    this.populateVehicles();
  }
}
